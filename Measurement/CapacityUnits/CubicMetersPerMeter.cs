using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement.CapacityUnits
{
    public class CubicMetersPerMeter : ICapacityUnit
    {
        public MeasurementType Type => MeasurementType.Capacity;

        public string ShortName => "m3/m";

        public string LongName => "Cubic Meters Per Meter";

        public double ConversionFactor => 1d;
    }
}
