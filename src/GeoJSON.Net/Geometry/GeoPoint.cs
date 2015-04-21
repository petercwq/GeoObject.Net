//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// In geography, a point refers to a Position on a map, expressed in y and x.
    /// </summary>
    /// <seealso cref="!:http://geojson.org/geojson-spec.html#point" />
    [DataContract]
    public class GeoPoint : GeoObject
    {
        /// <summary>
        /// Gets or sets the Coordinate(s).
        /// </summary>
        /// <value>The Coordinates.</value>
        //[JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        //[JsonConverter(typeof(PointConverter))]
        [IgnoreDataMember]
        public IGeoEntity Entity { get; set; }

        /// <summary>
        /// Gets the Coordinate(s).
        /// </summary>
        /// <value>The Coordinates.</value>
        //        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        //        [JsonConverter(typeof(PositionConverter))]
        [DataMember(Name = "coordinates", IsRequired = true)]
        public List<double> Coordinates
        {
            get
            {
                List<double> coordinates = new List<double>();
                if (Entity != null)
                {
                    coordinates.Add(Entity.X);
                    coordinates.Add(Entity.Y);
                    if (Entity.Z.HasValue)
                        coordinates.Add(Entity.Z.Value);
                }
                return coordinates;
            }

            set
            {
                if (value == null || value.Count < 2)
                    Entity = null;
                else
                    Entity = new GeoEntity(value[0], value[1], value.Count > 2 ? (double?)value[2] : null);
            }
        }

        public GeoPoint()
        {
            this.Type = GeoObjectType.Point;
        }

        //internal GeoPoint(List<double> coordinates)
        //    : this()
        //{
        //    this.Coordinates = coordinates;
        //}

        ///// <summary>
        ///// Initializes a new instance of the <see cref="Point"/> class.
        ///// </summary>
        ///// <param name="entity">The Position.</param>
        //public GeoPoint(IGeoEntity entity)
        //    : this()
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("entity");
        //    }

        //    this.Entity = entity;
        //}

        protected bool Equals(GeoPoint other)
        {
            return base.Equals(other) && Entity.Equals(other.Entity);
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
            return Entity.GetHashCode();
        }
    }
}