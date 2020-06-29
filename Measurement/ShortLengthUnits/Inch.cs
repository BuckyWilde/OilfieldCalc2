using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.ShortLengthUnits
{
    public class Inch : IShortLengthUnit
    {
        public string ShortName => "in";
        public string LongName  => "Inch";
        public double ConversionFactor => 0.03937007874d;
    }
}
