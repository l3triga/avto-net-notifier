using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace AvtoNetNotifier
{
    class AvtoNetParser : WebParser
    {
        public static readonly string SOURCE_URL = "https://www.avto.net/Ads/search.asp?SID=10000";

        public CarConfigurator CarConfigurator { get; }

        public AvtoNetParser()
        {
            CarConfigurator = new CarConfigurator();
        }

        public void ParseBrands()
        {
            var brandNode = document.DocumentNode.SelectSingleNode("//select[@name='znamka']");
            HtmlNodeCollection childNodes = brandNode.ChildNodes;

            foreach (var node in childNodes)
            {
                if (node.NodeType == HtmlNodeType.Element)
                {
                    var id = node.Attributes[0].Value;
                    if (String.IsNullOrEmpty(id))
                    {
                        continue;
                    }

                    var brand = node.InnerText;
                    CarConfigurator.CarBrands.Add(new CarBrand(id, brand));
                }
            }
        }
    }
}
