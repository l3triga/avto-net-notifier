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

        public override void Parse()
        {
            ParseBrandsAndModels();
        }

        public void ParseBrandsAndModels()
        {
            Dictionary<CarBrand, List<CarModel>> dictionary = 
                new Dictionary<CarBrand, List<CarModel>>(new CarBrand.EqualityComparer());

            HtmlNodeCollection nodes = GetSelectNodeByName("model");
            foreach (var node in nodes)
            {
                if (node.NodeType == HtmlNodeType.Element)
                {
                    var modelValue = node.Attributes["value"].Value;
                    if (String.IsNullOrEmpty(modelValue))
                    {
                        continue;
                    }
                    var brandValue = node.Attributes["class"].Value;

                    CarBrand brand = new CarBrand(brandValue);
                    CarModel model = new CarModel(modelValue);

                    if (dictionary.ContainsKey(brand))
                    {
                        dictionary[brand].Add(model);
                    }
                    else
                    {
                        dictionary.Add(brand, new List<CarModel> { model });
                    }
                }
            }

            foreach (var item in dictionary)
            {
                CarBrand brand = item.Key;
                brand.Models = item.Value;
                CarConfigurator.Brands.Add(brand);
            }
        }

        private HtmlNodeCollection GetSelectNodeByName(string name)
        {
            var selectNode = document.DocumentNode.SelectSingleNode("//select[@name='" + name + "']");
            return selectNode.ChildNodes;
        }
    }
}
