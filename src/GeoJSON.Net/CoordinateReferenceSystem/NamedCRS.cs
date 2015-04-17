//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Collections.Generic;

namespace GeoJSON.Net.CoordinateReferenceSystem
{
	/// <summary>
    ///     Defines the <see cref="!:http://geojson.org/geojson-spec.html#named-crs">Named CRS type</see>.
    /// </summary>
    public class NamedCRS : CRSBase, ICRSObject
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="NamedCRS" /> class.
        /// </summary>
        /// <param name="name">
        ///     The mandatory <see cref="!:http://geojson.org/geojson-spec.html#named-crs">name</see>
        ///     member must be a string identifying a coordinate reference system. OGC CRS URNs such as
        ///     'urn:ogc:def:crs:OGC:1.3:CRS84' shall be preferred over legacy identifiers such as 'EPSG:4326'.
        /// </param>
        public NamedCRS(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (name.Length == 0)
            {
                throw new ArgumentException("must be specified", "name");
            }

            Properties = new Dictionary<string, object> { { "name", name } };
            Type = CRSType.Name;
        }
    }
}