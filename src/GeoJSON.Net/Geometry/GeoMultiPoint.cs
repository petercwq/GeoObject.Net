﻿//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// Contains an array of <see cref="GeoPoint" />s.
    /// </summary>
    /// <seealso cref="!:http://geojson.org/geojson-spec.html#multipoint" />
    [DataContract]
    public class GeoMultiPoint : GeoObject
    {
        /// <summary>
        /// Gets the Coordinates.
        /// </summary>
        /// <value>The Coordinates.</value>
        //[JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        //[JsonConverter(typeof(MultiPointConverter))]
        [IgnoreDataMember]
        public List<GeoPoint> Points { get; private set; }

        /// <summary>
        /// Gets the Coordinates.
        /// </summary>
        /// <value>The Coordinates.</value>
        //[JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [DataMember(Name = "coordinates", IsRequired = true)]
        public List<List<double>> Coordinates
        {
            get
            {
                List<List<double>> coordinates = new List<List<double>>();
                foreach (var ipos in Points)
                {
                    coordinates.Add(ipos.Coordinates);
                }
                return coordinates;
            }

            set
            {
                foreach (var list in value)
                {
                    var point = new GeoPoint() { Coordinates = list };
                    this.Points.Add(point);
                }
            }
        }

        internal GeoMultiPoint(List<List<double>> coordinates)
        {
            this.Coordinates = coordinates;
            this.Type = GeoObjectType.MultiPoint;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoMultiPoint" /> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public GeoMultiPoint(List<GeoPoint> coordinates = null)
        {
            this.Points = coordinates ?? new List<GeoPoint>();
            this.Type = GeoObjectType.MultiPoint;
        }

        protected bool Equals(GeoMultiPoint other)
        {
            return base.Equals(other) && Points.SequenceEqual(other.Points);
        }

        public static bool operator !=(GeoMultiPoint left, GeoMultiPoint right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(GeoMultiPoint left, GeoMultiPoint right)
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
            return Equals((GeoMultiPoint)obj);
        }

        public override int GetHashCode()
        {
            return Points.GetHashCode();
        }
    }
}