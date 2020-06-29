using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.VolumeUnits
{
    public class CubicMeter : IVolumeUnit
    {
        public string ShortName => "m3";

        public string LongName => "Cubic Meters";

        public double ConversionFactor => 1d;
    }
}
