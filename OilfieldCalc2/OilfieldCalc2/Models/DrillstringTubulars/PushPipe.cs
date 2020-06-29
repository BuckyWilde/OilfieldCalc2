using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.DrillstringTubulars
{
    [Table("Drillstring")]
    public class PushPipe : DrillstringTubularBase
    {
        public override string ItemDescription => "Push Pipe";

        public override DrillstringTubularType TubularType => DrillstringTubularType.PushPipe;
    }
}
