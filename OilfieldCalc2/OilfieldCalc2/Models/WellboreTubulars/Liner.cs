using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.WellboreTubulars
{
    [Table("Wellbore")]
    public class Liner : WellboreTubularBase
    {
        public override string ItemDescription => "Liner";

        public override WellboreTubularType TubularType => WellboreTubularType.Liner;
    }
}
