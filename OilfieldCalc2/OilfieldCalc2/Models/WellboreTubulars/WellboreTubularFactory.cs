using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.WellboreTubulars
{
    public abstract class WellboreTubularFactory
    {
        public abstract ITubular Create();
    }
}
