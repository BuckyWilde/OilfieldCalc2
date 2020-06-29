using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement
{
    public static class CapacityUnit
    {
        public static IUnit CubicFeetPerFoot => new CapacityUnits.CubicFeetPerFoot();
        public static IUnit CubicMetersPerMeter => new CapacityUnits.CubicMetersPerMeter();
        public static IUnit FluidBarrelsPerFoot => new CapacityUnits.FluidBarrelsPerFoot();
        public static IUnit OilBarrelsPerFoot => new CapacityUnits.OilBarrelsPerFoot();
    }
}
