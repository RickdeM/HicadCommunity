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
using ISD.CAD.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RDM.HicadCommunity
{
	/// <summary>
	/// Asynchrone selection handler
	/// </summary>
	public static class SmartSelection
	{
		/// <summary>
		/// This is the Exception message when user is canceling the selecting with for example: ESC
		/// </summary>
		public static string CancelMessage = "Cancelling";

		/// <summary>
		/// Result Dataset to be returned with onFinish
		/// </summary>
		private static readonly List<object> Result = new List<object>();

		/// <summary>
		/// Create a Selection list to be used for continues Async Selection events
		/// </summary>
		private static readonly List<SmartSelectionDataSet> selectionList = new List<SmartSelectionDataSet>();

		/// <summary>
		/// Delegate void for exit selection events
		/// </summary>
		/// <param name="results">List of objects from the Smart Selection</param>
		/// <param name="ex"></param>
		public delegate void Exit(List<object> results, Exception ex);

		/// <summary>
		/// Delegate void for finish of selection event(s)
		/// </summary>
		/// <param name="results">List of objects from the Smart Selection</param>
		public delegate void Finish(List<object> results);

		/// <summary>
		/// Reset the Selection exit event, only last defined void will be linked!
		/// </summary>
		public static event Exit OnExit { add { ExitDelegate = value; } remove { ExitDelegate = null; } }

		/// <summary>
		/// Reset the Selection finished event, only last defined void will be linked!
		/// </summary>
		public static event Finish OnFinish { add { FinishDelegate = value; } remove { FinishDelegate = null; } }

		/// <summary>
		/// Events that will be fired onExit
		/// </summary>
		private static event Exit ExitDelegate;

		/// <summary>
		/// Events that will be fired onFinish
		/// </summary>
		private static event Finish FinishDelegate;

		/// <summary>
		/// Add a new Selection to the list
		/// </summary>
		/// <param name="sType">Data Selection Type</param>
		/// <param name="sMessage">Message to be shown</param>
		[DebuggerStepThrough()]
		public static void Add(SelectionType sType, string sMessage) => selectionList.Add(new SmartSelectionDataSet(sType, sMessage));

		/// <summary>
		/// Reset the SmartSelection dataset
		/// </summary>
		[DebuggerStepThrough()]
		public static void Clear()
		{
			// Delete all results objects
			Result.Clear();
			// Delete all Selection datasets
			selectionList.Clear();
		}

		/// <summary>
		/// Start the (multiple) selection, SystemCallbacks is implemented!
		/// </summary>
		[DebuggerStepThrough()]
		public static void Start() => SystemCallbacks.Call(StartCall);

		/// <summary>
		/// When te routine is stopped call all linked events
		/// </summary>
		[DebuggerStepThrough()]
		private static void FireOnExit(Exception ex)
		{
			// Load handler(s)
			Exit handler = ExitDelegate;
			// Check if handlres are available
			if (handler != null)
			{
				// Call all events
				ExitDelegate(Result, ex);
			}
			// Clear objects
			Clear();
		}

		/// <summary>
		/// When te routine is completed call all linked events
		/// </summary>
		[DebuggerStepThrough()]
		private static void FireOnFinish()
		{
			// Load handler(s)
			Finish handler = FinishDelegate;
			// Check if handlres are available
			if (handler != null)
			{
				// Call all events
				FinishDelegate(Result);
			}
			// Clear objects
			Clear();
		}

		/// <summary>
		/// Used for continues Async selection events
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		[DebuggerStepThrough()]
		private static void SelectionEvent(object sender, SelectionEventArgs e)
		{
			// Unlink current method
			Selection.SelectionEvent -= SelectionEvent;
			try
			{
				// Check for results
				if (e.Result.Count() > 0)
				{
					if (e.Result.First().GetType() == typeof(PointOptionImpl))
					{
						PointOptionImpl tmp = (PointOptionImpl)e.Result.First();
						if (tmp.Empty == true || tmp.Exists == false)
						{
							FireOnExit(new Exception(CancelMessage));
							return;
						}
					}

					// Add currents result(s) to the result list
					Result.Add(e.Result.First());
					// Remove the first selection
					selectionList.Remove(selectionList.First());
					// Check if any selections are required
					if (selectionList.Count() > 0)
					{
						// Link method
						Selection.SelectionEvent += SelectionEvent;
						// Start selection with provided selection data
						Selection.StartSelection(selectionList.First().SelectType, selectionList.First().Message);
					}
					else
						FireOnFinish();
				}
				else
				{
					throw new Exception("No results available in the SelectionEvent results");
				}
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				FireOnExit(ex);
			}
		}

		/// <summary>
		/// Start in selection within 'SystemCallbacks'
		/// </summary>
		[DebuggerStepThrough()]
		private static void StartCall()
		{
			// Check if any selections have been defined
			if (selectionList.Count() > 0)
			{
				// Link method
				Selection.SelectionEvent += SelectionEvent;
				// Start selection with provided selection data
				SmartSelectionDataSet selection = selectionList.First();
				Selection.StartSelection(selection.SelectType, selection.Message);
			}
		}

		/// <summary>
		/// Dataset for user defined selection(s)
		/// </summary>
		private class SmartSelectionDataSet
		{
			/// <summary>
			/// Creates a new dataset for the smart selection
			/// </summary>
			/// <param name="selectType">Data Selection Type</param>
			/// <param name="message">Message to be shown</param>
			public SmartSelectionDataSet(SelectionType selectType, string message)
			{
				SelectType = selectType;
				Message = message;
			}

			/// <summary>
			/// Get the defined message
			/// </summary>
			public string Message { get; }

			/// <summary>
			/// Get the defined selection type
			/// </summary>
			public SelectionType SelectType { get; }
		}
	}
}