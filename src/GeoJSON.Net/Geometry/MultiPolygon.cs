//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Converters;
using Newtonsoft.Json;

namespace GeoJSON.Net.Geometry
{
	/// <summary>
    /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#multipolygon">MultiPolygon</see> type.
    /// </summary>
    public class MultiPolygon : GeoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Gets the list of Polygons enclosed in this MultiPolygon.
        /// </summary>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(MultiPolygonConverter))]
        public List<Polygon> Coordinates { get; private set; }

        protected bool Equals(MultiPolygon other)
        {
            return base.Equals(other) && Coordinates.SequenceEqual(other.Coordinates);
        }

        public MultiPolygon()
            : this(new List<Polygon>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPolygon" /> class.
        /// </summary>
        /// <param name="polygons">The polygons contained in this MultiPolygon.</param>
        public MultiPolygon(List<Polygon> polygons)
        {
            if (polygons == null)
            {
                throw new ArgumentNullException("polygons");
            }

            Coordinates = polygons;
            Type = GeoJSONObjectType.MultiPolygon;
        }

        public static bool operator !=(MultiPolygon left, MultiPolygon right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(MultiPolygon left, MultiPolygon right)
        {
            return Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((MultiPolygon)obj);
        }

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }
    }
}