using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.MassUnits
{
    public class Kilogram : IMassUnit
    {
        public string ShortName => "kg";

        public string LongName => "Kilograms";

        public double ConversionFactor => 1d;
    }
}
