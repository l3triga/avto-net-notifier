using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using AvtoNetLibrary.Serializer;
using AvtoNetLibrary.Model;
using System.Web;

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
            int EQ7Initial = 0b111;

            foreach (var queryItem in QueryDictionary)
            {
                switch (queryItem.Key)
                {
                    case "star1":
                        if (!(bool)queryItem.Value)
                            EQ7Initial &= 0b101;
                        break;

                    case "star2":
                        if (!(bool)queryItem.Value)
                            EQ7Initial &= 0b110;
                        break;

                    case "star4":
                        if (!(bool)queryItem.Value)
                            EQ7Initial &= 0b011;
                        break;

                    case "znamka":
                        var attributeBrand = ObjectSerializer.Deserialize<CarBrand>((string)queryItem.Value);
                        QueryCollection.Add(queryItem.Key, attributeBrand.Value);
                        break;

                    case "model":
                    case "prodajalec":
                    case "lokacija":
                        var attributeString = ObjectSerializer.Deserialize<CarAttribute<string>>((string)queryItem.Value);
                        QueryCollection.Add(queryItem.Key, attributeString.Value);
                        break;

                    case "cenaMin":
                    case "cenaMax":
                    case "letnikMin":
                    case "letnikMax":
                    case "kmMIN":
                    case "kmMax":
                        var attributeUint = ObjectSerializer.Deserialize<CarAttribute<uint>>((string)queryItem.Value);
                        QueryCollection.Add(queryItem.Key, Convert.ToString(attributeUint.Value));
                        break;
                }
            }

            QueryCollection.Add("EQ7", Convert.ToString(EQ7Initial, 2) + "0100120");
        }

        public override string ToString()
        {
            List<string> queryItems = new List<string>();
            foreach (string name in QueryCollection)
            {
                queryItems.Add(String.Concat(name, "=", HttpUtility.UrlEncode(QueryCollection[name])));
            }

            return String.Join("&", queryItems);
        }
    }
}
