using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement
{
    /// <summary>
    /// Static class to get ShortLengthUnit concrete object
    /// </summary>
    public static class ShortLengthUnit
    {
        public static IUnit Inch => new ShortLengthUnits.Inch();
        public static IUnit Millimeter => new ShortLengthUnits.Millimeter();
    }
}
