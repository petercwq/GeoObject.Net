//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace GeoJSON.Net.Geometry
{
    public static class WktExts
    {
        static readonly IFormatProvider ci = CultureInfo.InvariantCulture;

        public static string ToWkt(this IGeoObject geometry)
        {
            if (geometry is GeoPoint)
            {
                string point = GeometryToWkt((GeoPoint)geometry);

                return string.Format("POINT({0})", point);
            }

            if (geometry is GeoLineString)
            {
                string linestring = GeometryToWkt((GeoLineString)geometry);

                return string.Format("LINESTRING({0})", linestring);
            }

            if (geometry is GeoPolygon)
            {
                string polygon = GeometryToWkt((GeoPolygon)geometry);

                return string.Format("POLYGON({0})", polygon);
            }

            if (geometry is GeoMultiPoint)
            {
                string point = GeometryToWkt((GeoMultiPoint)geometry);

                return string.Format("MULTIPOINT({0})", point);
            }

            if (geometry is GeoMultiLineString)
            {
                string multiLineString = GeometryToWkt((GeoMultiLineString)geometry);

                return string.Format("MULTILINESTRING({0})", multiLineString);
            }

            if (geometry is GeoMultiPolygon)
            {
                string multiPolygon = GeometryToWkt((GeoMultiPolygon)geometry);

                return string.Format("MULTIPOLYGON({0})", multiPolygon);
            }

            if (geometry is GeoCollection)
            {
                return string.Format("GEOMETRYCOLLECTION({0})", string.Join(",", ((GeoCollection)geometry).Geometries.Select(x => x.ToWkt())));
            }

            return null;
        }

        static string GeometryToWkt(IGeoEntity position)
        {
            if (position.Z.HasValue)
                return string.Format(ci, "{0} {1} {2}", position.X, position.Y, position.Z);
            else
                return string.Format(ci, "{0} {1}", position.X, position.Y);
        }

        static string GeometryToWkt(GeoPoint point)
        {
            return GeometryToWkt(point.Coordinates);
        }

        static string GeometryToWkt(GeoLineString lineString)
        {
            return string.Join(",", lineString.Coordinates.Select(GeometryToWkt));
        }

        static string GeometryToWkt(GeoPolygon polygon)
        {
            return string.Format("({0})", string.Join("),(", polygon.Coordinates.Select(GeometryToWkt)));
        }

        static string GeometryToWkt(GeoMultiPoint multiPoint)
        {
            return string.Format("({0})", string.Join(",", multiPoint.Coordinates.Select(GeometryToWkt)));
        }

        static string GeometryToWkt(GeoMultiLineString multiLine)
        {
            return string.Format("({0})", string.Join("),(", multiLine.Coordinates.Select(GeometryToWkt)));
        }

        static string GeometryToWkt(GeoMultiPolygon multiPolygon)
        {
            return string.Format("({0})", string.Join("),(", multiPolygon.Coordinates.Select(GeometryToWkt)));
        }

        /// <summary>
        /// Initialize a new IGemotry object from a standard WKT geometry
        /// </summary>
        /// <param name="wkt">The geometry in WKT to convert</param>
        public static IGeoObject ToGeometry(this string wkt)
        {
            wkt = wkt.Trim().Replace(", ", ",");
            Match match = Regex.Match(wkt, @"^([A-Z]+)\s*\((.+)\)$");
            if (match.Success)
            {
                switch (match.Groups[1].Value)
                {
                    case "POINT":
                        return WktToPoint(match.Groups[2].Value);
                    case "LINESTRING":
                        return WktToLineString(match.Groups[2].Value);
                    case "POLYGON":
                        return WktToPolygon(match.Groups[2].Value);
                    case "MULTIPOINT":
                        return WktToMultiPoint(match.Groups[2].Value);
                    case "MULTILINESTRING":
                        return WktToMultiLineString(match.Groups[2].Value);
                    case "MULTIPOLYGON":
                        return WktToMultiPolygon(match.Groups[2].Value);
                    case "GEOMETRYCOLLECTION":
                        var iterms = Regex.Matches(match.Groups[2].Value, @"\w+[\s\d,.\+\-\(\)]+\)");
                        List<IGeoObject> objects = new List<IGeoObject>();
                        foreach (Match item in iterms)
                        {
                            objects.Add(item.Value.ToGeometry());
                        }
                        return new GeoCollection(objects);
                }
            }
            throw new NotImplementedException(match.Groups[1].Value);
        }

        /// <summary>
        /// Point from WK.
        /// </summary>
        /// <returns>The Point</returns>
        /// <param name="wkt">WKT.</param>
        static GeoPoint WktToPoint(string wkt)
        {
            string[] values;
            values = wkt.Trim('(', ')').Split(' ');
            string z = (values.Length > 2 ? values[2] : null);
            var geopos = new GeoEntity(values[0], values[1], z);
            return new GeoPoint(geopos);
        }


        /// <summary>
        /// GeoLineString from WK.
        /// </summary>
        /// <returns>The GeoLineString</returns>
        /// <param name="wkt">WKT.</param>
        static GeoLineString WktToLineString(string wkt)
        {
            string[] terms = wkt.Split(',');
            string[] values;
            List<IGeoEntity> positions = new List<IGeoEntity>(terms.Length);
            for (int i = 0; i < terms.Length; i++)
            {
                values = terms[i].Split(' ');
                string z = (values.Length > 2 ? values[2] : null);
                var geopos = new GeoEntity(values[1], values[0], z);
                positions.Add(geopos);
            }

            GeoLineString test = new GeoLineString(positions);
            return test;
        }

        /// <summary>
        /// GeoPolygon from WK.
        /// </summary>
        /// <returns>The GeoPolygon</returns>
        /// <param name="wkt">WKT.</param>
        static GeoPolygon WktToPolygon(string wkt)
        {
            MatchCollection matches = Regex.Matches(wkt, @"\(([^\)]+)\)");
            List<GeoLineString> linestrings = new List<GeoLineString>(matches.Count);
            for (int i = 0; i < matches.Count; i++)
            {
                GeoLineString linestring = WktToLineString(matches[i].Groups[1].Value);
                linestrings.Add(linestring);
            }

            return new GeoPolygon(linestrings);
        }

        /// <summary>
        /// GeoMultiPoint from WK.
        /// </summary>
        /// <returns>The GeoMultiPoint</returns>
        /// <param name="wkt">WKT.</param>
        static GeoMultiPoint WktToMultiPoint(string wkt)
        {
            string[] terms = wkt.Split(',');
            return new GeoMultiPoint(terms.Select(WktToPoint).ToList());
        }

        /// <summary>
        /// MultiLineString from WK.
        /// </summary>
        /// <returns>The MultiLineString</returns>
        /// <param name="wkt">WKT.</param>
        static GeoMultiLineString WktToMultiLineString(string wkt)
        {
            MatchCollection matches = Regex.Matches(wkt, @"\(([^\)]+)\)");
            List<GeoLineString> linestrings = new List<GeoLineString>(matches.Count);
            for (int i = 0; i < matches.Count; i++)
            {
                GeoLineString linestring = WktToLineString(matches[i].Groups[1].Value);
                linestrings.Add(linestring);
            }

            return new GeoMultiLineString(linestrings);
        }

        /// <summary>
        /// MultiPolygon from WK.
        /// </summary>
        /// <returns>The MultiPolygon</returns>
        /// <param name="wkt">WKT.</param>
        static GeoMultiPolygon WktToMultiPolygon(string wkt)
        {
            MatchCollection matches = Regex.Matches(wkt, @"\(((\([\s\d,.\+\-]+\)(,\s*)?)+)\)");
            List<GeoPolygon> polygons = new List<GeoPolygon>(matches.Count);
            for (int i = 0; i < matches.Count; i++)
            {
                GeoPolygon polygon = WktToPolygon(matches[i].Groups[1].Value);
                polygons.Add(polygon);
            }

            return new GeoMultiPolygon(polygons);
        }
    }
}
