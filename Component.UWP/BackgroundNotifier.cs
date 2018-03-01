using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using System.Xml.Linq;
using Windows.Storage;
using AvtoNetLibrary.Parser;
using AvtoNetLibrary.Web;

namespace Component.UWP
{
    public sealed class BackgroundNotifier : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;

        private AvtoNetSearchParser Parser;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
            var queryStringBuilder = new QueryStringBuilder(settings.Values);
            queryStringBuilder.Build();

            Parser = new AvtoNetSearchParser
            {
                QueryString = queryStringBuilder.ToString()
            };

            do
            {
                bool status = await Parser.LoadSourceAsync(Encoding.GetEncoding(1250));
                if (status)
                {
                    Parser.Parse();
                    //GenerateToastNotification();
                }
            } while (1 == 2);

            _deferral.Complete();
        }

        private void GenerateToastNotification()
        {
            var xDoc = new XDocument(
                new XElement("toast",
                new XElement("visual",
                new XElement("binding", new XAttribute("template", "ToastGeneric"),
                new XElement("text", "C# Corner"),
                new XElement("text", "Do you got MVP award?")
                )
                ),// actions  
                new XElement("actions",
                new XElement("action", new XAttribute("activationType", "background"),
                new XAttribute("content", "Yes"), new XAttribute("arguments", "yes")),
                new XElement("action", new XAttribute("activationType", "background"),
                new XAttribute("content", "No"), new XAttribute("arguments", "no"))
                )
                )
            );

            var xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(xDoc.ToString());
            var toast = new ToastNotification(xmlDoc);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
