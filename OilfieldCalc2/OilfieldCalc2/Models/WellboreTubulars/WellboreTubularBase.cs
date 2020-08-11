using System;
using System.Collections.Generic;
using System.Text;
using OilfieldCalc2.Helpers;
using SQLiteNetExtensions.Attributes;
using OilfieldCalc2.Models.MeasureableUnit;
using SQLite;
using Xamarin.Essentials;
using OilfieldCalc2.Services;

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

        [TextBlob("StartBlobbed")]
        public Measurement StartDepth { get; set; }
        [TextBlob("EndBlobbed")]
        public Measurement EndDepth { get; set; }
        [TextBlob("IDBlobbed")]
        public Measurement InsideDiameter { get; set; }

#nullable enable
        public string? StartBlobbed { get; set; } //serialized Lengths
        public string? EndBlobbed { get; set; } //serialized InsideDiameters
        public string? IDBlobbed { get; set; } //serialized Outside Diameters
#nullable disable

        [Ignore]
        public double Length => this.EndDepth.Value - this.StartDepth.Value;
        [Ignore]
        public double InternalCapacityPerUnit
        {
            get
            {
                try
                {
                    if (this.InsideDiameter != null)
                        return OilfieldCalc2.Helpers.Capacity.GetInternalCapacity(this.InsideDiameter.MetricValue) * MeasurementUnitService.GetCurrentCapacityUnit().ConversionFactor;
                    else
                        return -1;
                }
                catch (ArgumentException e)
                {
                    throw new System.ArgumentException("Database is corrupt", e);
                }
            }
        }
        [Ignore]
        public double TotalInternalCapacity
        {
            get
            {
                try
                {
                    if (this.InsideDiameter != null && this.StartDepth != null && this.EndDepth != null)
                        return OilfieldCalc2.Helpers.Capacity.GetInternalCapacity(this.InsideDiameter.MetricValue) * MeasurementUnitService.GetCurrentVolumeUnit().ConversionFactor * this.Length;
                    else
                        return -1;
                }
                catch (ArgumentException e)
                {
                    throw new System.ArgumentException("Database is corrupt", e);
                }
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            IWellboreTubular otherTubular = obj as IWellboreTubular;

            if (otherTubular != null)
                return this.ItemSortOrder.CompareTo(otherTubular.ItemSortOrder);
            else
                throw new ArgumentException("Object is not IWellboreTubular");
        }

        public override string ToString()
        {
            //Get unit from persistant storage
            string smallLengthUnit = Preferences.Get("smallLengthUnit", "mm");

            return this.InsideDiameter + smallLengthUnit + " ID " + this.ItemDescription;
        }
    }
}
