using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.WellboreTubulars
{
    [Table("Wellbore")]
    public class Casing : WellboreTubularBase
    {
        public override string ItemDescription => "Casing";

        public override WellboreTubularType TubularType => WellboreTubularType.Casing;
    }
}
