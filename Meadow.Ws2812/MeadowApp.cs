using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Units;
using System.Linq;
using System.Threading.Tasks;

namespace MeadowApp
{
    // Change F7FeatherV2 to F7FeatherV1 for V1.x boards
    public class MeadowApp : App<F7FeatherV2>
	{
        private Ws2812 _ws2812;

        public override Task Run()
		{
			Resolver.Log.Info("Run...");

			_ws2812.SetColors(Enumerable.Repeat(Color.White, 10));
			_ws2812.Update();

			return base.Run();
		}

		public override Task Initialize()
		{
			Resolver.Log.Info("Initialize...");

			_ws2812 = new Ws2812(Device.CreateSpiBus(new Frequency(3.2, Frequency.UnitType.Megahertz)), 10);

			return base.Initialize();
		}
	}
}
