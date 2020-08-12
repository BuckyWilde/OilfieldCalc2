using OilfieldCalc2.Models.MeasureableUnit;
using OilfieldCalc2.Models.WellboreTubulars;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models
{
    public interface IWellboreTubular : ITubular
    {
        WellboreTubularType TubularType { get; set; }
        Measurement StartDepth { get; set; }
        Measurement EndDepth { get; set; }
        double Length { get; }
        int WashoutFactor { get; set; }
    }
}
