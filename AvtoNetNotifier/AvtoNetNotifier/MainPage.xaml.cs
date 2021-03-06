﻿using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using AvtoNetLibrary.Constants;

namespace AvtoNetNotifier
{
	public partial class MainPage : ContentPage
	{
        private IConnectivity Connectivity;

        private AvtoNetViewModelParser Parser;

		public MainPage()
		{
			InitializeComponent();
            Connectivity = CrossConnectivity.Current;
            Parser = new AvtoNetViewModelParser();

            BindingContext = Parser.CarConfigurator;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            bool isConnected = await IsAvtoNetReachable();
            if (isConnected)
                await ParseAvtoNetSource();

            Connectivity.ConnectivityTypeChanged += async (sender, args) => {
                bool reachable = await IsAvtoNetReachable();
                if (reachable && !Parser.CarConfigurator.IsInitialized)
                    await ParseAvtoNetSource();
            };
        }

        private async Task<bool> IsAvtoNetReachable()
        {
            Parser.CarConfigurator.InitializationStatus = "Preverjanje povezljivosti...";
            if (!Connectivity.IsConnected)
                return false;

            var reachable = await Connectivity.IsRemoteReachable(DomainConstants.Host);
            return reachable;
        }

        private async Task ParseAvtoNetSource()
        {
            Parser.CarConfigurator.InitializationStatus = "Nalaganje podatkov...";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            bool status = await Parser.LoadSourceAsync(Encoding.GetEncoding(1250));

            if (status)
                Parser.Parse();
        }
    }
}
