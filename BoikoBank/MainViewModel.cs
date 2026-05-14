using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace BoikoBank
{
    public class MainViewModel : INotifyPropertyChanged
    {
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

        public ICommand NavigationCommand { get; }

        public MainViewModel()
        { 
            CurrentPage = new Page1();
            NavigationCommand = new RelayCommand(p => Navigate(p?.ToString()));
        }

        private void Navigate(string destination)
        {
            if (string.IsNullOrEmpty(destination)) return;

            switch (destination)
            {
                case "ClientAndTransactions":
                    CurrentPage = new Page1();
                    break;
                case "Clients":
                    CurrentPage = new Page2(); 
                    break;
                case "Transactions":
                    CurrentPage = new Page3(); 
                    break;
                case "AddTransaction":
                    CurrentPage = new Page4(); 
                    break;
                case "Support":
                    CurrentPage = new Page5(); 
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}