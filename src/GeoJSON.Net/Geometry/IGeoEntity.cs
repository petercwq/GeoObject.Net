//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Collections.Generic;

namespace GeoJSON.Net.Geometry
{
    //[JsonArray]
    public interface IGeoEntity : IEquatable<IGeoEntity>
    {
        /// <summary>
        /// Gets the x.
        /// </summary>
        /// <value>The x.</value>
        double X { get; }

        /// <summary>
        /// Gets the y.
        /// </summary>
        /// <value>The y.</value>
        double Y { get; }

        /// <summary>
        /// Gets the z.
        /// </summary>
        double? Z { get; }
    }

    public static class GeoEntityExts
    {
        public static List<double> GetCoordinates(this IGeoEntity entity)
        {
            if (entity == null)
                return new List<double>(0);
            if (entity.Z.HasValue)
                return new List<double>() { entity.X, entity.Y, entity.Z.Value };
            else
                return new List<double>() { entity.X, entity.Y };
        }
    }
}