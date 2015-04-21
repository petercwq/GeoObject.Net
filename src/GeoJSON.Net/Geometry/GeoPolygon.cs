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
    /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#polygon" >Polygon</see> type.
    /// Coordinates of a Polygon are a list of
    /// <see cref="!:http://geojson.org/geojson-spec.html#linestring" >linear rings</see>
    /// coordinate arrays. The first element in the array represents the exterior ring. Any subsequent elements
    /// represent interior rings (or holes).
    /// </summary>
    /// <seealso cref="!:http://geojson.org/geojson-spec.html#polygon" />
    [DataContract]
    public class GeoPolygon : GeoObject
    {
        /// <summary>
        /// Gets the list of points outlining this Polygon.
        /// </summary>
        //[JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        //[JsonConverter(typeof(PolygonConverter))]
        public List<GeoLineString> LineStrings { get; set; }


        /// <summary>
        /// Gets the coordinates
        /// </summary>
        [DataMember(Name = "coordinates", IsRequired = true)]
        public List<List<List<double>>> Coordinates
        {
            get
            {
                List<List<List<double>>> coordinates = new List<List<List<double>>>();
                foreach (var linestring in LineStrings)
                {
                    coordinates.Add(linestring.Coordinates);
                }
                return coordinates;
            }

            set
            {
                foreach (var list in value)
                {
                    var linestring = new GeoLineString();
                    linestring.Coordinates = list;
                    this.LineStrings.Add(linestring);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoPolygon"/> class.
        /// </summary>
        internal GeoPolygon()
        {
            this.LineStrings = new List<GeoLineString>();
            this.Type = GeoObjectType.Polygon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoPolygon" /> class.
        /// </summary>
        /// <param name="linearRings">
        /// The <see cref="!:http://geojson.org/geojson-spec.html#linestring" >linear rings</see> with the first element
        /// in the array representing the exterior ring. Any subsequent elements represent interior rings (or holes).
        /// </param>
        public GeoPolygon(List<GeoLineString> linearRings)
        {
            if (linearRings == null)
            {
                throw new ArgumentNullException("coordinates");
            }

            if (linearRings.Any(linearRing => !linearRing.IsLinearRing()))
            {
                throw new ArgumentException("All elements must be closed LineStrings with 4 or more positions" +
                                            " (see GeoJSON spec at 'http://geojson.org/geojson-spec.html#linestring').", "coordinates");
            }

            LineStrings = linearRings;
            Type = GeoObjectType.Polygon;
        }

        protected bool Equals(GeoPolygon other)
        {
            return base.Equals(other) && LineStrings.SequenceEqual(other.LineStrings);
        }

        public static bool operator !=(GeoPolygon left, GeoPolygon right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(GeoPolygon left, GeoPolygon right)
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

            return Equals((GeoPolygon)obj);
        }

        public override int GetHashCode()
        {
            return LineStrings.GetHashCode();
        }
    }
}