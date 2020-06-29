using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.ShortLengths
{
    [Table("UnitOfMeasure")]
    public class MillimeterShortLength : ShortLength
    {
        public override string UnitName => "Millimeter";

        public override string Abreviation => "mm";

        public override double ConversionFactor => 1;
    }
}
