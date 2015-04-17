//  Author: Weiqing Chen <kevincwq@gmail.com>
//
//  Copyright (c) 2015 Weiqing Chen
//
//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

using System;

namespace GeoJSON.Net.Exceptions
{
    /// <summary>
    /// Exception raised when response from SimpleGeo API cannot be parsed
    /// </summary>
    public class ParsingException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ParsingException(string message = "Could not parse GeoJSON Response.", Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
