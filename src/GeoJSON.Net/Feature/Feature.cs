//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using GeoObject.Net.Geometry;

namespace GeoObject.Net.Feature
{
    /// <summary>
    /// A GeoJSON <see cref="!:http://geojson.org/geojson-spec.html#feature-objects">Feature Object</see>.
    /// </summary>
    [DataContract]
    [KnownType(typeof(GeoPoint))]
    [KnownType(typeof(GeoLineString))]
    [KnownType(typeof(GeoPolygon))]
    [KnownType(typeof(GeoMultiPoint))]
    [KnownType(typeof(GeoMultiLineString))]
    [KnownType(typeof(GeoMultiPolygon))]
    [KnownType(typeof(GeoCollection))]
    public class Feature : GeoObject
    {
        /// <summary>
        /// Gets or sets the geometry.
        /// </summary>
        /// <value>
        /// The geometry.
        /// </value>
        //[JsonProperty(PropertyName = "geometry", Required = Required.AllowNull)]
        //[JsonConverter(typeof(GeometryConverter))]
        [DataMember(Name = "geometry", IsRequired = true)]
        public GeoObject Geometry { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The handle.</value>
        //[JsonProperty(PropertyName = "id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        //[JsonProperty(PropertyName = "properties", Required = Required.AllowNull)]
        [DataMember(Name = "properties")]
        public Dictionary<string, object> Properties { get; private set; }

        internal Feature()
        {
            Type = GeoObjectType.Feature;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Feature" /> class.
        /// </summary>
        /// <param name="geometry">The Geometry Object.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="id">The (optional) identifier.</param>
        // [JsonConstructor]
        public Feature(GeoObject geometry, Dictionary<string, object> properties = null, string id = null)
            : this()
        {
            Geometry = geometry;
            Properties = properties ?? new Dictionary<string, object>();
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Feature" /> class.
        /// </summary>
        /// <param name="geometry">The Geometry Object.</param>
        /// <param name="properties">
        /// Class used to fill feature properties. Any public member will be added to feature
        /// properties
        /// </param>
        /// <param name="id">The (optional) identifier.</param>
        public Feature(GeoObject geometry, object properties, string id = null)
            : this()
        {
            Geometry = geometry;
            Id = id;

            if (properties == null)
            {
                Properties = new Dictionary<string, object>();
            }
            else
            {
                Properties = properties.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(properties, null));
            }
        }

        protected override Envelope ComputeBoxInternal()
        {
            return Geometry == null ? null : Geometry.BoundingBox;
        }
    }
}