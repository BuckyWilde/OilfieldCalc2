using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.ShortLengths
{
    [Table("UnitOfMeasure")]
    public class InchShortLength : ShortLength
    {
        public override string UnitName => "Inch";

        public override string Abreviation => "in";

        public override double ConversionFactor => 0.03937007874;
    }
}
