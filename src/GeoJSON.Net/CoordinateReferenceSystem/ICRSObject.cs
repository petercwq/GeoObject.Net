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
    /// Base Interface for CRSBase Object types.
    /// </summary>
    public interface ICRSObject
    {

        /// <summary>
        /// Gets the CRS type.
        /// </summary>
        CRSType Type { get; }
    }
}
