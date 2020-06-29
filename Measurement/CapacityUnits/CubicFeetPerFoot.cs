using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.CapacityUnits
{
    public class CubicFeetPerFoot : ICapacityUnit
    {        
        public string ShortName => "ft3/ft";

        public string LongName => "Cubic Feet Per Foot";

        public double ConversionFactor => 10.7639d;
    }
}
