# WS2812 LEDs Using SPI on the Wilderness Labs Meadow

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

## Current Theory

The idea is to use the MOSI signal from the SPI bus to signal the various high
and low periods. Some cheats that might be helpful:

* As long as T0H and T1H are in specification, you may be able to get away with
  T0L and T1L exceeding the maximum (provided that this period is less than the
  minimum for RESET.) This is naughty, but potentially helpful.

## Current Concerns

* Configured frequency (3.2 MHz) doesn't match actual frequency (3 MHz). This
  is likely because of a mismatch between the master clock speed and the
  closest divisor.
* After calls to `ISpiBus.Write`, the MOSI line is sometimes high, sometimes
  low. This isn't going to work with my current thinking -- it needs to be low.
  This is also present in between bytes during the same call to `Write`.

## References

[WS2812 Datasheet](https://cdn-shop.adafruit.com/datasheets/WS2812.pdf) (courtesy of Adafruit)
