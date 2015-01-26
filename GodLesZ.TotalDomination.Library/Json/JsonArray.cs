using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace GodLesZ.TotalDomination.Analyzer.Library.Json {

    public class JsonArray : DynamicObject, IEnumerable, IJson {
        private readonly List<IJson> _collection;

        public JsonArray(ICollection<object> collection) {
            _collection = new List<IJson>(collection.Count);
            foreach (var instance in collection.Cast<IDictionary<string, object>>()) {
                _collection.Add(new JsonObject(instance));
            }
        }

        public IEnumerator GetEnumerator() {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString() {
            var buffer = new StringBuilder();
            JsonParser.SerializeArray(_collection, buffer);
            return buffer.ToString();
        }

    }

}