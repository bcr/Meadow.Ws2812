using System.Collections.Generic;
using Meadow.Foundation;
using Meadow.Hardware;

namespace MeadowApp
{
    public class Ws2812
    {
        private ISpiBus _spiBus;
        private IDigitalOutputPort _chipSelect;
        private byte[] _transmitBuffer;

        private static readonly byte[] ws2812Bytes = new byte[] { 0x88, 0x8C, 0xC8, 0xCC };

        private static IEnumerable<byte> ByteToWs2812Byte(byte theByte)
        {
            for (int counter = 0;counter < 4;++counter)
            {
                yield return ws2812Bytes[(theByte & 0b1100_0000) >> 6];
                theByte <<= 2;
            }
        }

        public void SetColors(IEnumerable<Color> colors)
        {
            int position = 0;

            foreach (var color in colors)
            {
                foreach (var theByte in ByteToWs2812Byte(color.G))
                {
                    _transmitBuffer[position++] = theByte;
                }
                foreach (var theByte in ByteToWs2812Byte(color.R))
                {
                    _transmitBuffer[position++] = theByte;
                }
                foreach (var theByte in ByteToWs2812Byte(color.B))
                {
                    _transmitBuffer[position++] = theByte;
                }
            }
        }

        public void Update()
        {
            _spiBus.Write(_chipSelect, _transmitBuffer);
        }

        public Ws2812(ISpiBus spiBus, int ledCount)
        {
            _spiBus = spiBus;
            // To transmit 8 bits of color we need 4 bytes and there are 3 colors
            _transmitBuffer = new byte[ledCount * 4 * 3];
        }
    }
}
