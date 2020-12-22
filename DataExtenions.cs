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
using ISD.CAD.Base;
using ISD.CAD.Contexts;
using ISD.CAD.Data;
using ISD.CAD.IO;
using ISD.Math;
using ISD.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace HicadCommunity
{
	/// <summary>
	/// Data extensions for all kind of Objects
	/// </summary>
	public static class DataExtenions
	{
		private static UnconstrainedContext Context => ScriptBase.BaseContext as UnconstrainedContext;

		/// <summary>
		/// Get the active SheetMetal part, not the flange or bend zone
		/// </summary>
		/// <param name="context">Current Context</param>
		/// <returns></returns>
		public static PartImpl GetActiveSheetMetalNode(this UnconstrainedContext context)
		{
			// Make sure the Scene is available
			if (context.ActiveScene == null)
				throw new Exception("There is no open active scene");
			try
			{
				// Get the active node
				PartImpl result = (PartImpl)context?.ActiveNode;
				// Check if the result is empty or not a SheetMetal part
				if (result == null || result.Type != NodeType.SheetMetal)
					return null;
				// Return the parent active node
				return result.Parent != null && result.Parent.Type == NodeType.SheetMetal
					? (PartImpl)result.Parent
					: result;
			}
			catch (Exception ex)
			{
				FileLogger.Log(ex);
				return default;
			}
		}

		/// <summary>
		/// Get all Edges belonging to the part
		/// </summary>
		/// <param name="n">Node to use for returning all Edges</param>
		/// <returns></returns>
		public static List<Edge> GetEdges(this Node n) => ((Part)n).Edges;

		/// <summary>
		/// Get all Facets belonging to the part
		/// </summary>
		/// <param name="n">Node to use for returning all Facets</param>
		/// <returns></returns>
		public static List<Facet> GetFacets(this Node n) => ((Part)n).Facets;

		/// <summary>
		/// Get the material name of the part
		/// </summary>
		/// <param name="n">Node to get the material name from</param>
		/// <returns></returns>
		public static string GetMaterialName(this Node n) => n.AttributeSet.GetValue<string>("$07");

		/// <summary>
		/// Get the material size of the part
		/// </summary>
		/// <param name="n">Node to get the material size from</param>
		/// <returns></returns>
		public static double GetMaterialSize(this Node n) => n.AttributeSet.GetValue<double>("§04");

		/// <summary>
		/// Import a DXF/DWG file into the provided scene
		/// </summary>
		/// <param name="scene">The scane where the object needs to imported in</param>
		/// <param name="file">The file which needs to be imported in the scene</param>
		/// <param name="AutoMoveToZeroPoint">Automatically move the imported figure from BottomLeft to 0,0</param>
		/// <param name="SetScaleIndependent">Make the figure scale independent</param>
		/// <returns></returns>
		public static Figure ImportDxfDwg(
			this Scene scene,
			FileInfo file,
			bool AutoMoveToZeroPoint = false,
			bool SetScaleIndependent = false
		) => scene.ImportDxfDwg(file.FullName, AutoMoveToZeroPoint, SetScaleIndependent);

		/// <summary>
		/// Import a DXF/DWG file into the provided scene
		/// </summary>
		/// <param name="scene">The scane where the object needs to imported in</param>
		/// <param name="file">The file which needs to be imported in the scene</param>
		/// <param name="AutoMoveToZeroPoint">Automatically move the imported figure from BottomLeft to 0,0</param>
		/// <param name="SetScaleIndependent">Make the figure scale independent</param>
		/// <returns></returns>
		public static Figure ImportDxfDwg(
			this Scene scene,
			string file,
			bool AutoMoveToZeroPoint = false,
			bool SetScaleIndependent = false)
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
				FigureImpl result = FileIO.Load(file, new DXFImportSettings()) as FigureImpl;
				result.DrawingSheet = Context.ActiveScene.ActiveDrawingSheet;
				// Check if the Figure should me moved as close the the Zero point
				if (AutoMoveToZeroPoint)
				{
					// Only move the figure when there is a distance between them
					if (result.BoundingRect.BottomLeft.GetDistance(new Point2D()) > 0)
						// Move BottomLeft to the Zero point
						result.Move(new Vector2D(result.BoundingRect.BottomLeft, new Point2D()));
				}
				// Check if scale independent should be enablead
				if (SetScaleIndependent)
					// Make
					result.SetScaleIndependent(new Vector2D());
				// Return the imported object
				return result;
			}
			catch (Exception ex)
			{
				// Log the error for debugging purposes
				FileLogger.Log(ex);
				// Return the default value
				return default;
			}
		}

		/// <summary>
		/// Import a STP/STEP file into the provided scene
		/// </summary>
		/// <param name="scene">The scane where the object needs to imported in</param>
		/// <param name="file">The file which needs to be imported in the scene</param>
		/// <returns></returns>
		public static PartImpl ImportStep(this Scene scene, FileInfo file) => scene.ImportStep(file.FullName);

		/// <summary>
		/// Import a STP/STEP file into the provided scene
		/// </summary>
		/// <param name="scene">The scane where the object needs to imported in</param>
		/// <param name="file">The file which needs to be imported in the scene</param>
		/// <returns></returns>
		public static PartImpl ImportStep(this Scene scene, string file)
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
				PartImpl result = FileIO.Load(file, new StepImportSettings()) as PartImpl;
				// Return the imported object
				return result;
			}
			catch (Exception ex)
			{
				// Log the error for debugging purposes
				FileLogger.Log(ex);
				// Return the default value
				return default;
			}
		}

		/// <summary>
		/// Check if the current Node is referenced
		/// </summary>
		/// <param name="n">Node to check if it's referenced</param>
		/// <returns></returns>
		public static bool IsReferenced(this Node n) => n != null && n.Exists && n.Reference != null;

		/// <summary>
		/// Check if the current Node is externally referenced
		/// </summary>
		/// <param name="n">Node to check if it's externally referenced</param>
		/// <returns></returns>
		public static bool IsReferencedExternal(this Node n) => n.IsReferenced() && !string.IsNullOrEmpty(n.Reference.Location);

		/// <summary>
		/// Check if the current Node is internally referenced
		/// </summary>
		/// <param name="n">Node to check if it's internally referenced</param>
		/// <returns></returns>
		public static bool IsReferencedInternal(this Node n) => n.IsReferenced() && string.IsNullOrEmpty(n.Reference.Location);

		/// <summary>
		/// Parse a product path to a local/network file/directory
		/// </summary>
		/// <example>
		/// </example>
		/// <param name="file">file/directory to be paresd</param>
		/// <returns></returns>
		public static string ParseProductPath(this string file)
		{
			if (file is null)
				throw new ArgumentNullException(nameof(file));
			try
			{
				// Checks if the file exists
				if (File.Exists(file))
					// return provided file/directory
					return file;

				// check if first+third 'character' is a '\' then use hicad product path, may not start with a '\'
				if (file[0] != '\\' && file[2] != '\\')
				{
					// perform a regex
					Match regMatch = Regex.Match(file, @"(^[A-Z#0-9]):(.+)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
					// check if a match is found
					if (regMatch != null && regMatch.Groups != null && regMatch.Groups.Count > 0)
					{
						// return newly formatted file/directory
						return string.Format(
							"{0}{1}",
							Context.Configuration.GetProductPath(regMatch.Groups[1].Value),
							regMatch.Groups[2].Value
						);
					}
				}

				return file;
			}
			catch (Exception ex)
			{
				// Log the error for debugging purposes
				FileLogger.Log(ex);
				// Return the default value
				return default;
			}
		}

		/// <summary>
		/// Update a Attribute value or delete it
		/// </summary>
		/// <param name="attrSet">Attribute set to be used</param>
		/// <param name="name">Name of the requested attribute name</param>
		/// <param name="value">Value to be set</param>
		public static void SetObjectValue(this AttributeSet attrSet, string name, object value)
		{
			// Check if the parameters are correctly provided
			if (attrSet is null)
				throw new ArgumentNullException(nameof(attrSet));
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException($"'{nameof(name)}' cannot be null or empty", nameof(name));

			// Checks if the provided name must be deleted
			if (value == null)
			{
				if (attrSet.Contains(name))
					attrSet.Remove(name);
			}
			else
			{
				// Check the type of the value
				switch (value)
				{
					case string vString:
						// Check if the key not exists or the value differs
						if (attrSet.GetValue<string>(name) != vString)
							// Set the value
							attrSet.SetValue(name, vString);
						break;

					case double vDouble:
						// Check if the key not exists or the value differs
						if (attrSet.GetValue<double>(name) != vDouble)
							// Set the value
							attrSet.SetValue(name, vDouble);
						break;

					case int vInt:
						// Check if the key not exists or the value differs
						if (attrSet.GetValue<int>(name) != vInt)
							// Set the value
							attrSet.SetValue(name, vInt);
						break;

					default:
						// Default handling
						attrSet.SetValue(name, value);
						break;
				}
			}
		}
	}
}