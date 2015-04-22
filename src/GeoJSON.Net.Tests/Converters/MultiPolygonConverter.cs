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
    /// Converter to read and write the <see cref="GeoMultiPolygon" /> type.
    /// </summary>
    public class MultiPolygonConverter : JsonConverter
    {

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(GeoMultiPolygon);
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
            var o = serializer.Deserialize<JArray>(reader);
            var polygonConverter = new PolygonConverter();
            var polygons =
                o.Select(
                    polygonObject =>
                        polygonConverter.ReadJson(polygonObject.CreateReader(), typeof(GeoPolygon), polygonObject, serializer) as List<GeoLineString>)
                    .Select(lines => new GeoPolygon(lines))
                    .ToList();

            return polygons;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var polygons = (List<GeoPolygon>)value;

            writer.WriteStartArray();

            if (polygons != null && polygons.Count > 0)
            {
                for (int i = 0; i < polygons.Count; i++)
                {
                    var polygon = polygons[i];

                    // start of polygon
                    writer.WriteStartArray();

                    for (int j = 0; j < polygon.LineStrings.Count; j++)
                    {
                        var lineString = polygon.LineStrings[j];
                        var coordinateElements = lineString.Entities.OfType<GeoEntity>();
                        if (coordinateElements.Any())
                        {
                            // start linear rings of polygon
                            writer.WriteStartArray();

                            foreach (var position in coordinateElements)
                            {
                                var coordinates = position;

                                writer.WriteStartArray();

                                writer.WriteValue(coordinates.X);
                                writer.WriteValue(coordinates.Y);

                                if (coordinates.Z.HasValue)
                                {
                                    writer.WriteValue(coordinates.Z.Value);
                                }

                                writer.WriteEndArray();
                            }

                            writer.WriteEndArray();
                        }
                    }

                    writer.WriteEndArray();
                }

                writer.WriteEndArray();
            }
        }
    }
}