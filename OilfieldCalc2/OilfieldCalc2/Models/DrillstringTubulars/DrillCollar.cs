using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.DrillstringTubulars 
{
    [Table("Drillstring")]
    public class DrillCollar : DrillstringTubularBase
    {
        public override string ItemDescription => "Drill Collar";

        public override DrillstringTubularType TubularType => DrillstringTubularType.DrillCollar;
    }
}
