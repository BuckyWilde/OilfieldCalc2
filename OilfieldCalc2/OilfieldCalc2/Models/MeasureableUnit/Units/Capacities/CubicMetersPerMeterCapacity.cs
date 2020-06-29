using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.Capacities
{
    [Table("UnitOfMeasure")]
    public class CubicMetersPerMeterCapacity : Capacity
    {
        public override string UnitName => "Cubic Meters Per Meter";

        public override string Abreviation => "m3/m";

        public override double ConversionFactor => 1;
    }
}
