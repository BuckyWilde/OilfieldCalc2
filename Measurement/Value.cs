using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Measurement
{
    /// <summary>
    /// Value data type holds a value of a measurement and the units of measure associated with it.
    /// </summary>
    public struct Value : IMeasurement, IEquatable<Value>
    {
        public double CurrentValue { get; set; }
 
        public double MetricValue { get; private set; }

        //[JsonIgnore]
        public IUnit Unit { get; set; }

        /// <summary>
        /// Measurement value with associated unit of measure
        /// </summary>
        /// <param name="value">Measurement Value</param>
        /// <param name="unit">Unit of measure</param>
        public Value(double value, IUnit unit)
        {
            CurrentValue = value;
            Unit = unit;

            if (unit != null)
                MetricValue = CurrentValue / unit.ConversionFactor;
            else
                MetricValue = CurrentValue / 1;
        }

        public override string ToString()
        {
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("en-US");

            if (Unit != null)
                return this.CurrentValue + " " + this.Unit.ShortName;

            return CurrentValue.ToString("G", culture);
        }

        /// <summary>
        /// Converts units from one type to another (ie: feet to meters)
        /// Checks to see if units are compatible first and throws an
        /// exception if they are not
        /// </summary>
        /// <param name="newUnit"></param>
        /// <returns>Returns a a value instance with the new units attached</returns>
        public Value Convert (IUnit newUnit)
        {
            if (newUnit != null)
            {
                Type[] newTypes = newUnit.GetType().GetInterfaces();
                Type[] thisTypes = this.Unit.GetType().GetInterfaces();

                //compare type to ensure they are compatible
                //index 0 should point to the first level up interface
                if (newTypes[0] != thisTypes[0])
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                    throw new System.ArgumentException("Unit types are not compatible", nameof(newUnit));
#pragma warning restore CA1303 // Do not pass literals as localized parameters

                this.CurrentValue = this.MetricValue * newUnit.ConversionFactor;
                this.Unit = newUnit;
            }

            return this;
        }

        //public override bool Equals(object obj)
        //{
        //    var item = obj as IMeasurement;

        //    if (obj == null)
        //        return false;

        //    return this.MetricValue.Equals(item.MetricValue);

        //}

        public override int GetHashCode()
        {
            return this.MetricValue.GetHashCode();
        }

        public static bool operator ==(Value left, Value right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Value left, Value right)
        {
            return !(left == right);
        }

        public bool Equals(Value other)
        {
            if (other != null)
                return this.MetricValue.Equals(other.MetricValue);

            return false;
        }
    }
}
