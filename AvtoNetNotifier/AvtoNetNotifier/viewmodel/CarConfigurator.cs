using System;
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
                        Models = new MyObservableCollection<CarModel>(value.Models);
                        OnPropertyChanged("Models");
                        OnPropertyChanged("SelectedModel");
                    }
                }
            }
        }

        public MyObservableCollection<CarModel> Models { get; set; }
        public CarModel SelectedModel {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarModel>(nameof(SelectedModel), Models);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarModel>(nameof(SelectedModel), value);
            }
        }

        public MyObservableCollection<CarPrice> Prices { get; set; }
        public CarPrice MinPrice
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarPrice>(nameof(MinPrice), Prices);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarPrice>(nameof(MinPrice), value);
            }
        }
        public CarPrice MaxPrice
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarPrice>(nameof(MaxPrice), Prices);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarPrice>(nameof(MaxPrice), value);
            }
        }

        public MyObservableCollection<CarAge> Ages { get; set; }
        public CarAge MinAge
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAge>(nameof(MinAge), Ages);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAge>(nameof(MinAge), value);
            }
        }
        public CarAge MaxAge
        {
            get
            {
                return AppSetttingsSerializedDefaultGet<CarAge>(nameof(MaxAge), Ages);
            }
            set
            {
                AppSetingsSerializedDefaultSet<CarAge>(nameof(MaxAge), value);
            }
        }

        public CarConfigurator()
        {
            IsInitialized = false;

            Brands = new MyObservableCollection<CarBrand>();
            Models = new MyObservableCollection<CarModel>();
            Prices = new MyObservableCollection<CarPrice>();
            Ages = new MyObservableCollection<CarAge>();
        }

        private T AppSetttingsSerializedDefaultGet<T>(string property, MyObservableCollection<T> collection)
        {
            if (!IsInitialized)
                return default(T);

            var obj = ObjectSerializer.Deserialize<T>(
                AppSettings.GetValueOrDefault(property, null)
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
