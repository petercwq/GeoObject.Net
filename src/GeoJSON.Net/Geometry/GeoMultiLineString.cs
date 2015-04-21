//  Author: Weiqing Chen <kevincwq@gmail.com>
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
    /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#multilinestring">MultiLineString</see> type.
    /// </summary>
    [DataContract]
    public class GeoMultiLineString : GeoObject
    {
        /// <summary>
        /// Gets the Coordinates.
        /// </summary>
        /// <value>The Coordinates.</value>
        //[JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        //[JsonConverter(typeof(PolygonConverter))]
        [IgnoreDataMember]
        public List<GeoLineString> LineStrings { get; private set; }

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
                if (LineStrings == null)
                    LineStrings = new List<GeoLineString>(value.Count);
                foreach (var list in value)
                {
                    var linestring = new GeoLineString(list);
                    this.LineStrings.Add(linestring);
                }
            }
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="GeoMultiLineString"/> class.
        ///// </summary>
        //internal GeoMultiLineString()
        //{
        //    this.LineStrings = new List<GeoLineString>();
        //    this.Type = GeoObjectType.MultiLineString;
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoMultiLineString" /> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public GeoMultiLineString(List<GeoLineString> coordinates)
        {
            LineStrings = coordinates ?? new List<GeoLineString>();
            Type = GeoObjectType.MultiLineString;
        }

        protected bool Equals(GeoMultiLineString other)
        {
            return base.Equals(other) && LineStrings.SequenceEqual(other.LineStrings);
        }

        public static bool operator !=(GeoMultiLineString left, GeoMultiLineString right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(GeoMultiLineString left, GeoMultiLineString right)
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

            return Equals((GeoMultiLineString)obj);
        }

        public override int GetHashCode()
        {
            return LineStrings.GetHashCode();
        }
    }
}