using ISD.BaseTypes;
using System.Collections.Generic;

namespace RDM.HicadCommunity.Interfaces
{
	/// <summary>
	/// Cataloge object required interface
	/// </summary>
	public interface ICatalogeRow
	{
		/// <summary>
		/// Set the Data from the catalog
		/// </summary>
		/// <param name="si">Record StandardItem</param>
		/// <param name="CatData">Catalog data</param>
		void SetData(StandardItem si, Dictionary<string, object> CatData);
	}
}