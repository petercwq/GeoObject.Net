//  Author:
//       Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Converters;
using Newtonsoft.Json;

namespace GeoJSON.Net.Geometry
{
	/// <summary>
    /// Contains an array of <see cref="Point" />s.
    /// </summary>
    /// <seealso cref="!:http://geojson.org/geojson-spec.html#multipoint" />
    public class MultiPoint : GeoJSONObject, IGeometryObject
    {

        /// <summary>
        /// Gets the Coordinates.
        /// </summary>
        /// <value>The Coordinates.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(MultiPointConverter))]
        public List<Point> Coordinates { get; private set; }

        protected bool Equals(MultiPoint other)
        {
            return base.Equals(other) && Coordinates.SequenceEqual(other.Coordinates);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPoint" /> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public MultiPoint(List<Point> coordinates = null)
        {
            this.Coordinates = coordinates ?? new List<Point>();
            this.Type = GeoJSONObjectType.MultiPoint;
        }

        public static bool operator !=(MultiPoint left, MultiPoint right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(MultiPoint left, MultiPoint right)
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
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((MultiPoint)obj);
        }

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }
    }
}