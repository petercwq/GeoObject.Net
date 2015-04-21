//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using GeoJSON.Net.Converters;
using Newtonsoft.Json;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// In geography, a point refers to a Position on a map, expressed in y and x.
    /// </summary>
    /// <seealso cref="!:http://geojson.org/geojson-spec.html#point" />
    public class GeoPoint : GeoObject
    {
        /// <summary>
        /// Gets or sets the Coordinate(s).
        /// </summary>
        /// <value>The Coordinates.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PointConverter))]
        public IGeoEntity Coordinates { get; set; }

        [JsonConstructor]
        private GeoPoint() { }

        protected bool Equals(GeoPoint other)
        {
            return base.Equals(other) && Coordinates.Equals(other.Coordinates);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoPoint" /> class.
        /// </summary>
        /// <param name="coordinates">The Position.</param>
        public GeoPoint(IGeoEntity coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException("coordinates");
            }

            Coordinates = coordinates;
            Type = GeoObjectType.Point;
        }

        public static bool operator !=(GeoPoint left, GeoPoint right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(GeoPoint left, GeoPoint right)
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

            return Equals((GeoPoint)obj);
        }

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }
    }
}