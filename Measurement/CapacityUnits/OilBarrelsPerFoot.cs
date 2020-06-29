using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.CapacityUnits
{
    public class OilBarrelsPerFoot : ICapacityUnit
    {
        public string ShortName => "bbls/ft";

        public string LongName => "Oil Barrels Per Foot";

        public double ConversionFactor => 1.91713;
    }
}
