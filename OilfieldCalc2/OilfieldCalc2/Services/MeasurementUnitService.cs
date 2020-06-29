using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using OilfieldCalc2.Models.MeasureableUnit;

namespace OilfieldCalc2.Services
{
    public static class MeasurementUnitService
    {                
        public static UnitOfMeasure GetCurrentShortLengthUnit()
        {
            var unit = GetUnitsOfMeasure.ShortLengthUnits().Find(c => c.UnitName.Contains(Preferences.Get("ShortLengthUnit", "Millimeters")));
           
            if (unit != null)
                return unit;
            return ShortLength.Millimeter;
        }

        public static UnitOfMeasure GetCurrentLongLengthUnit()
        {
            var unit = GetUnitsOfMeasure.LongLengthUnits().Find(c => c.UnitName.Contains(Preferences.Get("LongLengthUnit", "Meters")));
            if (unit != null)
                return unit;
            return LongLength.Meter;
        }

        public static UnitOfMeasure GetCurrentVolumeUnit()
        {
            var unit = GetUnitsOfMeasure.VolumeUnits().Find(c => c.UnitName.Contains(Preferences.Get("VolumeUnit", "Cubic Meters")));
            if (unit != null)
                return unit;
            return Volume.CubicMeters;
        }

        public static UnitOfMeasure GetCurrentCapacityUnit()
        {
            var unit = GetUnitsOfMeasure.CapacityUnits().Find(c => c.UnitName.Contains(Preferences.Get("CapacityUnit", "Cubes per Meter")));
            if (unit != null)
                return unit;
            return Capacity.CubicMetersPerMeter;
        }

        public static UnitOfMeasure GetCurrentMassUnit()
        {
            var unit = GetUnitsOfMeasure.MassUnits().Find(c => c.UnitName.Contains(Preferences.Get("MassUnit", "Kilograms")));
            if (unit != null)
                return unit;
            return Mass.Kilogram;
        }

        public static void SetLongLengthUnit(UnitOfMeasure unit)
        {            
            if (unit is LongLength)
                Preferences.Set("LongLengthUnit", unit.UnitName);
            else
                throw new System.ArgumentException("Unit must be a Long Length", nameof(unit));
        }

        public static void SetShortLengthUnit(UnitOfMeasure unit)
        {
            if (unit is ShortLength)
                Preferences.Set("ShortLengthUnit", unit.UnitName);
            else
                throw new System.ArgumentException("Unit must be a Short Length", nameof(unit));
        }

        public static void SetVolumeUnit(UnitOfMeasure unit)
        {
            if (unit is Volume)
                Preferences.Set("VolumeUnit", unit.UnitName);
            else
                throw new System.ArgumentException("Unit must be a Volume", nameof(unit));
        }

        public static void SetCapacityUnit(UnitOfMeasure unit)
        {
            if (unit is Capacity)
                Preferences.Set("CapacityUnit", unit.UnitName);
            else
                throw new System.ArgumentException("Unit must be a Capacity", nameof(unit));          
        }

        public static void SetMassUnit(UnitOfMeasure unit)
        {
            if (unit is Mass)
                Preferences.Set("MassUnit", unit.UnitName);
            else
                throw new System.ArgumentException("Unit must be a mass", nameof(unit));
        }
        
    }
}
