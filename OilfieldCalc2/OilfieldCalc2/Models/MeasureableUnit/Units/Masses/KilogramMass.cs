using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.Masses
{
    [Table("UnitOfMeasure")]
    public class KilogramMass : Mass
    {
        public override string UnitName => "Kilogram";

        public override string Abreviation => "kg";

        public override double ConversionFactor => 1;
    }
}
