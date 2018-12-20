using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2018.Solver.Models
{
    public enum LicenseModeCalculationMode { Metadata = 1, License = 2 }

    /// <summary>
    /// Node from the license file
    /// </summary>
    public class NavigationLicenseNode
    {
        public int CalculateLicense(LicenseModeCalculationMode calculationMode)
        {
            switch (calculationMode)
            {
                case LicenseModeCalculationMode.Metadata:
                    return Metadata.DefaultIfEmpty(0).Sum() + Children.Sum(c => c.CalculateLicense(calculationMode));
                case LicenseModeCalculationMode.License:
                    if (!Children.Any())
                        return Metadata.DefaultIfEmpty(0).Sum();
                    var current = 0;
                    foreach(var index in Metadata.Select(m => m - 1))
                    {
                        if (index < Children.Count)
                            current += Children[index].CalculateLicense(calculationMode);
                    }
                    return current;
            }

            return 0;
        }

        /// <summary>
        /// Current index at end of node
        /// </summary>
        public int EndOfNodeIndex { get; set; }

        /// <summary>
        /// Metadata for this node
        /// </summary>
        public List<int> Metadata { get; set; } = new List<int>();

        /// <summary>
        /// Children for this node
        /// </summary>
        public List<NavigationLicenseNode> Children { get; set; } = new List<NavigationLicenseNode>();
    }
}
