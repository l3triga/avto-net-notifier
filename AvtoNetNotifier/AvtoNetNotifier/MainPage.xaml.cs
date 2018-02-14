using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AvtoNetNotifier
{
	public partial class MainPage : ContentPage
	{
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

        private async Task ParseAvtoNetSource()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            bool status = await Parser.LoadSourceAsync(AvtoNetParser.SOURCE_URL, Encoding.GetEncoding(1250));
            if (status)
            {
                Parser.Parse();
            }
        }
    }
}
