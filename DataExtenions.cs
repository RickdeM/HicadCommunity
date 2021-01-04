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
using ISD.BaseTypes;
using ISD.CAD.Base;
using ISD.CAD.Contexts;
using ISD.CAD.Contexts.Settings;
using ISD.CAD.Data;
using ISD.CAD.IO;
using ISD.Math;
using ISD.Scripting;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		/// Activate the Scene slot using UpdatePartReference
		/// </summary>
		/// <param name="slot">SceneSlot to be activated</param>
		/// <param name="update">How to handle the updating of external referenced parts</param>
		/// <returns></returns>
		public static SceneSlot Activate(this SceneSlot slot, UpdatePartReferences update)
		{
			// Activate the scene
			slot.Scene.Activate(update);
			// Return the provided SceneSlot
			return slot;
		}

		/// <summary>
		/// Activate the Scene slot using UpdatePartReference
		/// </summary>
		/// <param name="scene">Scene to be activated</param>
		/// <param name="update">How to handle the updating of external referenced parts</param>
		/// <returns></returns>
		public static Scene Activate(this Scene scene, UpdatePartReferences update)
		{
			// Load user preferences
			UpdatePartReferences tmpSave = Context.Configuration.Settings.UpdatePartReferences;
			try
			{
				// Override Reference settings
				Context.Configuration.Settings.UpdatePartReferences = update;
				// Actually load the drawing
				scene.Activate();
			}
			catch (Exception ex)
			{
				// Log the error for debugging purposes
				FileLogger.Log(ex);
			}
			finally
			{
				// Reset Reference settings
				Context.Configuration.Settings.UpdatePartReferences = tmpSave;
			}
			// Return the provided Scene
			return scene;
		}

		/// <summary>
		/// Close all opened drawings
		/// </summary>
		/// <param name="context">Context which contains all Scenes</param>
		/// <param name="saveDrawings">Save the drawings before closing it</param>
		/// <param name="savePartReference">How to handle the saving of external referenced parts</param>
		public static void CloseAllDrawings(
			this UnconstrainedContext context,
			bool saveDrawings = true,
			SavePartReferences savePartReference = SavePartReferences.Modified)
		{
			try
			{
				// Loop through all sceneslots
				foreach (SceneSlot sceneSlot in context.SceneSlots)
				{
					try
					{
						// If no scene is available, skip the slot
						if (sceneSlot.Scene == null)
							continue;
						// activate in order to acces Save/Load function
						sceneSlot.Scene.Activate();
						// Check if the drawing should be saved
						if (saveDrawings)
							Context.ActiveScene.Save(savePartReference);
						// close the drawing
						Context.ActiveScene.Close();
					}
					catch { }
				}
			}
			catch (Exception ex)
			{
				// Log the error for debugging purposes
				FileLogger.Log(ex);
			}
		}

		/// <summary>
		/// Create an orthonogal Coordination System by 3 Points
		/// </summary>
		/// <param name="context">used for creating the extenions method</param>
		/// <param name="PointO">Origion Point</param>
		/// <param name="PointX">Point on the X-axis</param>
		/// <param name="PointY">Point on the Y-axis</param>
		/// <returns></returns>
		public static CoordinateSystem CreateCoordinateSystemOrthonogal(this UnconstrainedContext context, Point3D PointO, Point3D PointX, Point3D PointY)
		{
			// Check the parameter
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			try
			{
				//      Y
				//      ^
				//  O - - - > X
				//
				// Y-vector is defind by: Nearest point Y on O>X then Point Y
				// Create the Coordination System
				CoordinateSystem result = new CoordinateSystem(
					PointO,
					new NormVector3D(new Vector3D(PointO, PointX)),
					new NormVector3D(new Vector3D(new Line3D(PointO, PointX).NearestPoint(PointY), PointY))
				);
				// Return the result
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
				// Log the error for debugging purposes
				FileLogger.Log(ex);
				// Return the default value
				return default;
			}
		}

		/// <summary>
		/// Get the location of the Kataloge
		/// </summary>
		/// <param name="context">Current Context</param>
		/// <returns></returns>
		public static string GetCatalogDirectory(this UnconstrainedContext context)
		{
			try
			{
				using (RegistryKey reg = Registry.LocalMachine.OpenSubKey($"SOFTWARE\\ISD Software und Systeme\\HiCAD\\{context.Version}", true))
					return reg.GetValue("CatDir").ToString();
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
		/// Get the directory of the CFGDB ( Configuration Database )
		/// </summary>
		/// <param name="context">Current Context</param>
		/// <returns></returns>
		public static string GetCfgdbDirectory(this UnconstrainedContext context) => Path.GetDirectoryName(context.GetCfgdbFile());

		/// <summary>
		/// Get the location of the CFGDB ( Configuration Database )
		/// </summary>
		/// <param name="context">Current Context</param>
		/// <returns></returns>
		public static string GetCfgdbFile(this UnconstrainedContext context)
		{
			try
			{
				using (RegistryKey reg = Registry.LocalMachine.OpenSubKey($"SOFTWARE\\ISD Software und Systeme\\HiCAD\\{context.Version}", true))
					return reg.GetValue("CfgDbPath").ToString();
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
		/// Get all Edges belonging to the part
		/// </summary>
		/// <param name="n">Node to use for returning all Edges</param>
		/// <returns></returns>
		public static List<Edge> GetEdges(this Node n) => n.GetPart().Edges;

		/// <summary>
		/// Get all Facets belonging to the part
		/// </summary>
		/// <param name="n">Node to use for returning all Facets</param>
		/// <returns></returns>
		public static List<Facet> GetFacets(this Node n) => n.GetPart().Facets;

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
		/// Get The Part
		/// </summary>
		/// <param name="n">Node to get the part of</param>
		/// <returns></returns>
		public static Part GetPart(this Node n) => (Part)n;

		/// <summary>
		/// Get a all PartsList relevant nodes from the given node.
		/// </summary>
		/// <param name="root"></param>
		/// <returns></returns>
		public static List<Node> GetProductStructure(this Node root)
		{
			// Get all nodes which are directly PartListRelevant
			List<Node> result = root.SubNodes.Where(x => x.IsPartsListRelevant).ToList();
			// Search 1 level deaper each sub node which is not Bom relevant
			foreach (Node n in root.SubNodes.Where(x => !x.IsPartsListRelevant))
				result.AddRange(n.GetProductStructure());
			// Return Ordered list: Ridder Pos > Item Number
			return result.OrderBy(x => x.Properties.ItemNumber).ToList();
		}

		/// <summary>
		/// Import a DXF/DWG file into the provided scene
		/// </summary>
		/// <param name="scene">The scane where the object needs to imported in</param>
		/// <param name="file">The file which needs to be imported in the scene</param>
		/// <param name="AutoMoveToZeroPoint">Automatically move the imported figure from BottomLeft to 0,0</param>
		/// <param name="SetScaleIndependent">Make the figure scale independent</param>
		/// <param name="configurationFile">Import Configuration file, default is 'C:\HiCAD\sys\acadhcad.dat'</param>
		/// <returns></returns>
		public static Figure ImportDxfDwg(
			this Scene scene,
			FileInfo file,
			bool AutoMoveToZeroPoint = false,
			bool SetScaleIndependent = false,
			string configurationFile = null
		) => scene.ImportDxfDwg(file.FullName, AutoMoveToZeroPoint, SetScaleIndependent, configurationFile);

		/// <summary>
		/// Import a DXF/DWG file into the provided scene
		/// </summary>
		/// <param name="scene">The scene where the object needs to imported in</param>
		/// <param name="file">The file which needs to be imported in the scene</param>
		/// <param name="AutoMoveToZeroPoint">Automatically move the imported figure from BottomLeft to 0,0</param>
		/// <param name="SetScaleIndependent">Make the figure scale independent</param>
		/// <param name="configurationFile">Import Configuration file, default is 'C:\HiCAD\sys\acadhcad.dat'</param>
		/// <returns></returns>
		public static Figure ImportDxfDwg(
			this Scene scene,
			string file,
			bool AutoMoveToZeroPoint = false,
			bool SetScaleIndependent = false,
			string configurationFile = null)
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
		/// Move a node given two points
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="start">Start point</param>
		/// <param name="end">End point</param>
		/// <returns></returns>
		public static T Move<T>(this T node, Point3D start, Point3D end) where T : Node => node.Move(new Vector3D(start, end));

		/// <summary>
		/// Move a node by the given vector
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="vec">Vector used for movement</param>
		/// <returns></returns>
		public static T Move<T>(this T node, Vector3D vec) where T : Node
		{
			// Move the Node
			new Transformation(vec).Apply(node);
			// Return the Node
			return node;
		}

		/// <summary>
		/// Move a Node using Workingplanes
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="start">Start CoordinateSystem for movement </param>
		/// <param name="end">End CoordinateSystem for movement</param>
		/// <returns></returns>
		public static T Move<T>(this T node, WorkingPlane start, WorkingPlane end) where T : Node => node.Move(start.CoordinateSystem, end.CoordinateSystem);

		/// <summary>
		/// Move a Node using Workingplanes
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="start">Start CoordinateSystem for movement </param>
		/// <param name="end">End CoordinateSystem for movement</param>
		/// <returns></returns>
		public static T Move<T>(this T node, CoordinateSystem start, WorkingPlane end) where T : Node => node.Move(start, end.CoordinateSystem);

		/// <summary>
		/// Move a Node using Workingplanes
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="start">Start CoordinateSystem for movement </param>
		/// <param name="end">End CoordinateSystem for movement</param>
		/// <returns></returns>
		public static T Move<T>(this T node, WorkingPlane start, CoordinateSystem end) where T : Node => node.Move(start.CoordinateSystem, end);

		/// <summary>
		/// Move a Node using CoordinationSystems
		/// </summary>
		/// <param name="node">Node to be moved</param>
		/// <param name="start">Start CoordinateSystem for movement </param>
		/// <param name="end">End CoordinateSystem for movement</param>
		/// <returns></returns>
		public static T Move<T>(this T node, CoordinateSystem start, CoordinateSystem end) where T : Node
		{
			// Move the Node
			start.BaseTransformation(end).Apply(node);
			// Return the Node
			return node;
		}

		/// <summary>
		/// Open a new SceneSlot, or load a drawing in a new slot
		/// </summary>
		/// <param name="context">Context which contains the SceneSlots</param>
		/// <param name="sceneName">FileName to open/create</param>
		/// <param name="readOnly">When opening existing document, open in readonly mode</param>
		/// <param name="update">How to handle the updating of external referenced parts</param>
		/// <returns></returns>
		public static Scene OpenNewSlot(this UnconstrainedContext context, string sceneName, bool readOnly = false, UpdatePartReferences update = UpdatePartReferences.Newer)
		{
			// Check if the parameters are correctly provided
			if (string.IsNullOrEmpty(sceneName))
				throw new ArgumentException("'fileName' cannot be null or empty", "fileName");
			// Check if a valid filename is provided
			if (!sceneName.EndsWith(".sza", StringComparison.InvariantCultureIgnoreCase))
				throw new Exception("File with the extension 'SZA' is expected");
			//throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty", nameof(fileName));
			// Get the first empty scene slot
			SceneSlot Empyslot = context.SceneSlots.FirstOrDefault(x => x.Scene == null);
			// Check if a slot is available
			if (Empyslot == null)
				throw new Exception(string.Format("All SceneSlots are in use. Open drawings limit of {0} reached", context.SceneSlots.Count()));
			// Check if the file not already exists
			if (!File.Exists(sceneName))
			{
				// Activate the SceneSlote
				Empyslot.Activate(update);
				// Create new Scene, trimming the ".SZA" otherwise the name will end up like 'Drawing1.SZA.SZA'
				context.NewScene(
					Path.Combine(Path.GetDirectoryName(sceneName), Path.GetFileNameWithoutExtension(sceneName))
				);
				// Return the created scene
				return context.ActiveScene;
			}
			else
			{
				// File already exists, look for matching names
				// Name of the Scene is either fully defined, or filename withouth any extension
				IEnumerable<SceneSlot> slots = context.SceneSlots.Where(x =>
					x.Scene != null &&
					(
						string.Equals(Path.GetFileNameWithoutExtension(x.Scene.Name), Path.GetFileNameWithoutExtension(sceneName), StringComparison.InvariantCultureIgnoreCase) ||
						string.Equals(Path.GetFileName(x.Scene.Name), Path.GetFileName(sceneName), StringComparison.InvariantCultureIgnoreCase)
					)
				);
				// Check if any matching scenes are found
				if (slots.Count() > 0)
				{
					// Check if any is exactly like the provided sceneName
					SceneSlot slotExact = slots.FirstOrDefault(x => string.Equals(x.Scene.Name, sceneName, StringComparison.InvariantCultureIgnoreCase));
					if (slotExact != null)
					{
						// Activate the 'current' drawing since the name is fully defined
						slotExact.Activate(update);
						// Return the scene
						return slotExact.Scene;
					}
					else
					{
						// No exactt match is found, check each slot
						foreach (SceneSlot slot in slots)
						{
							// Activate the slote
							slot.Activate(update);
							// Check if the Scene name is now exactly like the provided sceneName
							if (string.Equals(slot.Scene.Name, sceneName, StringComparison.InvariantCultureIgnoreCase))
							{
								// Return the activated scene
								return slot.Scene;
							}
						}
					}
				}
				// File which already exists is not openend in any slot, Activate the new slot
				Empyslot.Activate(update);
				// Load the request SceneName
				return context.Load(sceneName, readOnly);
			}
		}

		/// <summary>
		/// Parse a product path to a local/network file/directory
		/// </summary>
		/// <example>
		/// </example>
		/// <param name="file">file/directory to be paresd</param>
		/// <returns></returns>
		public static string ParseProductPath(this string file)
		{
			// Check if the parameters are correctly provided
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
						// return newly formatted file/directory
						return $"{Context.Configuration.GetProductPath(regMatch.Groups[1].Value)}{regMatch.Groups[2].Value}";
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
		/// Rotate a Node
		/// </summary>
		/// <param name="coor">Node to be rotated</param>
		/// <param name="angle">Angle in degrees</param>
		/// <param name="vector">Rotating vector (angle is CCW, rotates from X-axis to Y-axis), default axis is Z</param>
		/// <param name="point">Rotating point, default Point is 0,0,0</param>
		/// <returns></returns>
		public static CoordinateSystem Rotate(this CoordinateSystem coor, Angle angle, NormVector3D? vector = null, Point3D? point = null)
		{
			var tfs = new Transformation();
			tfs.SetRotation(point ?? new Point3D(), vector ?? NormVector3D.E3, angle);
			coor.Transform(tfs);
			return coor;
		}

		/// <summary>
		/// Rotate a Node
		/// </summary>
		/// <param name="node">Node to be rotated</param>
		/// <param name="angle">Angle in degrees</param>
		/// <param name="vector">Rotating vector (angle is CCW, rotates from X-axis to Y-axis), default axis is Z</param>
		/// <param name="point">Rotating point, default Point is 0,0,0</param>
		/// <returns></returns>
		public static T Rotate<T>(this T node, Angle angle, NormVector3D? vector = null, Point3D? point = null) where T : Node
		{
			// Create Transformation
			var tfs = new Transformation();
			tfs.SetRotation(point ?? new Point3D(), vector ?? NormVector3D.E3, angle);
			// Transform the Node
			tfs.Apply(node);
			// Return the node
			return node;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="scene">Scene to be saved</param>
		/// <param name="savePartReference">How to handle the saving of external referenced parts</param>
		/// <returns></returns>
		public static Scene Save(this Scene scene, SavePartReferences savePartReference)
		{
			// Load user preferences
			SavePartReferences tmpSave = Context.Configuration.Settings.SavePartReferences;
			try
			{
				// Override Reference settings
				Context.Configuration.Settings.SavePartReferences = savePartReference;
				// Actually load the drawing
				scene.Save();
			}
			catch (Exception ex)
			{
				// Log the error for debugging purposes
				FileLogger.Log(ex);
			}
			finally
			{
				// Reset Reference settings
				Context.Configuration.Settings.SavePartReferences = tmpSave;
			}
			return scene;
		}

		/// <summary>
		/// Update a Attribute value or delete it
		/// </summary>
		/// <param name="attrSet">Attribute set to be used</param>
		/// <param name="name">Name of the requested attribute name</param>
		/// <param name="value">Value to be set</param>
		public static AttributeSet SetObjectValue(this AttributeSet attrSet, string name, object value)
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
			return attrSet;
		}

		/// <summary>
		/// Set the BOM relevancy of a Node
		/// </summary>
		/// <param name="n">Node to be set/unset for BOM relevance</param>
		/// <param name="isRelevant">Flag for BOM relevant or not</param>
		/// <returns></returns>
		public static T SetPartsListRelevant<T>(this T n, bool isRelevant) where T : Node
		{
			try
			{
				// Check if the node is not null and exists
				if (n != null && n.Exists)
					// Set the Bom Relevance to the required value
					n.AttributeSet.SetObjectValue(SystemAttributes.Bomrelevant, isRelevant ? 1 : 0);
			}
			catch (Exception ex)
			{
				// Log the error for debugging purposes
				FileLogger.Log(ex);
			}
			// Return the Node
			return n;
		}
	}
}