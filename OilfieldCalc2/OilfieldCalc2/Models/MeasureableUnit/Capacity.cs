using OilfieldCalc2.Models.MeasureableUnit.Capacities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit
{
    public abstract class Capacity : UnitOfMeasure
    {
        public static CubicFeetPerFootCapacity CubicFeetPerFoot { get { return new CubicFeetPerFootCapacity(); } }
        public static CubicMetersPerMeterCapacity CubicMetersPerMeter { get { return new CubicMetersPerMeterCapacity(); } }
        public static FluidBarrelsPerFootCapacity FluidBarrelsPerFoot { get { return new FluidBarrelsPerFootCapacity(); } }
        public static OilBarrelsPerFootCapacity OilBarrelsPerFoot { get { return new OilBarrelsPerFootCapacity(); } }

        public override string MeasurementType => typeof(Capacity).ToString();
    }
}
