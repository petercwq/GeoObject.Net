//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

namespace GeoJSON.Net.CoordinateReferenceSystem
{

    /// <summary>
    ///     Defines the GeoJSON Coordinate Reference System Objects (CRS) types as defined in the
    ///     <see cref="!:http://geojson.org/geojson-spec.html#coordinate-reference-system-objects">geojson.org v1.0 spec</see>.
    /// </summary>
    public enum CRSType
    {
        /// <summary>
        ///     Defines a CRS type where the CRS cannot be assumed
        /// </summary>
        Unspecified,

        /// <summary>
        ///     Defines the <see cref="!:http://geojson.org/geojson-spec.html#named-crs">Named</see> CRS type.
        /// </summary>
        Name,

        /// <summary>
        ///     Defines the <see cref="!:http://geojson.org/geojson-spec.html#linked-crs">Linked</see> CRS type.
        /// </summary>
        Link
    }
}