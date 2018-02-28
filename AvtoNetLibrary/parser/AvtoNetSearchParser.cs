using System;
using System.Collections.Generic;
using AvtoNetLibrary.Model;

namespace AvtoNetLibrary.Parser
{
    public class AvtoNetSearchParser : WebParser
    {
        public override string SourceURL
        {
            get
            {
                return "https://www.avto.net/Ads/results.asp?" + QueryString;
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
            throw new NotImplementedException();
        }

        public CarSummary ParseNext()
        {
            throw new NotImplementedException();
        }
    }
}
