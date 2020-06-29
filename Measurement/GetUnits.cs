using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Measurement;

namespace Measurement
{
    public static class GetUnits
    {        
        /// <summary>
        /// Uses reflection to get all objects that are of type IShortLengthUnit
        /// </summary>
        /// <returns>List of ShortLengthUnits</returns>
        public static List<IUnit> ShortLengthUnits()
        {
            Assembly asm = typeof(IUnit).Assembly;
            var units = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IShortLengthUnit).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();

            List<IUnit> unitList = new List<IUnit>();
            foreach (string unitName in units)
                unitList.Add((IUnit)asm.CreateInstance(asm.GetName().Name + ".ShortLengthUnits." + unitName));

            return unitList;
        }

        /// <summary>
        /// Uses reflection to get all objects that are of type ILongLengthUnit
        /// </summary>
        /// <returns>List of LongLengthUnits</returns>
        public static List<IUnit> LongLengthUnits()
        {
            Assembly asm = typeof(IUnit).Assembly;
            var units = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(ILongLengthUnit).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();

            List<IUnit> unitList = new List<IUnit>();
            foreach (string unitName in units)
                unitList.Add((IUnit)asm.CreateInstance(asm.GetName().Name + ".LongLengthUnits." + unitName));

            return unitList;
        }

        /// <summary>
        /// Uses reflection to get all objects that are of type IVolumeUnit
        /// </summary>
        /// <returns>List of VolumeUnits</returns>
        public static List<IUnit> VolumeUnits()
        {
            Assembly asm = typeof(IUnit).Assembly;
            var units = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IVolumeUnit).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();

            List<IUnit> unitList = new List<IUnit>();
            foreach (string unitName in units)
                unitList.Add((IUnit)asm.CreateInstance(asm.GetName().Name + ".VolumeUnits." + unitName));

            return unitList;
        }

        /// <summary>
        /// Uses reflection to get all objects that are of type ICapacityUnit
        /// </summary>
        /// <returns>List of CapacityUnits</returns>
        public static List<IUnit> CapacityUnits()
        {
            Assembly asm = typeof(IUnit).Assembly;
            var units = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(ICapacityUnit).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();

            List<IUnit> unitList = new List<IUnit>();
            foreach (string unitName in units)
                unitList.Add((IUnit)asm.CreateInstance(asm.GetName().Name + ".CapacityUnits." + unitName));

            return unitList;
        }

        /// <summary>
        /// Uses reflection to get all objects that are of type IMassUnit
        /// </summary>
        /// <returns>List of MassUnits</returns>
        public static List<IUnit> MassUnits()
        {
            Assembly asm = typeof(IUnit).Assembly;
            var units = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IMassUnit).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();

            List<IUnit> unitList = new List<IUnit>();
            foreach (string unitName in units)
                unitList.Add((IUnit)asm.CreateInstance(asm.GetName().Name + ".MassUnits." + unitName));

            return unitList;
        }
    }
}