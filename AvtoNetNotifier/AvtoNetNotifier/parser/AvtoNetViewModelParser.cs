using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;
using AvtoNetLibrary.Parser;
using AvtoNetLibrary.Model;
using AvtoNetLibrary.Constants;

namespace AvtoNetNotifier
{
    class AvtoNetViewModelParser : WebParser
    {
        public override string SourceURL {
            get {
                return DomainConstants.FilterURL;
            }
        }

        public CarConfigurator CarConfigurator { get; }

        public AvtoNetViewModelParser()
        {
            CarConfigurator = new CarConfigurator();
        }

        public override void Parse()
        {
            ParseBrandsAndModels();
            ParsePrices();
            ParseAges();
            ParseKilometers();
            ParseSellers();

            CarConfigurator.IsInitialized = true;
        }

        public void ParseBrandsAndModels()
        {
            Dictionary<CarBrand, List<CarAttribute<string>>> dictionary = 
                new Dictionary<CarBrand, List<CarAttribute<string>>>(new CarBrand.EqualityComparer());

            HtmlNode defaultBrandNode = document.DocumentNode.
                SelectSingleNode("//select[@name='znamka']/option[@value='']");
            CarBrand defaultBrand = new CarBrand(defaultBrandNode.Attributes["value"].Value, 
                defaultBrandNode.InnerText);

            CarAttribute<string> defaultModel = null;

            HtmlNodeCollection nodes = GetSelectNodeByName("model");
            foreach (var node in nodes)
            {
                if (node.NodeType == HtmlNodeType.Element)
                {
                    if (defaultModel == null)
                    {
                        defaultModel = new CarAttribute<string>(node.Attributes["value"].Value, node.InnerText);
                        dictionary.Add(defaultBrand, new List<CarAttribute<string>> { defaultModel });
                        continue;
                    }

                    var modelValue = node.Attributes["value"].Value;
                    var brandValue = node.Attributes["class"].Value;

                    CarBrand brand = new CarBrand(brandValue);
                    CarAttribute<string> model = new CarAttribute<string>(modelValue, modelValue);

                    if (dictionary.ContainsKey(brand))
                    {
                        dictionary[brand].Add(model);
                    }
                    else
                    {
                        dictionary.Add(brand, new List<CarAttribute<string>> { defaultModel, model });
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

        public void ParsePrices()
        {
            HtmlNodeCollection nodesMin = GetSelectNodeByName("cenaMin");
            CarConfigurator.MinPrices.AddRange(ParseGenericAttribute<uint>(nodesMin));

            HtmlNodeCollection nodesMax = GetSelectNodeByName("cenaMax");
            CarConfigurator.MaxPrices.AddRange(ParseGenericAttribute<uint>(nodesMax));
        }

        public void ParseAges()
        {
            HtmlNodeCollection nodesMin = GetSelectNodeByName("letnikMin");
            CarConfigurator.MinAges.AddRange(ParseGenericAttribute<uint>(nodesMin));

            HtmlNodeCollection nodesMax = GetSelectNodeByName("letnikMax");
            CarConfigurator.MaxAges.AddRange(ParseGenericAttribute<uint>(nodesMax));
        }

        public void ParseKilometers()
        {
            HtmlNodeCollection nodesMin = GetSelectNodeByName("kmMIN");
            CarConfigurator.MinKilometers.AddRange(ParseGenericAttribute<uint>(nodesMin));

            HtmlNodeCollection nodesMax = GetSelectNodeByName("kmMax");
            CarConfigurator.MaxKilometers.AddRange(ParseGenericAttribute<uint>(nodesMax));
        }

        public void ParseSellers()
        {
            HtmlNodeCollection nodesSeller = GetSelectNodeByName("prodajalec");
            CarConfigurator.SellerTypes.AddRange(ParseGenericAttribute<string>(nodesSeller));

            HtmlNodeCollection nodesLocation = GetSelectNodeByName("lokacija");
            CarConfigurator.SellerLocations.AddRange(ParseGenericAttribute<string>(nodesLocation));
        }

        private List<CarAttribute<T>> ParseGenericAttribute<T>(HtmlNodeCollection nodes)
        {
            List<CarAttribute<T>> collection = new List<CarAttribute<T>>();
            foreach (var node in nodes)
            {
                if (node.NodeType == HtmlNodeType.Element)
                {
                    var value = node.Attributes["value"].Value;
                    var text = node.InnerText;

                    CarAttribute<T> attribute = new CarAttribute<T>((T)Convert.ChangeType(value, typeof(T)), text);
                    collection.Add(attribute);
                }
            }
            return collection;
        }

        private HtmlNodeCollection GetSelectNodeByName(string name)
        {
            var selectNode = document.DocumentNode.SelectSingleNode("//select[@name='" + name + "']");
            return selectNode.ChildNodes;
        }
    }
}
