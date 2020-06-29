using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.DrillstringTubulars
{
    [Table("Drillstring")]
    public class HeviWateDrillPipe : DrillstringTubularBase
    {
        public override string ItemDescription => "Hevi Wate Drill Pipe";

        public override DrillstringTubularType TubularType => DrillstringTubularType.HeviWateDrillPipe;
    }
}
