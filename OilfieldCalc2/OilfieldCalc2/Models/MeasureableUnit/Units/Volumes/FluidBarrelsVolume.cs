using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.Volumes
{
    [Table("UnitOfMeasure")]
    public class FluidBarrelsVolume : Volume
    {
        public override string UnitName => "Fluid Barrels";

        public override string Abreviation => "bbl";

        public override double ConversionFactor => 8.38641436;
    }
}
