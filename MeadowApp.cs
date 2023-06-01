using Meadow;
using Meadow.Devices;
using Meadow.Hardware;
using Meadow.Units;
using System.Threading.Tasks;

namespace MeadowApp
{
    // Change F7FeatherV2 to F7FeatherV1 for V1.x boards
    public class MeadowApp : App<F7FeatherV2>
	{
		private ISpiBus spiDevice;
		private IDigitalOutputPort chipSelect;

		public override Task Run()
		{
			Resolver.Log.Info("Run...");

			spiDevice.Write(chipSelect, new byte[] { 0x88, 0x8C, 0xC8, 0xCC });
			spiDevice.Write(chipSelect, new byte[] { 0b0101_0101, 0b1010_1010, 0b0101_0101, 0b1010_1010 });
			spiDevice.Write(chipSelect, new byte[] { 0b0100_0000, 0b0110_0000, 0b0111_0000, 0b0111_1000 });

			return base.Run();
		}

		public override Task Initialize()
		{
			Resolver.Log.Info("Initialize...");

			spiDevice = Device.CreateSpiBus(new Frequency(3.2, Frequency.UnitType.Megahertz));

			return base.Initialize();
		}
	}
}
