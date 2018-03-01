using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetLibrary.Constants
{
    public static class DomainConstants
    {
        public const string Protocol = "https";
        public const string Host = "avto.net";
        public const string Hostname = "www.avto.net";
        public const string FullHostname = Protocol + "://" + Hostname;

        public const string FilterURL = FullHostname + "/Ads/search.asp?SID=10000";
        public const string ResultURL = FullHostname + "/Ads/results.asp";
    }
}
