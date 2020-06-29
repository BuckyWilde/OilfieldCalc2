using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.Volumes
{
    [Table("UnitOfMeasure")]
    public class OilBarrelsVolume : Volume
    {
        public override string UnitName => "Oil Barrels";

        public override string Abreviation => "bbl";

        public override double ConversionFactor => 6.28981077;
    }
}
