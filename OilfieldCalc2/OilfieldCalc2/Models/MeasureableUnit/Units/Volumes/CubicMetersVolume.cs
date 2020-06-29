using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.Volumes
{
    [Table("UnitOfMeasure")]
    public class CubicMetersVolume : Volume
    {
        public override string UnitName => "Cubic Meters";

        public override string Abreviation => "m3";

        public override double ConversionFactor => 1;
    }
}
