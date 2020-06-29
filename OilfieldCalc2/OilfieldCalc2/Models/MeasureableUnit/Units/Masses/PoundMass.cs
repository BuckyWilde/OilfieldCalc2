using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.Masses
{
    [Table("UnitOfMeasure")]
    public class PoundMass : Mass
    {
        public override string UnitName => "Pounds";

        public override string Abreviation => "lbs";

        public override double ConversionFactor => 2.204622621848776;
    }
}
