using System;
using System.Collections.Generic;
using System.Text;
using OilfieldCalc2.Models.MeasureableUnit;

namespace OilfieldCalc2.Models
{
    public interface ITubular
    {
        int ItemId { get; set; }
        int ItemSortOrder { get; set; }
        string ItemDescription { get; set; }

        Measurement InsideDiameter { get; set; }

        double InternalCapacityPerUnit { get; }
        double TotalInternalCapacity { get; }
    }
}
