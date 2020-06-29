using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.CapacityUnits
{
    public class FluidBarrelsPerFoot : ICapacityUnit
    {
        public string ShortName => "bbls/ft";

        public string LongName => "Fluid Barrels Per Foot";

        public double ConversionFactor => 2.5561791d;
    }
}
