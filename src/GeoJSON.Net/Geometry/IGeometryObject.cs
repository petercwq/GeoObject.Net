//  Author:
//       Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

namespace GeoJSON.Net.Geometry
{
	/// <summary>
    /// Base Interface for GeometryObject types.
    /// </summary>
    public interface IGeometryObject
    {

        /// <summary>
        /// Gets the (mandatory) type of the <see cref="!:http://geojson.org/geojson-spec.html#geometry-objects">GeoJSON Object</see>.
        /// However, for <see cref="!:http://geojson.org/geojson-spec.html#geometry-objects">GeoJSON Objects</see> only
        /// the 'Point', 'MultiPoint', 'LineString', 'MultiLineString', 'Polygon', 'MultiPolygon', or 'GeometryCollection' types are allowed.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        GeoJSONObjectType Type { get; }
    }
}
