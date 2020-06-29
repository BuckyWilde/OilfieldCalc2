using OilfieldCalc2.Models.DrillstringTubulars;
using System;
using System.Collections.Generic;
using System.Text;
using OilfieldCalc2.Models.MeasureableUnit;

namespace OilfieldCalc2.Models
{
    public interface IDrillstringTubular : ITubular
    {
        DrillstringTubularType TubularType { get; set; }
        Measurement Length { get; set; }
        Measurement OutsideDiameter { get; set; }

        Measurement AdjustedWeightPerUnit { get; set; }
        double TotalWeight { get; }

        double DryDisplacementPerUnit { get; }
        double WetDisplacementPerUnit { get; }
        double TotalDryDisplacement { get; }
        double TotalWetDispalcement { get; }
    }
}
