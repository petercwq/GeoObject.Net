//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System.Runtime.Serialization;
using GeoObject.Net.CoordinateReferenceSystem;

namespace GeoObject.Net
{
    /// <summary>
    /// Base class for all IGeometryObject implementing types
    /// </summary>
    // [JsonObject(MemberSerialization.OptIn)]
    [DataContract]
    [KnownType(typeof(LinkedCRS))]
    [KnownType(typeof(NamedCRS))]
    public abstract class GeoObject //: IGeoJsonObject
    {
        //internal static readonly DoubleTenDecimalPlaceComparer DoubleComparer = new DoubleTenDecimalPlaceComparer();

        /// <summary>
        /// The bounding box of this <c>Geometry</c>.
        /// </summary>
        private Envelope _envelope;

        [IgnoreDataMember]
        public Envelope BoundingBox
        {
            get
            {
                if (_envelope == null)
                    _envelope = ComputeBoxInternal();
                if (_envelope == null)
                    _envelope = new Envelope();
                return _envelope;
            }
            set
            {
                _envelope = value;
            }
        }

        /// <summary>
        /// Gets or sets the (optional)
        /// <see cref="!:http://geojson.org/geojson-spec.html#coordinate-reference-system-objects" >Bounding Boxes</see>.
        /// </summary>
        /// <value>
        /// The value of <see cref="BoundingBoxes" /> must be a 2*n array where n is the number of dimensions represented in the
        /// contained geometries, with the lowest values for all axes followed by the highest values.
        /// The axes order of a bbox follows the axes order of geometries.
        /// In addition, the coordinate reference system for the bbox is assumed to match the coordinate reference
        /// system of the GeoJSON object of which it is a member.
        /// </value>
        // [JsonProperty(PropertyName = "bbox", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [DataMember(Name = "bbox", EmitDefaultValue = false, IsRequired = false)]
        internal double[] JsonBox
        {
            get
            {
                return _envelope == null ? null : _envelope.JsonBox;
            }
            set
            {
                if (_envelope == null)
                    _envelope = new Envelope();

                if (value != null && value.Length > 3)
                {
                    _envelope.Init(value[0], value[2], value[1], value[3]);

                }
                else
                {
                    _envelope.SetToNull();
                }
            }
        }

        /// <summary>
        /// Gets or sets the (optional)
        /// <see cref="!:http://geojson.org/geojson-spec.html#coordinate-reference-system-objects" >
        /// Coordinate Reference System
        /// Object.
        /// </see>
        /// </summary>
        /// <value>
        /// The Coordinate Reference System Objects.
        /// </value>
        // [JsonProperty(PropertyName = "crs", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
        // [JsonConverter(typeof(CrsConverter))]
        [DataMember(Name = "crs", EmitDefaultValue = false, IsRequired = false)]
        public CRSBase CRS { get; set; }

        /// <summary>
        /// Gets the (mandatory) type of the
        /// <see cref="!:http://geojson.org/geojson-spec.html#geojson-objects" >GeoJSON Object</see>.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        // [JsonProperty(PropertyName = "type", Required = Required.Always)]
        // [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "type", EmitDefaultValue = true, IsRequired = true)]
        // [IgnoreDataMember]
        public GeoObjectType Type { get; internal set; }

        //[DataMember(Name = "type", EmitDefaultValue = true, IsRequired = true)]
        //internal string TypeString
        //{
        //    get { return Enum.GetName(typeof(GeoObjectType), this.Type); }
        //    set { this.Type = (GeoObjectType)Enum.Parse(typeof(GeoObjectType), value, true); }
        //}

        protected abstract Envelope ComputeBoxInternal();

        //protected GeoObject()
        //{
        //    // CRS = DefaultCRS.Instance;
        //}

        /// <summary>
        /// Determines whether the specified <see cref="GeoObject" />, is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="GeoObject" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="GeoObject" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool Equals(GeoObject other)
        {
            if (Type != other.Type)
            {
                return false;
            }

            if (!Equals(CRS, other.CRS))
            {
                return false;
            }

            if (!Equals(_envelope, other._envelope))
            {
                return false;
            }

            return true;
        }

        ///// <summary>
        ///// Called when [deserialized].
        ///// </summary>
        ///// <param name="streamingContext">The streaming context.</param>
        //[OnDeserialized]
        //private void OnDeserialized(StreamingContext streamingContext)
        //{
        //    if (CRS == null)
        //    {
        //        CRS = DefaultCRS.Instance;
        //    }
        //}

        ///// <summary>
        ///// Called when [serialized].
        ///// </summary>
        ///// <param name="streamingContext">The streaming context.</param>
        //[OnSerialized]
        //private void OnSerialized(StreamingContext streamingContext)
        //{
        //    if (CRS == null)
        //    {
        //        CRS = DefaultCRS.Instance;
        //    }
        //}

        ///// <summary>
        ///// Called when [serializing].
        ///// </summary>
        ///// <param name="streamingContext">The streaming context.</param>
        //[OnSerializing]
        //private void OnSerializing(StreamingContext streamingContext)
        //{
        //    if (CRS is DefaultCRS)
        //    {
        //        CRS = null;
        //    }
        //}

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(GeoObject left, GeoObject right)
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
        public static bool operator ==(GeoObject left, GeoObject right)
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

            return Equals((GeoObject)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_envelope != null ? _envelope.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CRS != null ? CRS.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)Type;
                return hashCode;
            }
        }
    }
}