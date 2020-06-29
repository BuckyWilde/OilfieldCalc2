using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement
{
    /// <summary>
    /// Static class to get LongLengthUnits concrete object
    /// </summary>
    public static class LongLengthUnit
    {
        public static IUnit Feet => new LongLengthUnits.Feet();
        public static IUnit Meter => new LongLengthUnits.Meter();
    }
}
