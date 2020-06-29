using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.WellboreTubulars
{
    [Table("Wellbore")]
    public class OpenHole : WellboreTubularBase
    {
        public override string ItemDescription => "Open Hole";

        public override WellboreTubularType TubularType => WellboreTubularType.OpenHole;
    }
}
