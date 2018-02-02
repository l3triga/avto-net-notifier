using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;

namespace AvtoNetNotifier
{
    abstract class WebParser
    {
        protected HtmlDocument document;

        public WebParser()
        {
            document = new HtmlDocument();
        }

        public async Task<bool> LoadSourceAsync(string URL, Encoding encoding)
        {
            WebClient webClient = new WebClient();
            var data = await webClient.DownloadDataTaskAsync(new Uri(URL));
            var html = encoding.GetString(data);

            document.LoadHtml(html);

            return !String.IsNullOrEmpty(document.ParsedText);
        }

        abstract public void Parse();
    }
}
