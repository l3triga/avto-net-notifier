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

        private WebParser parser;

		public MainPage()
		{
			InitializeComponent();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ParseAvtoNetSource();
        }

        public async Task ParseAvtoNetSource()
        {
            parser = new WebParser();
            bool status = await parser.LoadSourceAsync(SOURCE_URL);
            if (status)
            {
                parser.Parse();
            }
        }
	}
}
