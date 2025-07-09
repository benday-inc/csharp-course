using System.ComponentModel;

namespace ViewModelTestingLab.Core
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private int _age = 0;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                    OnPropertyChanged(nameof(IsAdult));
                }
            }
        }

        public bool IsAdult => Age >= 18;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
