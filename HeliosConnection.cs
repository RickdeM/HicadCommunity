using Helios.API;
using ISD.PDM.API;
using ISD.Utilities;

namespace RDM.HicadCommunity
{
	public static class HeliosConnection
	{
		private static readonly ISessionProvider sessionProvider;

		static HeliosConnection()
		{
			AssemblyInit.InitFactories();
			sessionProvider = GenericFactory<ISessionProvider>.Create("SessionProvider");
		}

		public static Session Session => sessionProvider.ActiveSession;
	}

	public static class Main
	{
	}
}