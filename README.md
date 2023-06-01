# WS2812 LEDs Using SPI on the Meadow Labs Feather

## Timing

|Value    |Minimum|Nominal|Maximum|
|---------|-------|-------|-------|
|T0H      |200 ns |350 ns |500 ns |
|T1H      |550 ns |700 ns |850 ns |
|T0L      |650 ns |800 ns |950 ns |
|T1L      |450 ns |600 ns |750 ns |
|T0H + T0L|650 ns |1250 ns|1850 ns|
|T1H + T1L|650 ns |1250 ns|1850 ns|
|RESET    |50 µs  |-      |-      |

## Current Concerns

* Configured frequency (3.2 MHz) doesn't match actual frequency (3 MHz). This
  is likely because of a mismatch between the master clock speed and the
  closest divisor.
* After calls to `ISpiBus.Write`, the MOSI line is sometimes high, sometimes
  low. This isn't going to work with my current thinking -- it needs to be low.

## References

[WS2812 Datasheet](https://cdn-shop.adafruit.com/datasheets/WS2812.pdf) (courtesy of Adafruit)
