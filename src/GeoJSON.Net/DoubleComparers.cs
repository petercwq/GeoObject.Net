//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Collections.Generic;

namespace GeoJSON.Net
{
	/// <summary>
    /// Compares doubles for equality.
    /// </summary>
    /// <remarks>
    /// 10 decimal places equates to accuracy to 11.1 μm.
    /// </remarks>
    public class DoubleTenDecimalPlaceComparer : IEqualityComparer<double>
    {

        public bool Equals(double x, double y)
        {
            return Math.Abs(x - y) < 0.0000000001;
        }

        public int GetHashCode(double obj)
        {
            return obj.GetHashCode();
        }
    }

    /// <summary>
    /// Compares nullable doubles for equality.
    /// </summary>
    /// <remarks>
    /// 10 decimal places equates to accuracy to 11.1 μm.
    /// </remarks>
    public class NullableDoubleTenDecimalPlaceComparer : IEqualityComparer<double?>
    {

        public bool Equals(double? x, double? y)
        {
            return Math.Abs(x.GetValueOrDefault() - y.GetValueOrDefault()) < 0.0000000001;
        }

        public int GetHashCode(double? obj)
        {
            return obj.HasValue ? obj.Value.GetHashCode() : 0;
        }
    }
}