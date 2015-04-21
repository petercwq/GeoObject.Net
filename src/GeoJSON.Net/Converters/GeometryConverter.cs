//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoJSON.Net.Converters
{
	/// <summary>
    /// Converts <see cref="IGeoObject"/> types to and from JSON.
    /// </summary>
    public class GeometryConverter : JsonConverter
    {
        /// <summary>
        /// Reads the geo json.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="Newtonsoft.Json.JsonReaderException">
        /// json must contain a "type" property
        /// or
        /// type must be a valid geojson geometry object type
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// Feature and FeatureCollection types are Feature objects and not Geometry objects
        /// </exception>
        private static IGeoObject ReadGeoJson(JObject value)
        {
            JToken token;

            if (!value.TryGetValue("type", StringComparison.OrdinalIgnoreCase, out token))
            {
                throw new JsonReaderException("json must contain a \"type\" property");
            }

            GeoObjectType geoJsonType;

            if (!Enum.TryParse(token.Value<string>(), true, out geoJsonType))
            {
                throw new JsonReaderException("type must be a valid geojson geometry object type");
            }

            switch (geoJsonType)
            {
                case GeoObjectType.Point:
                    return value.ToObject<GeoPoint>();
                case GeoObjectType.MultiPoint:
                    return value.ToObject<GeoMultiPoint>();
                case GeoObjectType.LineString:
                    return value.ToObject<GeoLineString>();
                case GeoObjectType.MultiLineString:
                    return value.ToObject<GeoMultiLineString>();
                case GeoObjectType.Polygon:
                    return value.ToObject<GeoPolygon>();
                case GeoObjectType.MultiPolygon:
                    return value.ToObject<GeoMultiPolygon>();
                case GeoObjectType.GeometryCollection:
                    return value.ToObject<GeoCollection>();
                case GeoObjectType.Feature:
                case GeoObjectType.FeatureCollection:
                default:
                    throw new NotSupportedException("Feature and FeatureCollection types are Feature objects and not Geometry objects");
            }
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(IGeoObject).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return null;
                case JsonToken.StartObject:
                    var value = JObject.Load(reader);
                    return ReadGeoJson(value);
                case JsonToken.StartArray:
                    var values = JArray.Load(reader);
                    var geometries = new List<IGeoObject>(values.Count);
                    geometries.AddRange(values.Cast<JObject>().Select(ReadGeoJson));
                    return geometries;
            }

            throw new JsonReaderException("expected null, object or array token but received " + reader.TokenType);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}