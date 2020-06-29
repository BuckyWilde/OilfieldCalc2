using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.DrillstringTubulars
{
    [Table("Drillstring")]
    public class DrillPipe : DrillstringTubularBase
    {
        public override string ItemDescription => "Drill Pipe";

        public override DrillstringTubularType TubularType => DrillstringTubularType.DrillPipe;

    }
}
