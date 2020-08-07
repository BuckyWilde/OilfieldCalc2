using System;
using SQLite;
using SQLiteNetExtensions.Attributes;
using Xamarin.Essentials;
using OilfieldCalc2.Models.MeasureableUnit;
using OilfieldCalc2.Services;

namespace OilfieldCalc2.Models.DrillstringTubulars
{
    [Table("Drillstring")]
    public class DrillstringTubularBase : IDrillstringTubular, IComparable
    {
        [PrimaryKey, AutoIncrement]
        public int ItemId { get; set; }
        public int ItemSortOrder { get; set; }
        public virtual string ItemDescription { get; set; }
        public virtual DrillstringTubularType TubularType { get; set; }

        [TextBlob("LengthBlobbed")]
        public Measurement Length { get; set; }

        [TextBlob("IDBlobbed")]
        public Measurement InsideDiameter { get; set; }

        [TextBlob("ODBlobbed")]
        public Measurement OutsideDiameter { get; set; }

        [TextBlob("WeightBlobbed")]
        public Measurement AdjustedWeightPerUnit { get; set; }

#nullable enable
        public string? LengthBlobbed { get; set; } //serialized Lengths
        public string? IDBlobbed { get; set; } //serialized InsideDiameters
        public string? ODBlobbed { get; set; } //serialized Outside Diameters
        public string? WeightBlobbed { get; set; } //serialized Adjusted Weight
#nullable disable

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
                    if (this.InsideDiameter != null && this.Length != null)
                        return OilfieldCalc2.Helpers.Capacity.GetInternalCapacity(this.InsideDiameter.MetricValue) * MeasurementUnitService.GetCurrentVolumeUnit().ConversionFactor * this.Length.Value;
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
        public double TotalWeight
        {
            get
            {
                try
                {
                    if (this.AdjustedWeightPerUnit != null && this.Length != null)
                        return this.AdjustedWeightPerUnit.MetricValue * this.Length.MetricValue * MeasurementUnitService.GetCurrentMassUnit().ConversionFactor;
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
        public double DryDisplacementPerUnit
        {
            get
            {
                try
                {


                    if (this.OutsideDiameter != null && this.InsideDiameter != null)
                        return OilfieldCalc2.Helpers.Capacity.GetDryDisplacement(this.OutsideDiameter.MetricValue, this.InsideDiameter.MetricValue) * MeasurementUnitService.GetCurrentCapacityUnit().ConversionFactor;
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
        public double TotalDryDisplacement
        {
            get
            {
                try
                {
                    if (this.OutsideDiameter != null && this.InsideDiameter != null && this.Length != null)
                        return OilfieldCalc2.Helpers.Capacity.GetDryDisplacement(this.OutsideDiameter.MetricValue, this.InsideDiameter.MetricValue) * MeasurementUnitService.GetCurrentVolumeUnit().ConversionFactor * this.Length.Value;
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
        public double WetDisplacementPerUnit
        {
            get
            {
                try
                {
                    if (this.OutsideDiameter != null)
                        return OilfieldCalc2.Helpers.Capacity.GetWetDisplacement(this.OutsideDiameter.MetricValue) * MeasurementUnitService.GetCurrentCapacityUnit().ConversionFactor;
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
        public double TotalWetDispalcement
        {
            get
            {
                try
                {
                    if (this.OutsideDiameter != null)
                        return OilfieldCalc2.Helpers.Capacity.GetWetDisplacement(this.OutsideDiameter.MetricValue) * MeasurementUnitService.GetCurrentVolumeUnit().ConversionFactor * Length.Value;
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

            IDrillstringTubular otherTubular = obj as IDrillstringTubular;

            if (otherTubular != null)
                return this.ItemSortOrder.CompareTo(otherTubular.ItemSortOrder);
            else
                throw new ArgumentException("Object is not IDrillstringTubular");
        }

        public override string ToString()
        {
            //Get unit from persistant storage
            string smallLengthUnit = Preferences.Get("smallLengthUnit", "mm");

            return this.OutsideDiameter + smallLengthUnit + " OD " + this.ItemDescription;
        }
    }
}
