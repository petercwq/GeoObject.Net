//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#multipolygon">MultiPolygon</see> type.
    /// </summary>
    [DataContract]
    public class GeoMultiPolygon : GeoObject
    {
        /// <summary>
        /// Gets the list of Polygons enclosed in this MultiPolygon.
        /// </summary>
        //[JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        //[JsonConverter(typeof(MultiPolygonConverter))]
        [IgnoreDataMember]
        public List<GeoPolygon> Polygons { get; private set; }

        /// <summary>
        /// Gets the coordinates
        /// </summary>
        [DataMember(Name = "coordinates", IsRequired = true)]
        public List<List<List<List<double>>>> Coordinates
        {

            get
            {
                List<List<List<List<double>>>> coordinates = new List<List<List<List<double>>>>();
                foreach (var polygon in Polygons)
                {
                    coordinates.Add(polygon.Coordinates);
                }
                return coordinates;
            }

            set
            {
                foreach (var list in value)
                {
                    var polygon = new GeoPolygon(list);
                    this.Polygons.Add(polygon);
                }
            }
        }

        protected bool Equals(GeoMultiPolygon other)
        {
            return base.Equals(other) && Polygons.SequenceEqual(other.Polygons);
        }

        internal GeoMultiPolygon(List<List<List<List<double>>>> coordinates)
        {
            this.Coordinates = coordinates;
            this.Type = GeoObjectType.MultiPolygon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoMultiPolygon" /> class.
        /// </summary>
        /// <param name="polygons">The polygons contained in this MultiPolygon.</param>
        public GeoMultiPolygon(List<GeoPolygon> polygons)
        {
            if (polygons == null)
            {
                throw new ArgumentNullException("polygons");
            }

            Polygons = polygons;
            Type = GeoObjectType.MultiPolygon;
        }

        public static bool operator !=(GeoMultiPolygon left, GeoMultiPolygon right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(GeoMultiPolygon left, GeoMultiPolygon right)
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

            return Equals((GeoMultiPolygon)obj);
        }

        public override int GetHashCode()
        {
            return Polygons.GetHashCode();
        }
    }
}