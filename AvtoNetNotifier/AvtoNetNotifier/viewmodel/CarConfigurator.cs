using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                OnPropertyChanged("MinPrice");
                OnPropertyChanged("MaxPrice");
                OnPropertyChanged("MinAge");
                OnPropertyChanged("MaxAge");
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

        public MyObservableCollection<CarBrand> Brands { get; set; }

        public CarBrand SelectedBrand
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarBrand>(nameof(SelectedBrand), Brands);
            }
            set
            {
                if (IsInitialized)
                {
                    AppSettings.AddOrUpdateValue(nameof(SelectedBrand),
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
                return AppSetttingsSerializedDefaultGet<CarAttribute<string>>(nameof(SelectedModel), Models);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<string>>(nameof(SelectedModel), value);
            }
        }

        public MyObservableCollection<CarAttribute<uint>> MinPrices { get; set; }
        public MyObservableCollection<CarAttribute<uint>> MaxPrices { get; set; }
        public CarAttribute<uint> MinPrice
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<uint>>(nameof(MinPrice), MinPrices);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<uint>>(nameof(MinPrice), value);
            }
        }
        public CarAttribute<uint> MaxPrice
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<uint>>(nameof(MaxPrice), MaxPrices);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<uint>>(nameof(MaxPrice), value);
            }
        }

        public MyObservableCollection<CarAttribute<uint>> MinAges { get; set; }
        public MyObservableCollection<CarAttribute<uint>> MaxAges { get; set; }
        public CarAttribute<uint> MinAge
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<uint>>(nameof(MinAge), MinAges);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<uint>>(nameof(MinAge), value);
            }
        }
        public CarAttribute<uint> MaxAge
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAttribute<uint>>(nameof(MaxAge), MaxAges);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAttribute<uint>>(nameof(MaxAge), value);
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
