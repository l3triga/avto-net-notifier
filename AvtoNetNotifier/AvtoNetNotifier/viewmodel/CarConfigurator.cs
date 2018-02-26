using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using AvtoNetLibrary.Serializer;
using AvtoNetLibrary.Model;

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
                OnPropertyChanged("MinPrice");
                OnPropertyChanged("MaxPrice");
                OnPropertyChanged("MinAge");
                OnPropertyChanged("MaxAge");
                OnPropertyChanged("MinKilometer");
                OnPropertyChanged("MaxKilometer");
                OnPropertyChanged("SellerType");
                OnPropertyChanged("SellerLocation");
            }
        }

        private string _initializationStatus;
        public string InitializationStatus {
            get {
                return _initializationStatus;
            }
            set {
                _initializationStatus = value;
                OnPropertyChanged("InitializationStatus");
            }
        }

        public bool New
        {
            get {
                return AppSettings.GetValueOrDefault("star1", true);
            }
            set {
                AppSettings.AddOrUpdateValue("star1", value);
            }
        }

        public bool Test
        {
            get {
                return AppSettings.GetValueOrDefault("star2", true);
            }
            set {
                AppSettings.AddOrUpdateValue("star2", value);
            }
        }

        public bool Used
        {
            get {
                return AppSettings.GetValueOrDefault("star4", true);
            }
            set {
                AppSettings.AddOrUpdateValue("star4", value);
            }
        }

        public MyObservableCollection<CarBrand> Brands { get; set; }
        public CarBrand SelectedBrand
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarBrand>("znamka", Brands);
            }
            set
            {
                if (IsInitialized)
                {
                    AppSettings.AddOrUpdateValue("znamka",
                        ObjectSerializer.Serialize<CarBrand>(value)
                    );
                    OnPropertyChanged("SelectedBrand");
                    if (value.Models != null)
                    {
                        Models = new MyObservableCollection<CarAttribute<string>>(value.Models);
                        OnPropertyChanged("Models");
                        OnPropertyChanged("SelectedModel");
                    }
                }
            }
        }

        public MyObservableCollection<CarAttribute<string>> Models { get; set; }
        public CarAttribute<string> SelectedModel {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<string>>("model", Models);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<string>>("model", value);
            }
        }

        public MyObservableCollection<CarAttribute<uint>> MinPrices { get; set; }
        public CarAttribute<uint> MinPrice
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<uint>>("cenaMin", MinPrices);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<uint>>("cenaMin", value);
            }
        }

        public MyObservableCollection<CarAttribute<uint>> MaxPrices { get; set; }
        public CarAttribute<uint> MaxPrice
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<uint>>("cenaMax", MaxPrices);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<uint>>("cenaMax", value);
            }
        }

        public MyObservableCollection<CarAttribute<uint>> MinAges { get; set; }
        public CarAttribute<uint> MinAge
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<uint>>("letnikMin", MinAges);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<uint>>("letnikMin", value);
            }
        }

        public MyObservableCollection<CarAttribute<uint>> MaxAges { get; set; }
        public CarAttribute<uint> MaxAge
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<uint>>("letnikMax", MaxAges);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<uint>>("letnikMax", value);
            }
        }

        public MyObservableCollection<CarAttribute<uint>> MinKilometers { get; set; }
        public CarAttribute<uint> MinKilometer
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<uint>>("kmMIN", MinKilometers);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<uint>>("kmMIN", value);
            }
        }

        public MyObservableCollection<CarAttribute<uint>> MaxKilometers { get; set; }
        public CarAttribute<uint> MaxKilometer
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<uint>>("kmMax", MaxKilometers);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<uint>>("kmMax", value);
            }
        }

        public MyObservableCollection<CarAttribute<string>> SellerTypes { get; set; }
        public CarAttribute<string> SellerType
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<string>>("prodajalec", SellerTypes);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<string>>("prodajalec", value);
            }
        }

        public MyObservableCollection<CarAttribute<string>> SellerLocations { get; set; }
        public CarAttribute<string> SellerLocation
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<string>>("lokacija", SellerLocations);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<string>>("lokacija", value);
            }
        }

        public CarConfigurator()
        {
            IsInitialized = false;

            Brands = new MyObservableCollection<CarBrand>();
            Models = new MyObservableCollection<CarAttribute<string>>();

            MinPrices = new MyObservableCollection<CarAttribute<uint>>();
            MaxPrices = new MyObservableCollection<CarAttribute<uint>>();

            MinAges = new MyObservableCollection<CarAttribute<uint>>();
            MaxAges = new MyObservableCollection<CarAttribute<uint>>();

            MinKilometers = new MyObservableCollection<CarAttribute<uint>>();
            MaxKilometers = new MyObservableCollection<CarAttribute<uint>>();

            SellerTypes = new MyObservableCollection<CarAttribute<string>>();
            SellerLocations = new MyObservableCollection<CarAttribute<string>>();
        }

        private T AppSetttingsSerializedDefaultGet<T>(string property, MyObservableCollection<T> collection)
        {
            if (!IsInitialized)
                return default(T);
            var obj = ObjectSerializer.Deserialize<T>(
                AppSettings.GetValueOrDefault(property, ObjectSerializer.Serialize <T>(collection.FirstOrDefault()))
            );

            collection.TryGetValue(obj, out T refObj);
            return refObj;
        }

        private void AppSetingsSerializedDefaultSet<T>(string property, T value)
        {
            if (IsInitialized)
            {
                AppSettings.AddOrUpdateValue(property,
                    ObjectSerializer.Serialize<T>(value)
                );
                OnPropertyChanged(property);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
