using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit.LongLengths
{
    [Table("UnitOfMeasure")]
    public class MeterLongLength : LongLength
    {
        public override string UnitName { get => "Meter"; }
        public override string Abreviation { get => "m"; }
        public override double ConversionFactor { get => 1; }
    }
}
