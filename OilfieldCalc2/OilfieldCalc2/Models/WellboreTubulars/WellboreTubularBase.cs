using System;
using System.Collections.Generic;
using System.Text;
using OilfieldCalc2.Helpers;
using OilfieldCalc2.Models.MeasureableUnit;
using SQLite;
using Xamarin.Essentials;

namespace OilfieldCalc2.Models.WellboreTubulars
{
    [Table("Wellbore")]
    public class WellboreTubularBase : IWellboreTubular
    {
        [PrimaryKey, AutoIncrement]
        public int ItemId { get; set; }
        public int ItemSortOrder { get; set; }
        public virtual string ItemDescription { get; set; }
        public virtual WellboreTubularType TubularType { get; set; }

        public double StartDepth { get; set; }
        public double EndDepth { get; set; }

        [Ignore]
        public Measurement InsideDiameter { get; set; }

        [Ignore]
        public double SectionLength => this.EndDepth - this.StartDepth;
        [Ignore]
        public double InternalCapacityPerUnit => OilfieldCalc2.Helpers.Capacity.GetInternalCapacity(this.InsideDiameter.MetricValue)*this.InsideDiameter.Unit.ConversionFactor;
        [Ignore]
        public double TotalInternalCapacity => this.SectionLength * this.InternalCapacityPerUnit;

        public override string ToString()
        {
            //Get unit from persistant storage
            string smallLengthUnit = Preferences.Get("smallLengthUnit", "mm");

            return this.InsideDiameter + smallLengthUnit + " ID " + this.ItemDescription;
        }
    }
}
