using System.Collections.Generic;
using System.Linq;

namespace GodLesZ.TotalDomination.Library.Api.Http {

    public class RequestBuilder {
        protected Dictionary<string, string> _headers = new Dictionary<string, string>();



        public void AddBaseHeaders(IEnumerable<KeyValuePair<string, string>> headers) {
            headers
                .ToList()
                .ForEach(kvp => _headers.Add(kvp.Key, kvp.Value));
        }


        public void Post(string data, IEnumerable<KeyValuePair<string, string>> additionalHeaders = null) {
            var headers = _headers;
            if (additionalHeaders != null) {
                additionalHeaders
                    .ToList()
                    .ForEach(kvp => headers.Add(kvp.Key, kvp.Value));
            }

        }
         
    }

}