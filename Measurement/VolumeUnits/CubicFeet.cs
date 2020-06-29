using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.VolumeUnits
{
    public class CubicFeet : IVolumeUnit
    {
        public string ShortName => "ft3";

        public string LongName => "Cubic Feet";

        public double ConversionFactor => 35.3146667d;
    }
}
