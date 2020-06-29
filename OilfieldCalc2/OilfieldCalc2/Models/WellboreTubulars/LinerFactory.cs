using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.WellboreTubulars
{
    public class LinerFactory : WellboreTubularFactory
    {
        public override ITubular Create() => new Liner();
    }
}
