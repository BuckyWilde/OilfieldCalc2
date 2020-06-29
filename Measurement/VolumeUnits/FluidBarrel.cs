using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.VolumeUnits
{
    public class FluidBarrel : IVolumeUnit
    {
        public string ShortName => "bbl";

        public string LongName => "Fluid Barrels";

        public double ConversionFactor => 8.38641436d;
    }
}
