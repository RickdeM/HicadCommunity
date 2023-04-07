using ISD.CAD.Base;
using ISD.CAD.Contexts;
using ISD.CAD.Contexts.Settings;
using ISD.CAD.Data;
using ISD.Math;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace RDM.HicadCommunity
{
	public static partial class DataExtenions
	{
		/// <summary>
		/// Flag for status of the HiCAD messages
		/// </summary>
		private static bool MessagesActive = true;

		/// <summary>
		/// Flag for status of the Configuration Redraw
		/// </summary>
		private static bool RedrawActive = true;

		/// <summary>
		/// Flag for status of the UI
		/// </summary>
		private static bool UIActive = true;

		/// <summary>
		/// Close all opened drawings
		/// </summary>
		/// <param name="context">Context which contains all Scenes</param>
		/// <param name="save">Save the drawings before closing it</param>
		/// <param name="savePartReference">How to handle the saving of external referenced parts</param>
		public static void CloseAllDrawings(
			this UnconstrainedContext context,
			bool save = true,
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
						if (save)
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
				Logger.Log(ex);
			}
		}

		/// <summary>
		/// Create an orthonogal Coordination System by 3 Points
		/// </summary>
		/// <param name="context">used for creating the extenions method</param>
		/// <param name="pointO">Origion Point</param>
		/// <param name="pointX">Point on the X-axis</param>
		/// <param name="pointY">Point on the Y-axis</param>
		/// <returns></returns>
		public static CoordinateSystem CreateCoordinateSystemOrthonogal(this UnconstrainedContext context, Point3D pointO, Point3D pointX, Point3D pointY)
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
					pointO,
					new NormVector3D(new Vector3D(pointO, pointX)),
					new NormVector3D(new Vector3D(new Line3D(pointO, pointX).NearestPoint(pointY), pointY))
				);
				// Return the result
				return result;
			}
			catch (Exception ex)
			{
				// Log the error for debugging purposes
				Logger.Log(ex);
				// Return the default value
				return default;
			}
		}

		/// <summary>
		/// Get the active SheetMetal part, not the flange or bend zone
		/// </summary>
		/// <param name="context">Current Context</param>
		/// <returns></returns>
		public static Node GetActiveSheetMetalNode(this UnconstrainedContext context)
		{
			// Make sure the Scene is available
			if (context.ActiveScene == null)
				throw new Exception("There is no open active scene");
			try
			{
				// Get the active node
				Node result = context?.ActiveNode;
				// Check if the result is empty or not a SheetMetal part
				if (result == null || result.Type != NodeType.SheetMetal)
					return null;
				// Return the parent active node
				return result.Parent != null && result.Parent.Type == NodeType.SheetMetal
					? result.Parent
					: result;
			}
			catch (Exception ex)
			{
				// Log the error for debugging purposes
				Logger.Log(ex);
				// Return the default value
				return default;
			}
		}

		/// <summary>
		/// Get the location of the Kataloge
		/// </summary>
		/// <param name="context">Current Context</param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static string GetCatalogDirectory(this UnconstrainedContext context)
		{
			try
			{
				using (RegistryKey reg = Registry.LocalMachine.OpenSubKey($"SOFTWARE\\ISD Software und Systeme\\HiCAD\\{context.Version}", false))
					return reg.GetValue("CatDir").ToString();
			}
			catch (Exception ex)
			{
				// Log the error for debugging purposes
				Logger.Log(ex);
				// Return the default value
				return default;
			}
		}

		/// <summary>
		/// Get the directory of the CFGDB ( Configuration Database )
		/// </summary>
		/// <param name="context">Current Context</param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static string GetCfgdbDirectory(this UnconstrainedContext context) => Path.GetDirectoryName(context.GetCfgdbFile());

		/// <summary>
		/// Get the location of the CFGDB ( Configuration Database )
		/// </summary>
		/// <param name="context">Current Context</param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static string GetCfgdbFile(this UnconstrainedContext context)
		{
			try
			{
				using (RegistryKey reg = Registry.LocalMachine.OpenSubKey($"SOFTWARE\\ISD Software und Systeme\\HiCAD\\{context.Version}", false))
					return reg.GetValue("CfgDbPath").ToString();
			}
			catch (Exception ex)
			{
				// Log the error for debugging purposes
				Logger.Log(ex);
				// Return the default value
				return default;
			}
		}

		/// <summary>
		/// Activate the HiCAD Messages
		/// </summary>
		/// <param name="context"></param>
		public static void MessagesActivate(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Can only be Activated when its Deactivated
			if (!MessagesActive)
			{
				// Activate the HiCAD Messages
				context.Configuration.ActivateMessages();
				// Set the flag
				MessagesActive = true;
			}
		}

		/// <summary>
		/// Check if the HiCAD Messages are Activated
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static bool MessagesActivated(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Return if the HiCAD Messages are Activated
			return MessagesActive;
		}

		/// <summary>
		/// Deactivate the HiCAD Messages
		/// </summary>
		/// <param name="context"></param>
		public static void MessagesDeactivate(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Can only be Deactivated when its Activated
			if (MessagesActive)
			{
				// Deactivate the HiCAD Messages
				context.Configuration.DeactivateMessages();
				// Set the flag
				MessagesActive = false;
			}
		}

		/// <summary>
		/// Check if the HiCAD Messages are Deactivated
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static bool MessagesDeactivated(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Return if the HiCAD Messages are Deactivated
			return MessagesActive == false;
		}

		/// <summary>
		/// Activate the Redraw (3D renderings)
		/// </summary>
		/// <param name="context"></param>
		public static void RedrawActivate(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Can only be Activated when its Deactivated
			if (!RedrawActive)
			{
				// Activate the redraw (3D renderings)
				context.Configuration.ActivateRedraw();
				// Set the flag
				RedrawActive = true;
			}
		}

		/// <summary>
		/// Check if 'Redraw' (3D renderings) is Activated
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static bool RedrawActivated(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Return if the Redraw is Activated
			return RedrawActive;
		}

		/// <summary>
		/// Deactivate the Redraw (3D renderings)
		/// </summary>
		/// <param name="context"></param>
		public static void RedrawDeactivate(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Can only be Deactivated when its Activated
			if (RedrawActive)
			{
				// Deactivate the redraw (3D renderings)
				context.Configuration.DeactivateRedraw();
				// Set the flag
				RedrawActive = false;
			}
		}

		/// <summary>
		/// Check if 'Redraw' (3D renderings) is Deactivated
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static bool RedrawDeactivated(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Return if the Redraw is Deactivated
			return RedrawActive == false;
		}

		/// <summary>
		/// Activate the UI
		/// </summary>
		/// <param name="context"></param>
		public static void UIActivate(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Can only be Activated when its Deactivated
			if (!UIActive)
			{
				// Activate the UI
				context.Configuration.ActivateUserInterface();
				// Set the flag
				UIActive = true;
			}
		}

		/// <summary>
		/// Check if the UI is Activated
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static bool UIActivated(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Return if the UI is Activated
			return UIActive;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="context"></param>
		public static void UIDeactivate(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Can only be Deactivated when its Activated
			if (UIActive)
			{
				// Deactivate the UI
				context.Configuration.DeactivateUserInterface();
				// Set the flag
				UIActive = false;
			}
		}

		/// <summary>
		/// Check if the UI is Deactivated
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static bool UIDeactivated(this UnconstrainedContext context)
		{
			// Make sure the context is available
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			// Return if the UI is Deactivated
			return UIActive == false;
		}
	}
}