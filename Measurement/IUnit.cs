using System;
using System.Collections.Generic;
using System.Text;

namespace Measurement
{
    public interface IUnit
    {
        string ShortName { get; }           //Abreviation
        string LongName { get; }            //Full name
        double ConversionFactor { get; }    //factor to convert to and from Metric
    }
}
