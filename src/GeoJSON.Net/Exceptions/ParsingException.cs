//  Adapted from GeoJSON.Net https://github.com/jbattermann/GeoJSON.Net
//  Copyright © 2014 Jörg Battermann & Other Contributors

namespace GeoJSON.Net.Exceptions
{

    #region __CodeSorter_unsorted
    using System;
    #endregion

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
