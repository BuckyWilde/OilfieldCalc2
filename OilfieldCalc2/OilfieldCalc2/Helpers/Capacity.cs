using System;
using System.Collections.Generic;
using System.Text;

namespace OilfieldCalc2.Helpers
{
    /// <summary>
    /// Capacity class calculates wet & dry displacements,
    /// internal capacities & annular capacities
    /// </summary>
    public static class Capacity
    {
        /// <summary>
        /// Returns the dry displacement per unit of length
        /// </summary>
        /// <param name="OD">Outside diameter of the tubular</param>
        /// <param name="ID">Inside diameter of the tubular</param>
        /// <returns></returns>
        public static double GetDryDisplacement(double Od, double Id)
        {
            return Math.PI * (Math.Pow((Od / 2), 2) - Math.Pow((Id / 2), 2)) / 1000000;
        }

        /// <summary>
        /// Returns the annular capacity per unit of length
        /// </summary>
        /// <param name="ID">Inside diameter of the outer tubular</param>
        /// <param name="OD">Outside diameter of inner tubular</param>
        /// <returns></returns>
        public static double GetAnnularCapacity(double Id, double Od)
        {
            return (Math.PI * (Math.Pow((Id / 2), 2) - Math.Pow((Od / 2), 2)) / 1000000);
        }

        /// <summary>
        /// Returns the wet displacement per unit of length of the tubular
        /// </summary>
        /// <param name="OD">Outside diameter of the tubular</param>
        /// <returns></returns>
        public static double GetWetDisplacement(double Od)
        {
            return (Math.PI * (Math.Pow((Od / 2), 2)) / 1000000);
        }

        /// <summary>
        /// Returns the internal capacity per unit if length of the tubular
        /// </summary>
        /// <param name="ID">Inside diameter of the tubular</param>
        /// <returns></returns>
        public static double GetInternalCapacity(double Id)
        {
            return (Math.PI * (Math.Pow((Id / 2), 2)) / 1000000);
        }
    }
}
