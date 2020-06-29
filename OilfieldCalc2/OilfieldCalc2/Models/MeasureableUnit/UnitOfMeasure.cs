using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit
{
    public class UnitOfMeasure : IEquatable<UnitOfMeasure>
    {
        public int Id { get; set; }
        public virtual string UnitName { get; set; }            //Full name of the unit of measure
        public virtual string Abreviation { get; set; }         //Standard abreviation of the unit
        public virtual double ConversionFactor { get; set; }    //Conversion factor to convert the unit to equivalent metric value
        public virtual string MeasurementType { get; set; }

        public bool Equals(UnitOfMeasure other)
        {
            if (other == null)
                return false;

            return this.UnitName == other.UnitName;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            UnitOfMeasure uomObj = obj as UnitOfMeasure;

            if (uomObj == null)
                return false;
            else
                return this.Equals(uomObj);
        }

        public override int GetHashCode()
        {
            if (this.UnitName == null || this.Abreviation == null)
                return 0;
            else
                return this.UnitName.GetHashCode();
        }

        public static bool operator == (UnitOfMeasure obj1, UnitOfMeasure obj2)
        {
            if ((object)obj1 == null || (object)obj2 == null)
                return System.Object.Equals(obj1, obj2);

            return obj1.Equals(obj2);
        }

        public static bool operator !=(UnitOfMeasure obj1, UnitOfMeasure obj2)
        {
            return !(obj1 == obj2);
        }

        public override string ToString()
        {
            return Abreviation;
        }
    }
}