using System;
using System.Runtime.CompilerServices;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync;

namespace OilfieldCalc2.Models.MeasureableUnit
{
    /// <summary>
    /// Measurement object. Allows a measurement value to
    /// be associated with the units of measure it derives
    /// from. Also allows conversion to different units of
    /// measure but checks to make sure the units are
    /// compatible prior to conversion.
    /// </summary>
    
    public class Measurement : IEquatable<Measurement>
    {
        public double Value { get; set; }                //The numeric data value
        public double MetricValue { get; private set; }  //Calculates and stores the metric equivalent based on the conversion factor. Used for converting to other units of measure

        public UnitOfMeasure Unit { get; set; }          //The units of measure associated with the measurement

        /// <summary>
        /// Measurement data with associated units of measure attached
        /// </summary>
        /// <param name="value">The numeric data of the measurement</param>
        /// <param name="unit">Units of meassure accociated with the measurement</param>
        public Measurement(double value, UnitOfMeasure unit)
        {
            Value = value;
            Unit = unit;

            if (Unit != null)
                MetricValue = Value / Unit.ConversionFactor;
            else
                MetricValue = Value;
        }

        /// <summary>
        /// Converts to new unit of measure. Throws exception if units are not compatible
        /// </summary>
        /// <param name="newUnit">The units of measure to be converted to</param>
        /// <returns>New, converted Measurement</returns>
        public Measurement Convert (UnitOfMeasure newUnit)
        {
            if (newUnit!=null && Unit.MeasurementType!=null)
            {
                //Check to make sure units are compatible before attempting to convert                
                if (Unit.MeasurementType == newUnit.MeasurementType)
                {
                    //perform the conversion...
                    Value = MetricValue * newUnit.ConversionFactor;
                    Unit = newUnit;
                }
                else
                {
                    throw new System.ArgumentOutOfRangeException(nameof(newUnit), newUnit, "Units are not compatible and unable to convert");
                }
            }
            return this;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Gets the current units of measure in long form
        /// </summary>
        /// <returns>Current units of measure</returns>
        public UnitOfMeasure GetUnitOfMeasure()
        {
            return Unit;
        }

        /// <summary>
        /// Gets the current units of measure in abriviated form
        /// </summary>
        /// <returns>Current units of measure abreviation</returns>
        public string GetUnitAbreviation()
        {
            return Unit.Abreviation;
        }

        public bool Equals(Measurement other)
        {
            if (other == null)
                return false;

            if (this.Value == other.Value && this.Unit == other.Unit)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Measurement mObj = obj as Measurement;

            if (mObj == null)
                return false;
            else
                return this.Equals(mObj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Value) ? Value.GetHashCode() : 0);

                return hash;
            }
        }

        public static bool operator == (Measurement obj1, Measurement obj2)
        {
            if ((object)obj1 == null || (object)obj2 == null)
                return System.Object.Equals(obj1, obj2);

            return obj1.Equals(obj2);
        }

        public static bool operator != (Measurement obj1, Measurement obj2)
        {
            return !(obj1 == obj2);
        }
    }
}
