//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System.Runtime.Serialization;
namespace GeoObject.Net
{
    /// <summary>
    /// Defines the GeoJSON Objects types as defined in the <see cref="!:http://geojson.org/geojson-spec.html#geojson-objects">geojson.org v1.0 spec</see>.
    /// </summary>
    [DataContract]
    public enum GeoObjectType
    {
        /// <summary>
        /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#point">Point</see> type.
        /// </summary>
        [EnumMember(Value = "Point")]
        Point,

        /// <summary>
        /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#multipoint">MultiPoint</see> type.
        /// </summary>
        [EnumMember(Value = "MultiPoint")]
        MultiPoint,

        /// <summary>
        /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#linestring">LineString</see> type.
        /// </summary>
        [EnumMember(Value = "LineString")]
        LineString,

        /// <summary>
        /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#multilinestring">MultiLineString</see> type.
        /// </summary>
        [EnumMember(Value = "MultiLineString")]
        MultiLineString,

        /// <summary>
        /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#polygon">Polygon</see> type.
        /// </summary>
        [EnumMember(Value = "Polygon")]
        Polygon,

        /// <summary>
        /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#multipolygon">MultiPolygon</see> type.
        /// </summary>
        [EnumMember(Value = "MultiPolygon")]
        MultiPolygon,

        /// <summary>
        /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#geometry-collection">GeometryCollection</see> type.
        /// </summary>
        [EnumMember(Value = "GeometryCollection")]
        GeometryCollection,

        /// <summary>
        /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#feature-objects">Feature</see> type.
        /// </summary>
        [EnumMember(Value = "Feature")]
        Feature,

        /// <summary>
        /// Defines the <see cref="!:http://geojson.org/geojson-spec.html#feature-collection-objects">FeatureCollection</see> type.
        /// </summary>
        [EnumMember(Value = "FeatureCollection")]
        FeatureCollection
    }
}
