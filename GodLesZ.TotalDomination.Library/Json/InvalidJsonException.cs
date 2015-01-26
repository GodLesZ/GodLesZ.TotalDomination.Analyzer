using System;

namespace GodLesZ.TotalDomination.Analyzer.Library.Json {

    /// <summary>
    /// Exception raised when <see cref="JsonParser" /> encounters an invalid token.
    /// </summary>
    public class InvalidJsonException : Exception {
        public InvalidJsonException(string message)
            : base(message) {

        }
    }

}