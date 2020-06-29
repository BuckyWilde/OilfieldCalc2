using OilfieldCalc2.Models.MeasureableUnit.LongLengths;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit
{
    [Table("UnitOfMeasure")]
    public abstract class LongLength : UnitOfMeasure
    {
        public static MeterLongLength Meter { get { return new MeterLongLength(); } }
        public static FootLongLength Feet { get { return new FootLongLength(); } }

        public override string MeasurementType => typeof(LongLength).ToString();
    }
}
