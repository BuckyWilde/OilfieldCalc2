using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement
{
    /// <summary>
    /// Static class to get VolumeUnit concrete object
    /// </summary>
    public static class VolumeUnit
    {
        public static IUnit CubicFeet => new VolumeUnits.CubicFeet();
        public static IUnit CubicMeter => new VolumeUnits.CubicMeter();
        public static IUnit FluidBarrel => new VolumeUnits.FluidBarrel();
        public static IUnit OilBarrel => new VolumeUnits.OilBarrel();
    }
}
