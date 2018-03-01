using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AvtoNetLibrary.Model;
using HtmlAgilityPack;
using AvtoNetLibrary.Constants;

namespace AvtoNetLibrary.Parser
{
    public class AvtoNetSearchParser : WebParser
    {
        public override string SourceURL
        {
            get
            {
                return DomainConstants.ResultURL + "?" + QueryString;
            }
        }

        public string QueryString { get; set; }
        public List<CarSummary> CarOffers { get; }

        public AvtoNetSearchParser()
        {
            QueryString = "";
            CarOffers = new List<CarSummary>();
        }

        public AvtoNetSearchParser(string queryString)
        {
            QueryString = queryString;
            CarOffers = new List<CarSummary>();
        }

        public override void Parse()
        {
            Regex idRegex = new Regex(@"/(\d+)/");
            Regex priceRegex = new Regex(@"\s+");

            HtmlNodeCollection resultNodes = document.DocumentNode.SelectNodes("//div[@class='ResultsAd']");
            foreach (var node in resultNodes)
            {
                CarSummary summary = new CarSummary();

                HtmlNode linkNode = node.SelectSingleNode("//div[@class='ResultsAdData']/a[@class='Adlink']");
                summary.URL = linkNode.Attributes["href"].Value.Replace("..", DomainConstants.FullHostname);
                summary.Name = linkNode.ChildNodes[1].InnerText;

                HtmlNodeCollection listNodes = node.SelectNodes("//div[@class='ResultsAdData']/ul/li");
                summary.RegistrationDate = listNodes[0].InnerText;
                summary.Kilometers = listNodes[1].InnerText;
                summary.Engine = listNodes[2].InnerText;
                summary.Transmission = listNodes[3].InnerText;

                summary.ImageSource = ParseAttribute(node, "//div[@class='ResultsAdPhotoTop']//img", "src");
                Match regexMatch = idRegex.Match(summary.ImageSource);
                if(regexMatch.Groups.Count == 2)
                    summary.ID = Convert.ToUInt32(regexMatch.Groups[1].Value);

                summary.Price = priceRegex.Replace(ParseText(node, "//div[@class='ResultsAdPrice']"), String.Empty);
  
                CarOffers.Add(summary);
            }
        }
    }
}
