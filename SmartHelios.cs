using Helios.API;
using ISD.PDM.API;
using ISD.Utilities;
using static ISD.CAD.Data.PDMNodeExtensions;
using CDATA = ISD.CAD.Data;

namespace RDM.HicadCommunity
{
	public static class SmartHelios
	{
		private static readonly ISessionProvider sessionProvider;
		private static DocumentHandler document;
		private static PartHandler part;
		private static ProjectHandler project;

		static SmartHelios()
		{
			AssemblyInit.InitFactories();
			sessionProvider = GenericFactory<ISessionProvider>.Create("SessionProvider");
		}

		public static DocumentHandler Document => document ?? (document = new DocumentHandler());
		public static PartHandler Part => part ?? (part = new PartHandler());
		public static ProjectHandler Project => project ?? (project = new ProjectHandler());
		public static Session Session => sessionProvider.ActiveSession;

		public class DocumentHandler : Entityimpl<Document>
		{
		}

		public abstract class Entityimpl<T> where T : IDataEntity
		{
			public T IdentifierQuery(string identified)
			{
				IdentifierQuery<T> pq = new IdentifierQuery<T>(identified);
				Session.Apply(pq);
				return pq.Result;
			}
		}

		public class PartHandler : Entityimpl<Part>
		{
		}

		public class ProjectHandler : Entityimpl<Project>
		{
		}

		public static Part QueryPart(this CDATA.Node n) => n.QueryPart(Session);

		public static Document QueryDocument(this CDATA.Node n) => n.QueryDocument(Session);
	}
}