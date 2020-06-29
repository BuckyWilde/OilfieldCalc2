using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.LongLengthUnits
{
    public class Feet : ILongLengthUnit
    {
        public string ShortName => "ft";

        public string LongName => "Feet";

        public double ConversionFactor => 3.280839895d;
    }
}
