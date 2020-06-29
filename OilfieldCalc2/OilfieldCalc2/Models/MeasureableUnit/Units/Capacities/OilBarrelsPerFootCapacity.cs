using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.Capacities
{
    public class OilBarrelsPerFootCapacity : Capacity
    {
        public override string UnitName => "Oil Barrels Per Foot";

        public override string Abreviation => "bbl/ft";

        public override double ConversionFactor => 1.91713;
    }
}
