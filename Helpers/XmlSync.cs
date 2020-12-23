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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace HicadCommunity.Helpers
{
	/// <summary>
	/// Xml Sync, simplifies casting an object to an XML File
	/// </summary>
	public class XmlSync : PropertyChangedHandler
	{
		/// <summary>
		/// Xml File location
		/// </summary>
		protected readonly string XmlFile;

		/// <summary>
		/// Empty constructor needed for Serialising
		/// </summary>
		public XmlSync()
		{
		}

		/// <summary>
		/// Constructor for XML location
		/// </summary>
		/// <param name="xmlFile">Xml File location</param>
		public XmlSync(string xmlFile)
		{
			// Make sure a xml file is provided
			if (!xmlFile.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase))
				throw new Exception($"'.xml' file is expected, '{Path.GetExtension(xmlFile)}' given");
			// Check if the directory exists
			if (!Directory.Exists(Path.GetDirectoryName(XmlFile)))
				// Create the directory exists
				Directory.CreateDirectory(Path.GetDirectoryName(XmlFile));
			XmlFile = xmlFile;
		}

		/// <summary>
		/// Deserialise from the specified file
		/// </summary>
		protected void DeserialiseFromFile() => DeserialiseFromFile(new StackTrace().GetFrames()[1].GetMethod().DeclaringType);

		/// <summary>
		/// Serialise the current object to a file
		/// </summary>
		protected void SerialisetoFile() => SerialiseToFile(new StackTrace().GetFrame(1).GetMethod().DeclaringType);

		/// <summary>
		/// Serialise the current object to a string
		/// </summary>
		/// <returns></returns>
		protected string SerialiseToString() => SerialiseToString(new StackTrace().GetFrame(1).GetMethod().DeclaringType);

		/// <summary>
		/// Deserialisation handler
		/// </summary>
		/// <param name="type">Type of current object</param>
		private void DeserialiseFromFile(Type type)
		{
			// Check if file exists
			if (File.Exists(XmlFile))
				// Open a StreamReader
				using (TextReader textReader = new StreamReader(XmlFile))
				{
					// Create object from the XML file
					object Configuration = new XmlSerializer(type).Deserialize(textReader);
					// Loop through all properties
					foreach (PropertyInfo prop in Configuration.GetType().GetProperties())
					{
						try
						{
							// Update the field of the current opbject
							GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(Configuration, null));
						}
						catch { }
					}
				}
			else
				// Serialise the current object to a file
				SerialiseToFile(type);
		}

		/// <summary>
		/// File Serialisation handler
		/// </summary>
		/// <param name="type">Type of current object</param>
		private void SerialiseToFile(Type type)
		{
			// Write the XmlString to the XmlFile
			File.WriteAllText(XmlFile, SerialiseToString(type));
		}

		/// <summary>
		/// String Serialisation handler
		/// </summary>
		/// <param name="type">Type of current object</param>
		/// <returns></returns>
		private string SerialiseToString(Type type)
		{
			// Opening a stringwriter
			using (StringWriter writer = new StringWriter())
			{
				// Create XmlSerializer and directly write to the writer
				new XmlSerializer(type).Serialize(writer, this);
				// Return the value of the writer
				return writer.ToString();
			}
		}
	}
}