using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.Capacities
{
    [Table("UnitOfMeasure")]
    public class FluidBarrelsPerFootCapacity : Capacity
    {
       public override string UnitName => "Fluid Barrels Per Foot";

        public override string Abreviation => "bbl/ft";

        public override double ConversionFactor => 2.5561791;
    }
}
