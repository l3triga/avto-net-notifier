using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AvtoNetNotifier
{
	public partial class MainPage : ContentPage
	{
        public const string SOURCE_URL = "https://www.avto.net/Ads/search.asp?SID=10000";

        private AvtoNetParser Parser;

		public MainPage()
		{
			InitializeComponent();
            Parser = new AvtoNetParser();

            BindingContext = Parser.CarConfigurator;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ParseAvtoNetSource();
        }

        public async Task ParseAvtoNetSource()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            bool status = await Parser.LoadSourceAsync(AvtoNetParser.SOURCE_URL, Encoding.GetEncoding(1252));
            if (status)
            {
                Parser.Parse();
            }
        }
	}
}
