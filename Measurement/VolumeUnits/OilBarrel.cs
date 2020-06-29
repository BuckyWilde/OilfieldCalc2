using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.VolumeUnits
{
    public class OilBarrel : IVolumeUnit
    {
        public string ShortName => "bbl";

        public string LongName => "Oil Barrels";

        public double ConversionFactor => 6.28981077d;
    }
}
