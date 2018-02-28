using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;

namespace AvtoNetLibrary.Parser
{
    public abstract class WebParser
    {
        public abstract string SourceURL { get; }

        protected HtmlDocument document;

        public WebParser()
        {
            document = new HtmlDocument();
        }

        public async Task<bool> LoadSourceAsync(Encoding encoding)
        {
            WebClient webClient = new WebClient();
            var data = await webClient.DownloadDataTaskAsync(new Uri(SourceURL));
            var html = encoding.GetString(data);

            document.LoadHtml(html);

            return !String.IsNullOrEmpty(document.ParsedText);
        }

        abstract public void Parse();
    }
}
