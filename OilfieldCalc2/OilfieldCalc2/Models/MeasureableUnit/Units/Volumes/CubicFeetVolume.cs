using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.Volumes
{
    [Table("UnitOfMeasure")]
    public class CubicFeetVolume : Volume
    {
        public override string UnitName => "Cubic Feet";

        public override string Abreviation => "ft3";

        public override double ConversionFactor => 35.3146667;
    }
}
