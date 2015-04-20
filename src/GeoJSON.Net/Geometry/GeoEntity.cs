//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Globalization;
using System.Linq;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// A position is the fundamental geometry construct. 
    /// The "coordinates" member of a geometry object is composed of one position (in the case of a Point geometry)
    /// , an array of positions (LineString or MultiPoint geometries), 
    /// an array of arrays of positions (Polygons, MultiLineStrings), 
    /// or a multidimensional array of positions (MultiPolygon).
    /// <see cref="!:http://geojson.org/geojson-spec.html#positions">
    /// </summary>
    public class GeoEntity : IGeoEntity
    {
        /// <summary>
        /// Gets or sets the coordinates, is a 2-size array
        /// </summary>
        /// <value>
        /// The coordinates.
        /// </value>
        private double?[] Coordinates { get; set; }

        private static readonly NullableDoubleTenDecimalPlaceComparer DoubleComparer = new NullableDoubleTenDecimalPlaceComparer();

        /// <summary>
        /// Gets the x.
        /// </summary>
        /// <value>The x.</value>
        public double X
        {
            get { return Coordinates[1].GetValueOrDefault(); }
            private set { Coordinates[1] = value; }
        }

        /// <summary>
        /// Gets the y.
        /// </summary>
        /// <value>The y.</value>
        public double Y
        {
            get { return Coordinates[0].GetValueOrDefault(); }
            private set { Coordinates[0] = value; }
        }

        /// <summary>
        /// Gets the z.
        /// </summary>
        public double? Z
        {
            get { return Coordinates[2]; }
            private set { Coordinates[2] = value; }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="GeoEntity" /> class from being created.
        /// </summary>
        private GeoEntity()
        {
            Coordinates = new double?[3];
        }

        /// <summary>
        /// Determines whether the specified <see cref="GeoEntity" />, is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="GeoEntity" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="GeoEntity" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        protected bool Equals(GeoEntity other)
        {
            return Coordinates.SequenceEqual(other.Coordinates, DoubleComparer);
        }

        public bool Equals(IGeoEntity pos)
        {
            if (pos == null || !(pos is GeoEntity))
                return false;
            else
                return Equals(pos as GeoEntity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoEntity" /> class.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="x">The x.</param>
        /// <param name="z">The z in m(eter).</param>
        public GeoEntity(double y, double x, double? z = null)
            : this()
        {
            Y = y;
            X = x;
            Z = z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoEntity" /> class.
        /// </summary>
        /// <param name="y">The y, in degree when use X, in meters when use Easting, e.g. '38.889722'.</param>
        /// <param name="x">The x, in degree when use Y, in meters when use Northing, e.g. '-77.008889'.</param>
        /// <param name="z">The z in m(eters).</param>
        public GeoEntity(string y, string x, string z = null)
            : this()
        {
            if (y == null)
            {
                throw new ArgumentNullException("y");
            }

            if (x == null)
            {
                throw new ArgumentNullException("x");
            }

            if (string.IsNullOrWhiteSpace(y))
            {
                throw new ArgumentOutOfRangeException("y", "May not be empty.");
            }

            if (string.IsNullOrWhiteSpace(x))
            {
                throw new ArgumentOutOfRangeException("x", "May not be empty.");
            }

            double ty;
            double tx;

            if (!double.TryParse(y, NumberStyles.Float, CultureInfo.InvariantCulture, out ty))
            {
                throw new ArgumentOutOfRangeException("y", "Y must be a proper float number value.");
            }

            if (!double.TryParse(x, NumberStyles.Float, CultureInfo.InvariantCulture, out tx))
            {
                throw new ArgumentOutOfRangeException("x", "X must be a proper float number value.");
            }

            Y = ty;
            X = tx;

            if (z != null)
            {
                double tz;
                if (!double.TryParse(z, NumberStyles.Float, CultureInfo.InvariantCulture, out tz))
                {
                    throw new ArgumentOutOfRangeException("z", "Z must be a proper z (m(eter) as double) value, e.g. '6500'.");
                }

                Z = tz;
            }
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(GeoEntity left, GeoEntity right)
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
        public static bool operator ==(GeoEntity left, GeoEntity right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
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

            return Equals((GeoEntity)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Coordinates != null ? Coordinates.GetHashCode() : 0;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Z == null
                ? string.Format(CultureInfo.InvariantCulture, "Y: {0}, X: {1}", Y, X)
                : string.Format(CultureInfo.InvariantCulture, "Y: {0}, X: {1}, Z: {2}", Y, X, Z);
        }
    }
}