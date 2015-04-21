//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;

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
}