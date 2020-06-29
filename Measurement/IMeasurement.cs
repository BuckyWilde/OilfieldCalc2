using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement
{
    /// <summary>
    /// IMeasurement interface. CurrentValue is based on the metric value with a conversion value factored in.
    /// MetricValue will always be the same. The conversion factor is in the unit.
    /// </summary>
    public interface IMeasurement
    {
        double CurrentValue { get; set; }

        double MetricValue { get;}

        IUnit Unit { get; set; }
    }
}
