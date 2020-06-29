using OilfieldCalc2.Models.MeasureableUnit.Masses;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit
{
    public abstract class Mass : UnitOfMeasure
    {
        public static KilogramMass Kilogram { get { return new KilogramMass(); } }
        public static PoundMass Pound { get { return new PoundMass(); } }

        public override string MeasurementType => typeof(Mass).ToString();
    }
}
