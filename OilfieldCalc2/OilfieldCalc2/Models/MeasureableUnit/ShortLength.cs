using OilfieldCalc2.Models.MeasureableUnit.ShortLengths;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit
{
    public abstract class ShortLength : UnitOfMeasure
    {
        public static MillimeterShortLength Millimeter { get { return new MillimeterShortLength(); } }
        public static InchShortLength Inch { get { return new InchShortLength(); } }

        public override string MeasurementType => typeof(ShortLength).ToString();
    }
}
