using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.Capacities
{
    [Table("UnitOfMeasure")]
    public class CubicFeetPerFootCapacity : Capacity
    {
        public override string UnitName => "Cubic Feet Per Foot";

        public override string Abreviation => "ft3/ft";

        public override double ConversionFactor => 10.7639;
    }
}
