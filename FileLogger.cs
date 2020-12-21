/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HicadCommunity
{
	internal class FileLogger
	{
		private static readonly FileLogger logger = new FileLogger();

		/// <summary>
		/// The LogFile location
		/// </summary>
		private readonly string LogFile;

		/// <summary>
		/// The name of the LogFile
		/// </summary>
		private readonly string LogFileName;

		/// <summary>
		/// The path where the LogFile will be placed
		/// </summary>
		private readonly string TempDir;

		private readonly FileStream SourceStream;

		/// <summary>
		/// Initialiser for the Loggin
		/// </summary>
		public FileLogger()
		{
			// Define the filename
			LogFileName = $"{DateTime.Now:yyyy-MM-dd}.log";
			// Define the directory
			TempDir = Path.Combine(Path.GetTempPath(), "HiCAD", "HiCAD Community", Assembly.GetExecutingAssembly().GetName().Name);
			// Create directory if it doesnt exists
			if (!Directory.Exists(TempDir))
				Directory.CreateDirectory(TempDir);
			// Auto delete all old files
			ClearOldLogFiles();
			// Create the LogFile full name
			LogFile = Path.Combine(TempDir, LogFileName);
			// make sure it exists
			if (!File.Exists(LogFile))
				File.Create(LogFile).Close();
			// Create the new Async FileStream for writing
			SourceStream = new FileStream(
					LogFile,
					FileMode.Append,
					FileAccess.Write,
					FileShare.Read,
					bufferSize: 4096,
					useAsync: true);
		}

		public void Write(string module, Exception ex) => _ = WriteAsync(module, ex.Message + " | " + Regex.Replace(ex.StackTrace, @"\t|\n|\r", ""));

		/// <summary>
		/// Write to a file
		/// </summary>
		/// <param name="module">Module name</param>
		/// <param name="line">Line to be logged</param>
		/// <returns></returns>
		public void Write(string module, string line) => _ = WriteAsync(module, line);

		/// <summary>
		/// Log an Exception to the file
		/// </summary>
		/// <param name="ex"></param>
		internal static void Log(Exception ex)
		{
			MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
			Type Class = methodBase.ReflectedType;
			string module = $"{Class.Namespace}.{Class.Name}.{methodBase.Name}";
			logger.Write(module, ex);
		}

		/// <summary>
		/// Log a line to the logfile
		/// </summary>
		/// <param name="line"></param>
		internal static void Log(string line)
		{
			MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
			Type Class = methodBase.ReflectedType;
			string module = $"{Class.Namespace}.{Class.Name}.{methodBase.Name}";
			logger.Write(module, line);
		}

		/// <summary>
		/// Delete al old logfiles (7-days)
		/// </summary>
		internal void ClearOldLogFiles()
		{
			try
			{
				// Make sure the path exists
				if (!Directory.Exists(TempDir))
					return;
				// Get all old files (7days)
				IEnumerable<FileInfo> oldfiles = Directory
					.GetFiles(TempDir)
					.Select(x => new FileInfo(x))
					.Where(x => x.Extension.ToLower() == ".log")
					.Where(x => x.LastAccessTime < DateTime.Now.AddDays(-7));
				// Delete all old files
				foreach (FileInfo fi in oldfiles)
					fi.Delete();
			}
			catch { }
		}

		/// <summary>
		/// Write Async to a file
		/// </summary>
		/// <param name="module">Module name</param>
		/// <param name="line">Line to be logged</param>
		/// <returns></returns>
		private async Task WriteAsync(string module, string line)
		{
			string m = module;
			string l = line;
			await Task.Delay(100);
			try
			{
				// Get all bytes from the line
				byte[] encodedText = Encoding.Unicode.GetBytes($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][{m}] - {l}\r\n");

				// Write to the file
				await SourceStream.WriteAsync(encodedText, 0, encodedText.Length);
				await SourceStream.FlushAsync();
			}
			catch (Exception)
			{
			}
		}
	}
}