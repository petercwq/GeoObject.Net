//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GeoJSON.Net.Feature
{
    /// <summary>
    /// Defines the FeatureCollection type.
    /// </summary>
    [DataContract]
    public class FeatureCollection : GeoObject
    {
        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>The features.</value>
        //[JsonProperty(PropertyName = "features", Required = Required.Always)]
        [DataMember(Name = "features", IsRequired = true, EmitDefaultValue = true)]
        public List<Feature> Features { get; private set; }

        public FeatureCollection()
        {
            Type = GeoObjectType.FeatureCollection;
            Features = new List<Feature>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureCollection" /> class.
        /// </summary>
        /// <param name="features">The features.</param>
        public FeatureCollection(List<Feature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException("features");
            }

            Features = features;
            Type = GeoObjectType.FeatureCollection;
        }
    }
}