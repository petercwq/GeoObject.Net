//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Collections.Generic;
using GeoObject.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoObject.Net.Converters
{
	/// <summary>
    /// Converter to read and write the <see cref="GeoLineString" /> type.
    /// </summary>
    public class LineStringConverter : JsonConverter
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
            return objectType == typeof(GeoLineString);
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
            double[][] coordinates = existingValue == null
                ? serializer.Deserialize<double[][]>(reader)
                : (double[][])existingValue;

            IList<IGeoEntity> positions = new List<IGeoEntity>(coordinates.Length);

            foreach (var coordinate in coordinates)
            {
                var x = coordinate[0];
                var y = coordinate[1];
                double? z = null;

                if (coordinate.Length == 3)
                {
                    z = coordinate[2];
                }

                positions.Add(new GeoEntity(y, x, z));
            }

            return positions;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var coordinateElements = value as List<IGeoEntity>;
            if (coordinateElements != null && coordinateElements.Count > 0)
            {
                var coordinateArray = new JArray();

                foreach (var position in coordinateElements)
                {
                    // TODO: position types should expose a double[] coordinates property that can be used to write values 
                    var coordinates = (GeoEntity)position;
                    var coordinateElement = new JArray(coordinates.X, coordinates.Y);
                    if (coordinates.Z.HasValue)
                    {
                        coordinateElement = new JArray(coordinates.X, coordinates.Y, coordinates.Z);
                    }

                    coordinateArray.Add(coordinateElement);
                }

                serializer.Serialize(writer, coordinateArray);
            }
            else
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}