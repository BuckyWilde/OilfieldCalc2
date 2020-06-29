using OilfieldCalc2.Models.DrillstringTubulars;
using OilfieldCalc2.Models.WellboreTubulars;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models
{
    public class Tubular
    {
        //Dictionaries of tubular factories
        private readonly Dictionary<DrillstringTubularType, DrillstringTubularFactory> _drillstringTubularfactories;
        private readonly Dictionary<WellboreTubularType, WellboreTubularFactory> _wellboreTubularfactories;

        public Tubular()
        {
            _drillstringTubularfactories = new Dictionary<DrillstringTubularType, DrillstringTubularFactory>();
            _wellboreTubularfactories = new Dictionary<WellboreTubularType, WellboreTubularFactory>();

            //Populate the dictionary factories with each drillstring tubular factory
            //Uses reflection to create instances of the tubular factory based on the tubular type Enum
            foreach(DrillstringTubularType dstType in Enum.GetValues(typeof(DrillstringTubularType)))
            {
                var drillstringTubularFactory= (DrillstringTubularFactory)Activator.CreateInstance(Type.GetType("OilfieldCalc2.Models.DrillstringTubulars." + Enum.GetName(typeof(DrillstringTubularType), dstType) + "Factory"));
                _drillstringTubularfactories.Add(dstType, drillstringTubularFactory);
            }

            foreach (WellboreTubularType wbtType in Enum.GetValues(typeof(WellboreTubularType)))
            {
                var wellboreTubularFactory = (WellboreTubularFactory)Activator.CreateInstance(Type.GetType("OilfieldCalc2.Models.WellboreTubulars." + Enum.GetName(typeof(WellboreTubularType), wbtType) + "Factory"));
                _wellboreTubularfactories.Add(wbtType, wellboreTubularFactory);
            }            
        }

        public ITubular Create(WellboreTubularType wbtType) => _wellboreTubularfactories[wbtType].Create();

        public ITubular Create(DrillstringTubularType dstType) => _drillstringTubularfactories[dstType].Create();
    }
}
