//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System.Runtime.Serialization;
namespace GeoObject.Net.CoordinateReferenceSystem
{
	/// <summary>
    /// The default CRS is a geographic coordinate reference system,
    /// using the WGS84 datum, and with x and y units of decimal degrees.
    /// see http://geojson.org/geojson-spec.html#coordinate-reference-system-objects
    /// </summary>
    [DataContract]
    public class DefaultCRS : NamedCRS
    {
        /// <summary>
        /// The CRS
        /// </summary>
        private static readonly DefaultCRS Crs = new DefaultCRS();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static DefaultCRS Instance
        {
            get { return Crs; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCRS" /> class.
        /// </summary>
        private DefaultCRS()
            : base("urn:ogc:def:crs:OGC::CRS84")
        {
        }
    }
}