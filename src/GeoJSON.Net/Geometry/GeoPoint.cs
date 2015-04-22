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
        public IGeoEntity Entity { get; private set; }

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
                return Entity.GetCoordinates();
            }
            set
            {
                Entity = new GeoEntity(value);
            }
        }

        //public GeoPoint()
        //{
        //    this.Type = GeoObjectType.Point;
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLineString"/> class.
        /// </summary>
        internal GeoPoint(List<double> coordinates)
        {
            this.Coordinates = coordinates;
            this.Type = GeoObjectType.Point;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="entity">The Position.</param>
        public GeoPoint(IGeoEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.Entity = entity;
            this.Type = GeoObjectType.Point;
        }

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

        protected override Envelope ComputeBoxInternal()
        {
            if (Entity == null)
                return null;

            var env = new Envelope(Entity);
            return env;
        }
    }
}