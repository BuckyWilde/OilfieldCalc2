using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OilfieldCalc2.Models.MeasureableUnit
{
    /// <summary>
    /// Get all the units of measure for such purposes as picker population
    /// </summary>
    public static class GetUnitsOfMeasure
    {
        /// <summary>
        /// Get a list of all ShortLength units
        /// </summary>
        /// <returns></returns>
        public static List<UnitOfMeasure> ShortLengthUnits()
        {
            List<UnitOfMeasure> units = new List<UnitOfMeasure>();

            foreach (Type type in Assembly.GetAssembly(typeof(ShortLength)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(ShortLength))))
            {
                units.Add((ShortLength)Activator.CreateInstance(type));
            }

            return units;
        }

        /// <summary>
        /// Get a list of all LongLength units
        /// </summary>
        /// <returns></returns>
        public static List<UnitOfMeasure> LongLengthUnits()
        {
            List<UnitOfMeasure> units = new List<UnitOfMeasure>();

            foreach (Type type in Assembly.GetAssembly(typeof(LongLength)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(LongLength))))
            {
                units.Add((LongLength)Activator.CreateInstance(type));
            }

            return units;
        }

        /// <summary>
        /// Get a list of all Volume units
        /// </summary>
        /// <returns></returns>
        public static List<UnitOfMeasure> VolumeUnits()
        {
            List<UnitOfMeasure> units = new List<UnitOfMeasure>();

            foreach (Type type in Assembly.GetAssembly(typeof(Volume)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Volume))))
            {
                units.Add((Volume)Activator.CreateInstance(type));
            }

            return units;
        }

        /// <summary>
        /// Get a list of all Capacity units
        /// </summary>
        /// <returns></returns>
        public static List<UnitOfMeasure> CapacityUnits()
        {
            List<UnitOfMeasure> units = new List<UnitOfMeasure>();

            foreach (Type type in Assembly.GetAssembly(typeof(Capacity)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Capacity))))
            {
                units.Add((Capacity)Activator.CreateInstance(type));
            }

            return units;
        }

        /// <summary>
        /// Get a list of all Mass units
        /// </summary>
        /// <returns></returns>
        public static List<UnitOfMeasure> MassUnits()
        {
            List<UnitOfMeasure> units = new List<UnitOfMeasure>();

            foreach (Type type in Assembly.GetAssembly(typeof(Mass)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Mass))))
            {
                units.Add((Mass)Activator.CreateInstance(type));
            }

            return units;
        }
    }
}
