using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using AvtoNetLibrary.Serializer;
using AvtoNetLibrary.Model;

namespace AvtoNetLibrary.Web
{
    public class QueryStringBuilder
    {
        public IDictionary<string, object> QueryDictionary { get; set; }

        private NameValueCollection QueryCollection;

        public QueryStringBuilder()
        {
            QueryDictionary = new Dictionary<string, object>();
            QueryCollection = new NameValueCollection();
        }

        public QueryStringBuilder(IDictionary<string, object> dictionary)
        {
            QueryDictionary = dictionary;
            QueryCollection = new NameValueCollection();
        }

        public void Build()
        {
            foreach (var queryItem in QueryDictionary)
            {
                switch (queryItem.Key)
                {
                    case "star1":
                    case "star2":
                    case "star4":
                        break;

                    case "znamka":
                        break;

                    default:
                        //var attribute = ObjectSerializer.Deserialize<CarAttribute<string>>((string)queryItem.Value);
                        //QueryCollection.Add(queryItem.Key, attribute.Value);
                        break;
                }
            }
        }

        public override string ToString()
        {
            return QueryCollection.ToString();
        }
    }
}
