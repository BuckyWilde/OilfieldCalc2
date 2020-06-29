using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.LongLengths
{
    [Table("UnitOfMeasure")]
    public class FootLongLength : LongLength
    {
        public override string UnitName => "feet";

        public override string Abreviation => "ft";

        public override double ConversionFactor => 3.280839895d;
    }
}
