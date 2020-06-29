using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.LongLengthUnits
{
    public class Meter : ILongLengthUnit
    {
        public string ShortName => "m";

        public string LongName => "Meter";

        public double ConversionFactor => 1d;
    }
}
