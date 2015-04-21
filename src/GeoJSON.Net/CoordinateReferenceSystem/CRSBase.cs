//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GeoJSON.Net.CoordinateReferenceSystem
{
    /// <summary>
    /// Base class for all IGeometryObject implementing types
    /// </summary>
    //[JsonObject(MemberSerialization.OptIn)]
    [DataContract]
    [KnownType(typeof(LinkedCRS))]
    [KnownType(typeof(NamedCRS))]
    public abstract class CRSBase
    {
        /// <summary>
        /// Gets the properties.
        /// </summary>
        //[JsonProperty(PropertyName = "properties", Required = Required.Always)]
        [DataMember(Name = "properties", IsRequired = true)]
        public Dictionary<string, object> Properties { get; internal set; }

        /// <summary>
        /// Gets the type of the GeometryObject object.
        /// </summary>
        //[JsonProperty(PropertyName = "type", Required = Required.Always)]
        //[JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "type", IsRequired = true)]
        // [IgnoreDataMember]
        public CRSType Type { get; internal set; }

        //[DataMember(Name = "type", EmitDefaultValue = true, IsRequired = true)]
        //internal string TypeString
        //{
        //    get { return Enum.GetName(typeof(CRSType), this.Type); }
        //    set { this.Type = (CRSType)Enum.Parse(typeof(CRSType), value, true); }
        //}
    }
}