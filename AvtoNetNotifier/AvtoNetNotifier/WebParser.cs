using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AvtoNetNotifier
{
    class WebParser
    {
        protected HtmlDocument document;

        public async Task<bool> LoadSourceAsync(string URL)
        {
            var webHTML = new HtmlWeb();
            document = await webHTML.LoadFromWebAsync(URL);

            return !String.IsNullOrEmpty(document.ParsedText);
        }
    }
}
