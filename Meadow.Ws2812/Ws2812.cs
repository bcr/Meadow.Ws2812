using System;
using System.Collections.Generic;
using Meadow.Foundation;
using Meadow.Hardware;
using System.Linq;

namespace MeadowApp
{
    public class Ws2812
    {
        private ISpiBus _spiBus;
        private IDigitalOutputPort _chipSelect;
        private byte[] _transmitBuffer;

        private const int _bytesPerColorPart = 4;

        private static readonly byte[] ws2812Bytes = new byte[] { 0x44, 0x46, 0x64, 0x66 };

        public int LedCount { get; internal set; }

        private static IEnumerable<byte> ByteToWs2812Byte(byte theByte)
        {
            for (int counter = 0;counter < 4;++counter)
            {
                yield return ws2812Bytes[(theByte & 0b1100_0000) >> 6];
                theByte <<= 2;
            }
        }

        public void Clear()
        {
            // prepare the transmit buffer to send all "0"s turning all LEDs off
            _transmitBuffer = Enumerable.Repeat(ws2812Bytes[0], _transmitBuffer.Length).ToArray();
        }

        public void ClearLed(int ledIndex)
        {
            // 4 bytes per color and 3 colors
            int start = ledIndex * _bytesPerColorPart * 3;
            int end = start + _bytesPerColorPart * 3;

            for(var i = start; i < end; i++)
            {
                _transmitBuffer[i] = ws2812Bytes[0];
            }
        }

        public void SetColors(IEnumerable<Color> colors)
        {
            int position = 0;

            foreach (var color in colors.Take(LedCount))
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

        public Ws2812(ISpiBus spiBus, int ledCount, IDigitalOutputPort chipSelect = null)
        {
            _spiBus = spiBus;
            LedCount = ledCount;
            _chipSelect = chipSelect;
            // To transmit 8 bits of color we need 4 bytes and there are 3 colors
            _transmitBuffer = new byte[ledCount * _bytesPerColorPart * 3];
        }
    }
}
