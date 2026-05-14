using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace BoikoBank
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Client> _clients;
        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Client> CombinedList { get; set; }
        public ICommand NavigationCommand { get; }

        public MainViewModel()
        { 
            CurrentPage = new Page1();
            NavigationCommand = new RelayCommand(p => Navigate(p?.ToString()));
            CombinedList = new ObservableCollection<Client>();

            AddTestData();
        }

        private void AddTestData()
        {
            CombinedList.Add(new Client(1, "Бойко", "Владислав", "2354",2134));
            CombinedList.Add(new Client(2, "Тестов", "Тестер", "01.01.2026",23523));
        }

        private void Navigate(string destination)
        {
            if (string.IsNullOrEmpty(destination)) return;

            switch (destination)
            {
                case "ClientAndTransactions":
                    CurrentPage = new Page1 { DataContext = this };
                    break;
                case "Clients":
                    CurrentPage = new Page2 { DataContext = this }; 
                    break;
                case "Transactions":
                    CurrentPage = new Page3 { DataContext = this }; 
                    break;
                case "AddTransaction":
                    CurrentPage = new Page4 { DataContext = this }; 
                    break;
                case "Support":
                    CurrentPage = new Page5 { DataContext = this }; 
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

}