//  Author:
//       Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

namespace GeoJSON.Net.CoordinateReferenceSystem
{
	/// <summary>
    /// Represents an unspecified Coordinate Reference System 
    /// i.e. where a geojson object has a null crs
    /// </summary>
    public class UnspecifiedCRS : ICRSObject
    {

        /// <summary>
        /// Gets the CRS type.
        /// </summary>
        public CRSType Type
        {
            get
            {
                return CRSType.Unspecified;
            }
        }
    }
}