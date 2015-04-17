//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Converters;
using Newtonsoft.Json;

namespace GeoJSON.Net.Geometry
{

    /// <summary>
    ///     Defines the <see cref="!:http://geojson.org/geojson-spec.html#multilinestring">MultiLineString</see> type.
    /// </summary>
    public class MultiLineString : GeoJSONObject, IGeometryObject
    {

        /// <summary>
        ///     Gets the Coordinates.
        /// </summary>
        /// <value>The Coordinates.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PolygonConverter))]
        public List<LineString> Coordinates { get; private set; }

        protected bool Equals(MultiLineString other)
        {
            return base.Equals(other) && Coordinates.SequenceEqual(other.Coordinates);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiLineString" /> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public MultiLineString(List<LineString> coordinates)
        {
            Coordinates = coordinates ?? new List<LineString>();
            Type = GeoJSONObjectType.MultiLineString;
        }

        public static bool operator !=(MultiLineString left, MultiLineString right)
        {
            return !Equals(left, right);
        }

        public static bool operator ==(MultiLineString left, MultiLineString right)
        {
            return Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((MultiLineString)obj);
        }

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }
    }
}