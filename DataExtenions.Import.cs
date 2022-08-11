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

using HiCAD.Data;
using ISD.CAD.Data;
using ISD.CAD.IO;
using ISD.Math;
using System;
using System.IO;

namespace RDM.HicadCommunity
{
	public static partial class DataExtenions
	{
		#region STP

		/// <summary>
		/// Import a STP/STEP file into the provided scene
		/// </summary>
		/// <param name="scene">The scene where the object needs to imported in</param>
		/// <param name="ms">The file which is stored in a MemoryStream</param>
		/// <returns></returns>
		public static Node ImportStp(this Scene scene, MemoryStream ms)
		{
			// Create a Filename
			string tmpFile = Path.GetTempFileName();
			try
			{
				// Create a filestream
				using (FileStream fs = new FileStream(tmpFile, FileMode.Create, FileAccess.Write))
				{
					// Write to the file
					ms.WriteTo(fs);
				}
				// Import the DXF/DWG
				Node result = scene.ImportStp(tmpFile);
				// return the result
				return result;
			} catch (Exception)
			{
				return default;
			} finally
			{
				// Check if the file exists
				if (File.Exists(tmpFile))
					// Delete the temporary file
					File.Delete(tmpFile);
			}
		}

		/// <summary>
		/// Import a STP/STEP file into the provided scene
		/// </summary>
		/// <param name="scene">The scane where the object needs to imported in</param>
		/// <param name="file">The file which needs to be imported in the scene</param>
		/// <returns></returns>
		public static Node ImportStp(this Scene scene, FileInfo file) => scene.ImportStp(file.FullName);

		/// <summary>
		/// Import a STP/STEP file into the provided scene
		/// </summary>
		/// <param name="scene">The scane where the object needs to imported in</param>
		/// <param name="file">The file which needs to be imported in the scene</param>
		/// <returns></returns>
		public static Node ImportStp(this Scene scene, string file)
		{
			// Note:
			// STP import settings can only be managed in the CFGDB: Interfaces > General 3-D interfaces
			// "AutoOptimise" and "Repair Tool" are disabled by default, its advised to enable these options
			// See the online help for detailed information for both options

			// Check if the parameters are correctly provided
			if (scene is null)
				throw new ArgumentNullException(nameof(scene));
			if (file is null)
				throw new ArgumentNullException(nameof(file));

			try
			{
				// The scene must be active in order to load any new objects
				if (!scene.Active)
					throw new Exception("Provided Scene is not activated");
				// Load the STP File
				Node result = FileIO.Load(file, new StepImportSettings()) as Node;
				// Update browser
				Context.EnforceBrowserUpdate();
				// Return the imported object
				return result;
			} catch (Exception ex)
			{
				// Log the error for debugging purposes
				Logger.Log(ex);
				// Return the default value
				return default;
			}
		}

		#endregion STP

		#region DXF

		/// <summary>
		/// Import a DXF from a byte
		/// </summary>
		/// <param name="scene">The scene where the object needs to imported in</param>
		/// <param name="ms">The file which is stored in a MemoryStream</param>
		/// <param name="autoMoveToZeroPoint">Automatically move the imported figure from BottomLeft to 0,0</param>
		/// <param name="setScaleIndependent">Make the figure scale independent</param>
		/// <param name="configurationFile">Import Configuration file, default is 'C:\HiCAD\sys\acadhcad.dat'</param>
		/// <returns></returns>
		public static Figure ImportDxfDwg(this Scene scene, MemoryStream ms, bool autoMoveToZeroPoint = false, bool setScaleIndependent = false, string configurationFile = null)
		{
			// Create a Filename
			string tmpFile = Path.GetTempFileName();
			try
			{
				// Create a filestream
				using (FileStream fs = new FileStream(tmpFile, FileMode.Create, FileAccess.Write))
				{
					// Write to the file
					ms.WriteTo(fs);
				}
				// Import the DXF/DWG
				Figure result = scene.ImportDxfDwg(tmpFile, autoMoveToZeroPoint, setScaleIndependent, configurationFile);
				// Update browser
				Context.EnforceBrowserUpdate();
				// return the result
				return result;
			} catch (Exception)
			{
				return default;
			} finally
			{
				// Check if the file exists
				if (File.Exists(tmpFile))
					// Delete the temporary file
					File.Delete(tmpFile);
			}
		}

		/// <summary>
		/// Import a DXF/DWG file into the provided scene
		/// </summary>
		/// <param name="scene">The scane where the object needs to imported in</param>
		/// <param name="file">The file which needs to be imported in the scene</param>
		/// <param name="autoMoveToZeroPoint">Automatically move the imported figure from BottomLeft to 0,0</param>
		/// <param name="setScaleIndependent">Make the figure scale independent</param>
		/// <param name="configurationFile">Import Configuration file, default is 'C:\HiCAD\sys\acadhcad.dat'</param>
		/// <returns></returns>
		public static Figure ImportDxfDwg(this Scene scene, FileInfo file, bool autoMoveToZeroPoint = false, bool setScaleIndependent = false, string configurationFile = null)
			=> scene.ImportDxfDwg(file.FullName, autoMoveToZeroPoint, setScaleIndependent, configurationFile);

		/// <summary>
		/// Import a DXF/DWG file into the provided scene
		/// </summary>
		/// <param name="scene">The scene where the object needs to imported in</param>
		/// <param name="file">The file which needs to be imported in the scene</param>
		/// <param name="autoMoveToZeroPoint">Automatically move the imported figure from BottomLeft to 0,0</param>
		/// <param name="setScaleIndependent">Make the figure scale independent</param>
		/// <param name="configurationFile">Import Configuration file, default is 'C:\HiCAD\sys\acadhcad.dat'</param>
		/// <returns></returns>
		public static Figure ImportDxfDwg(this Scene scene, string file, bool autoMoveToZeroPoint = false, bool setScaleIndependent = false, string configurationFile = null)
		{
			// Note:
			// DXF/DWG import settings can only be managed in the CFGDB: Interfaces > General 3-D interfaces

			// Check if the parameters are correctly provided
			if (scene is null)
				throw new ArgumentNullException(nameof(scene));
			if (file is null)
				throw new ArgumentNullException(nameof(file));

			try
			{
				// The scene must be active in order to load any new objects
				if (!scene.Active)
					throw new Exception("Provided Scene is not activated");
				// Load the DXF/DWG File, All configurations can be found in the CFGDB:s
				FigureImpl result = FileIO.Load(
					file,
					string.IsNullOrEmpty(configurationFile) ? new DXFImportSettings() : new DXFImportSettings() { ConfigurationFile = configurationFile }
				) as FigureImpl;
				// Update browser
				Context.EnforceBrowserUpdate();
				result.DrawingSheet = Context.ActiveScene.ActiveDrawingSheet;
				// Check if the Figure should me moved as close the the Zero point
				if (autoMoveToZeroPoint)
				{
					// Only move the figure when there is a distance between them
					if (result.BoundingRect.BottomLeft.GetDistance(new Point2D()) > 0)
						// Move BottomLeft to the Zero point
						result.Move(new Vector2D(result.BoundingRect.BottomLeft, new Point2D()));
				}
				// Check if scale independent should be enablead
				if (setScaleIndependent)
					// Make
					result.SetScaleIndependent(new Vector2D());
				// Return the imported object
				return result;
			} catch (Exception ex)
			{
				// Log the error for debugging purposes
				Logger.Log(ex);
				// Return the default value
				return default;
			}
		}

		#endregion DXF
	}
}