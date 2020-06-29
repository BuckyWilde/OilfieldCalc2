using OilfieldCalc2.Models.MeasureableUnit.Volumes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit
{
    public abstract class Volume : UnitOfMeasure
    {
        public static CubicFeetVolume CubicFeet { get { return new CubicFeetVolume(); } }
        public static CubicMetersVolume CubicMeters { get { return new CubicMetersVolume(); } }
        public static OilBarrelsVolume OilBarrels { get { return new OilBarrelsVolume(); } }
        public static FluidBarrelsVolume FluidBarrels { get { return new FluidBarrelsVolume(); } }

        public override string MeasurementType => typeof(Volume).ToString();
    }
}
