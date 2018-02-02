using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;

namespace AvtoNetNotifier
{
    class CarConfigurator : INotifyPropertyChanged
    {
        public ObservableCollection<CarBrand> Brands { get; set; }
    
        private CarBrand _selectedBrand;
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
        }

        public ObservableCollection<CarModel> Models { get; set; }

        public CarModel SelectedModel { get; set; }

        public CarConfigurator()
        {
            Brands = new ObservableCollection<CarBrand>();
            Models = new ObservableCollection<CarModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
