using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.ShortLengthUnits
{
    public class Millimeter : IShortLengthUnit
    {
        public string ShortName => "mm";
        public string LongName => "millimeter";
        public double ConversionFactor => 1d;
    }
}
