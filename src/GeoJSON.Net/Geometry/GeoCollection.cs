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
    /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#geometry-collection">GeometryCollection</see> type.
    /// </summary>
    [DataContract]
    public class GeoCollection : GeoObject
    {
        /// <summary>
        /// Gets the list of Polygons enclosed in this MultiPolygon.
        /// </summary>
        //[JsonProperty(PropertyName = "geometries", Required = Required.Always)]
        //[JsonConverter(typeof(GeometryConverter))]
        [DataMember(Name = "geometries", IsRequired = true)]
        public List<GeoObject> Geometries { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="GeoCollection"/>, is equal to this instance.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        protected bool Equals(GeoCollection other)
        {
            return base.Equals(other) && Geometries.SequenceEqual(other.Geometries);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoCollection" /> class.
        /// </summary>
        /// <param name="geometries">The geometries contained in this GeometryCollection.</param>
        public GeoCollection(List<GeoObject> geometries)
        {
            if (geometries == null)
            {
                throw new ArgumentNullException("geometries");
            }

            Geometries = geometries;
            Type = GeoObjectType.GeometryCollection;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(GeoCollection left, GeoCollection right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(GeoCollection left, GeoCollection right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
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

            return Equals((GeoCollection)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Geometries.GetHashCode();
        }

        protected override Envelope ComputeBoxInternal()
        {
            if (Geometries == null)
                return null;

            var env = new Envelope();
            foreach (var g in Geometries)
            {
                env.ExpandToInclude(g.BoundingBox);
            }
            return env;
        }
    }
}