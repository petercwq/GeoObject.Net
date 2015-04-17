//  Author:
//       Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Exceptions;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace GeoJSON.Net.Converters
{
	/// <summary>
    /// 
    /// </summary>
    public class MultiPointConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MultiPoint);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var coordinates = serializer.Deserialize<double[][]>(reader);
            var positions = new List<Point>(coordinates.Length);
            try
            {
                foreach (var coordinate in coordinates)
                {
                    var x = coordinate[0];
                    var y = coordinate[1];
                    double? z = null;

                    if (coordinate.Length == 3)
                    {
                        z = coordinate[2];
                    }

                    positions.Add(new Point(new Position(y, x, z)));
                }

                return positions;
            }
            catch (Exception ex)
            {
                throw new ParsingException("Could not parse GeoJSON Response. (Y or X missing from Point geometry?)", ex);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var points = (List<Point>)value;

            if (points.Any())
            {
                var converter = new PointConverter();

                writer.WriteStartArray();

                foreach (var point in points)
                {
                    converter.WriteJson(writer, point.Coordinates, serializer);
                }

                writer.WriteEndArray();
            }
        }
    }
}