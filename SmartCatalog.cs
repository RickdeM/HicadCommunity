using ISD.BaseTypes;
using ISD.CAD.PartSrv;
using ISD.CAD.PartSrv.Queries;
using RDM.HicadCommunity.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RDM.HicadCommunity
{
	/// <summary>
	/// Hicad Catalog handler
	/// </summary>
	public static class SmartCatalog
	{
		/// <summary>
		/// Get all columns from a Table
		/// </summary>
		/// <param name="si">StandardItem which contains the standardId</param>
		/// <returns></returns>
		public static DataColumnCollection GetColumns(StandardItem si) => CatPartSrv.GetTable(si.StandardId).GetData().Columns;

		/// <summary>
		/// Get all columns from a Table
		/// </summary>
		/// <param name="standardId">Catalog standardId</param>
		/// <returns></returns>
		public static DataColumnCollection GetColumns(int standardId) => CatPartSrv.GetTable(standardId).GetData().Columns;

		/// <summary>
		/// Get all columns from a Table
		/// </summary>
		/// <param name="tableName">Catalog TableName</param>
		/// <returns></returns>
		public static DataColumnCollection GetColumns(string tableName) => CatPartSrv.GetTable(tableName).GetData().Columns;

		/// <summary>
		/// Get a Table record
		/// </summary>
		/// <typeparam name="T">Generic Type return</typeparam>
		/// <param name="standardId">StandardItem standardId for the requested TableRow</param>
		/// <param name="itemId">StandardItem itemId for the requested TableRow</param>
		/// <returns></returns>
		public static T GetRow<T>(int standardId, int itemId) where T : ICatalogeRow => GetRow<T>(standardId, itemId);

		/// <summary>
		/// Get a Table record
		/// </summary>
		/// <typeparam name="T">Generic Type return</typeparam>
		/// <param name="si">StandardItem for the requested TableRow</param>
		/// <returns></returns>
		public static T GetRow<T>(this StandardItem si) where T : ICatalogeRow
		{
			try
			{
				// Create the result
				T result = (T)Activator.CreateInstance(typeof(T));
				// Set the data
				result.SetData(si, LoadRow(si));
				// Return the result
				return result;
			}
			catch (Exception ex)
			{
				FileLogger.Log(ex);
				return default;
			}
		}

		/// <summary>
		/// Get a Table record
		/// </summary>
		/// <param name="standardId">StandardItem standardId for the requested TableRow</param>
		/// <param name="itemId">StandardItem itemId for the requested TableRow</param>
		/// <returns></returns>
		public static Dictionary<string, object> GetRow(int standardId, int itemId) => LoadRow(standardId, itemId);

		/// <summary>
		/// Get a Table record
		/// </summary>
		/// <param name="si">StandardItem for the requested TableRow</param>
		/// <returns></returns>
		public static Dictionary<string, object> GetRow(this StandardItem si) => LoadRow(si);

		/// <summary>
		/// Get all Table records
		/// </summary>
		/// <typeparam name="T">Generic Type return</typeparam>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public static List<T> GetRows<T>(string tableName) where T : ICatalogeRow
		{
			List<T> objList = new List<T>();
			try
			{
				int id = CatPartSrv.GetTable(tableName).Id;
				foreach (Dictionary<string, object> row in GetRows(tableName))
				{
					T instance = (T)Activator.CreateInstance(typeof(T));
					int int32 = Convert.ToInt32(row["ID"]);
					instance.SetData(new StandardItem(id, int32), row);
					objList.Add(instance);
				}
			}
			catch (Exception ex)
			{
				FileLogger.Log(ex);
			}
			return objList;
		}

		/// <summary>
		/// Get all Table records
		/// </summary>
		/// <param name="tableName">Name of the Table</param>
		/// <param name="find">KeyValues which the row must match to</param>
		/// <returns></returns>
		public static List<Dictionary<string, object>> GetRows(string tableName, Dictionary<string, object> find = null) => LoadRows(tableName, find);

		/// <summary>
		/// Get all Table records
		/// </summary>
		/// <param name="standardId">Table ID</param>
		/// <param name="find">KeyValues which the row must match to</param>
		/// <returns></returns>
		public static List<Dictionary<string, object>> GetRows(int standardId, Dictionary<string, object> find = null) => LoadRows(standardId, find);

		/// <summary>
		/// Get all Table records
		/// </summary>
		/// <param name="si">StandardItem which contains the StandardID</param>
		/// <param name="find">KeyValues which the row must match to</param>
		/// <returns></returns>
		public static List<Dictionary<string, object>> GetRows(StandardItem si, Dictionary<string, object> find = null) => LoadRows(si.StandardId, find);

		/// <summary>
		/// Get the DisplayName of a table
		/// </summary>
		/// <param name="si">StandardItem which contains the standardId</param>
		/// <returns></returns>
		public static string GetTableDisplayName(StandardItem si) => CatPartSrv.GetTable(si.StandardId).DisplayName;

		/// <summary>
		/// Get the DisplayName of a table
		/// </summary>
		/// <param name="standardId">Catalog standardId</param>
		/// <returns></returns>
		public static string GetTableDisplayName(int standardId) => CatPartSrv.GetTable(standardId).DisplayName;

		/// <summary>
		/// Get the DisplayName of a table
		/// </summary>
		/// <param name="tableName">Catalog TableName</param>
		/// <returns></returns>
		public static string GetTableDisplayName(string tableName) => CatPartSrv.GetTable(tableName).DisplayName;

		/// <summary>
		/// Updata a CatalogTable record. ON YOUR OWN RISK, SHOULD ONLY BE DONE BY 1 PERSON
		/// </summary>
		/// <param name="si">StandardItem for finding the record</param>
		/// <param name="values"></param>
		public static Dictionary<string, object> InsertRow(string tableName, Dictionary<string, object> values)
		{
			try
			{
				// Create a result set
				Dictionary<string, object> result = new Dictionary<string, object>();
				// Get the TableInfo
				TableInfo table = CatPartSrv.GetTable(tableName);
				// Get the DataTable
				DataTable data = table.GetData();
				// Get the DataRow
				DataRow row = data.NewRow();
				// Make the row Read/Write
				row.BeginEdit();
				// Loop through all columns to be set
				foreach (KeyValuePair<string, object> keyValue in values)
				{
					try
					{
						// Try to update the columns
						row[keyValue.Key] = keyValue.Value;
					}
					catch { }
				}
				// Make the row Read
				row.EndEdit();
				// Add the row to the recordset
				data.Rows.Add(row);
				// Update the table
				table.Update(data);
				// Get the TableColumns
				DataColumnCollection columns = table.GetData().Columns;
				// loop through all columns
				for (int index = 0; index < row.ItemArray.Count(); ++index)
					// Add the Column with its value
					result.Add(columns[index].ColumnName, row.ItemArray[index]);
				// Return the result
				return result;
			}
			catch (Exception ex)
			{
				FileLogger.Log(ex);
				throw;
			}
		}

		public static void Reload() => catPartSrv = null;

		/// <summary>
		/// Updata a CatalogTable record. ON YOUR OWN RISK, SHOULD ONLY BE DONE BY 1 PERSON
		/// </summary>
		/// <param name="si">StandardItem for finding the record</param>
		/// <param name="keyValues"></param>
		public static bool UpdateRow(StandardItem si, Dictionary<string, object> values)
		{
			try
			{
				// Get the TableInfo
				TableInfo table = CatPartSrv.GetTable(si.StandardId);
				// Get the DataTable
				DataTable data = table.GetData();
				// Get the DataRow
				DataRow row = data.GetDataRow(si.ItemId);
				// Make the row Read/Write
				row.BeginEdit();
				// Loop through all columns to be updated
				foreach (KeyValuePair<string, object> keyValue in values)
				{
					try
					{
						// Try to update the columns
						row[keyValue.Key] = keyValue.Value;
					}
					catch { }
				}
				// Make the row Read
				row.EndEdit();
				// Update the row
				table.Update(data);
				return true;
			}
			catch (Exception ex)
			{
				FileLogger.Log(ex);
				return false;
			}
		}

		#region Catalog handler

		private static PartSrv catPartSrv;

		/// <summary>
		/// The Cataloge Partserver handler
		/// </summary>
		private static PartSrv CatPartSrv => catPartSrv ?? (catPartSrv = new PartSrv());

		/// <summary>
		/// Load a single Catalog table row
		/// </summary>
		/// <param name="si">StandardItem which contains the standardId</param>
		/// <returns></returns>
		private static Dictionary<string, object> LoadRow(StandardItem si) => LoadRow(CatPartSrv.GetTable(si.StandardId), si.ItemId);

		/// <summary>
		/// Load a single Catalog table row
		/// </summary>
		/// <param name="standardId">Catalog TableID</param>
		/// <param name="itemId">Catalog ItemID</param>
		/// <returns></returns>
		private static Dictionary<string, object> LoadRow(int standardId, int itemId) => LoadRow(CatPartSrv.GetTable(standardId), itemId);

		/// <summary>
		/// Load a single Catalog table row
		/// </summary>
		/// <param name="tableInfo">Catalog Table</param>
		/// <param name="itemID"></param>
		/// <returns></returns>
		private static Dictionary<string, object> LoadRow(TableInfo tableInfo, int itemID)
		{
			try
			{
				// Create result
				Dictionary<string, object> result = new Dictionary<string, object>();
				// Get the Table columns
				DataColumnCollection columns = tableInfo.GetData().Columns;
				// Find the requested row
				DataRow dataRow = tableInfo.GetData().Rows.Find(itemID);
				// loop through all columns
				for (int index = 0; index < dataRow.ItemArray.Count(); ++index)
					// Add the Column with its value
					result.Add(columns[index].ColumnName, dataRow.ItemArray[index]);
				// return the result
				return result;
			}
			catch (Exception ex)
			{
				FileLogger.Log(ex);
				throw;
			}
		}

		/// <summary>
		/// Load all Catalog table rows
		/// </summary>
		/// <param name="standardId">Catalog Table ID</param>
		/// <param name="find">KeyValues which the row must match to</param>
		/// <returns></returns>
		private static List<Dictionary<string, object>> LoadRows(int standardId, Dictionary<string, object> find = null) => LoadRows(CatPartSrv.GetTable(standardId), find);

		/// <summary>
		/// Load all Catalog table rows
		/// </summary>
		/// <param name="tableName">Catalog tableName</param>
		/// <param name="find">KeyValues which the row must match to</param>
		/// <returns></returns>
		private static List<Dictionary<string, object>> LoadRows(string tableName, Dictionary<string, object> find = null) => LoadRows(CatPartSrv.GetTable(tableName), find);

		/// <summary>
		/// Load all Catalog table rows
		/// </summary>
		/// <param name="tableInfo">Catalog Table</param>
		/// <param name="find">KeyValues which the row must match to</param>
		/// <returns></returns>
		private static List<Dictionary<string, object>> LoadRows(TableInfo tableInfo, Dictionary<string, object> find = null)
		{
			try
			{
				// Create result
				List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
				// Get the Table columns
				DataColumnCollection columns = tableInfo.GetData().Columns;
				// Loop through all Table rows
				foreach (DataRow row in (InternalDataCollectionBase)tableInfo.GetData().Rows)
				{
					// Create result record
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					// Loop through all columns
					for (int index = 0; index < row.ItemArray.Count(); ++index)
						// Add the Column with its value
						dictionary.Add(columns[index].ColumnName, row.ItemArray[index]);
					// Check if the records must match column(s) value(s)
					if (find == null)
					{
						// Add the record
						dictionaryList.Add(dictionary);
						continue;
					}
					// Check if all values match the find records
					if (find.All(x => dictionary[x.Key].ToString() == x.Value.ToString()))
						dictionaryList.Add(dictionary);
				}
				// return the result
				return dictionaryList;
			}
			catch (Exception ex)
			{
				FileLogger.Log(ex);
				throw;
			}
		}

		#endregion Catalog handler
	}
}