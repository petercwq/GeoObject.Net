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
    /// A position is the fundamental geometry construct. 
    /// The "coordinates" member of a geometry object is composed of one position (in the case of a Point geometry)
    /// , an array of positions (LineString or MultiPoint geometries), 
    /// an array of arrays of positions (Polygons, MultiLineStrings), 
    /// or a multidimensional array of positions (MultiPolygon).
    /// </summary>
    public class Position : IPosition
    { }
}