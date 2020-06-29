using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement
{
    /// <summary>
    /// Static class to get MassUnit concrete object
    /// </summary>
    public static class MassUnit
    {
        public static IUnit Kilogram => new MassUnits.Kilogram();
    }
}
