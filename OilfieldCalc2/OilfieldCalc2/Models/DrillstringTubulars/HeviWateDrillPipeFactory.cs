using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.DrillstringTubulars
{
    public class HeviWateDrillPipeFactory : DrillstringTubularFactory
    {
        public override ITubular Create() => new HeviWateDrillPipe();
    }
}
