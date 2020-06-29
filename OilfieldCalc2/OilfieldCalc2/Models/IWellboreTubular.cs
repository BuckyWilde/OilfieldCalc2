using OilfieldCalc2.Models.WellboreTubulars;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models
{
    public interface IWellboreTubular : ITubular
    {
        WellboreTubularType TubularType { get; set; }
        double StartDepth { get; set; }
        double EndDepth { get; set; }
        double SectionLength { get; }
    }
}
