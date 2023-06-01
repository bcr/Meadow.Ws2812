using Meadow;
using Meadow.Devices;
using System.Threading.Tasks;

namespace MeadowApp
{
    // Change F7FeatherV2 to F7FeatherV1 for V1.x boards
    public class MeadowApp : App<F7FeatherV2>
	{
		public override Task Run()
		{
			Resolver.Log.Info("Run...");

			// Add code here

			return base.Run();
		}

		public override Task Initialize()
		{
			Resolver.Log.Info("Initialize...");

			// Add code here

			return base.Initialize();
		}
	}
}
