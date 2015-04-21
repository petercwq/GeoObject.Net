//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System.Runtime.Serialization;

namespace GeoJSON.Net.CoordinateReferenceSystem
{
    /// <summary>
    /// Defines the GeoJSON Coordinate Reference System Objects (CRS) types as defined in the
    /// <see cref="!:http://geojson.org/geojson-spec.html#coordinate-reference-system-objects">geojson.org v1.0 spec</see>.
    /// </summary>
    [DataContract]
    public enum CRSType
    {
        /// <summary>
        /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#named-crs">Named</see> CRS type.
        /// </summary>
        [EnumMember(Value = "Name")]
        Name,

        /// <summary>
        /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#linked-crs">Linked</see> CRS type.
        /// </summary>
        [EnumMember(Value = "Link")]
        Link
    }
}