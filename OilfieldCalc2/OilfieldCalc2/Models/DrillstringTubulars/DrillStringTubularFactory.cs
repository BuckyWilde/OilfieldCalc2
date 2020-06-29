using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.DrillstringTubulars
{
    public abstract class DrillstringTubularFactory
    {
        public abstract ITubular Create();
    }
}
