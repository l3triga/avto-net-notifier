﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AvtoNetNotifier
{
    class CarConfigurator : INotifyPropertyChanged
    {
        private static ISettings AppSettings = CrossSettings.Current;

        private bool _isInitialized;
        public bool IsInitialized {
            get {
                return _isInitialized;
            }
            set {
                _isInitialized = value;
                OnPropertyChanged("IsInitialized");
                OnPropertyChanged("SelectedBrand");
            }
        }

        public bool New
        {
            get {
                return AppSettings.GetValueOrDefault(nameof(New), true);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(New), value);
            }
        }

        public bool Test
        {
            get {
                return AppSettings.GetValueOrDefault(nameof(Test), true);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(Test), value);
            }
        }

        public bool Used
        {
            get {
                return AppSettings.GetValueOrDefault(nameof(Used), true);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(Used), value);
            }
        }

        public ObservableCollection<CarBrand> Brands { get; set; }
        /*private CarBrand _selectedBrand;
        public CarBrand SelectedBrand {
            get {
                return _selectedBrand;
            }
            set {
                _selectedBrand = value;
                OnPropertyChanged("SelectedBrand");
                this.Models = new ObservableCollection<CarModel>(_selectedBrand.Models);
                OnPropertyChanged("Models");
            }
        }*/

        public CarBrand SelectedBrand
        {
            get
            {
                if (!IsInitialized)
                    return null;

                var brand = ObjectSerializer.Deserialize<CarBrand>(
                    AppSettings.GetValueOrDefault(nameof(SelectedBrand), null)
                );
                return Brands[Brands.IndexOf(brand)];
            }
            set
            {
                if (IsInitialized)
                {
                    AppSettings.AddOrUpdateValue(nameof(SelectedBrand),
                        ObjectSerializer.Serialize<CarBrand>(value)
                    );
                    OnPropertyChanged("SelectedBrand");
                    this.Models = new ObservableCollection<CarModel>(value.Models ?? null);
                    OnPropertyChanged("Models");
                }
            }
        }

        public ObservableCollection<CarModel> Models { get; set; }
        public CarModel SelectedModel { get; set; }

        public ObservableCollection<CarPrice> Prices { get; set; }
        public CarPrice MinPrice { get; set; }
        public CarPrice MaxPrice { get; set; }

        public CarConfigurator()
        {
            IsInitialized = false;

            Brands = new ObservableCollection<CarBrand>();
            Models = new ObservableCollection<CarModel>();
            Prices = new ObservableCollection<CarPrice>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
