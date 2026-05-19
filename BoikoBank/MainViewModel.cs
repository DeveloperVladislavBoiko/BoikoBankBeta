using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
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
        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        private Client _newClient = new Client();
        public Client NewClient
        {
            get => _newClient;
            set
            {
                _newClient = value;
                OnPropertyChanged(nameof(NewClient));
            }
        }

        public ObservableCollection<Client> ClientList { get; set; }
        public ICommand NavigationCommand { get; }
        public RelayCommand AddClientCommand { get; private set; }
        public RelayCommand EditClientCommand { get; private set; }
        public RelayCommand SaveClientCommand { get; private set; }
        public MainViewModel()
        { 
            CurrentPage = new Page1();
            NavigationCommand = new RelayCommand(p => SelectedPage(p?.ToString()));
            ClientList = new ObservableCollection<Client>();
            AddClientCommand = new RelayCommand(ExecuteAddClient);
            EditClientCommand = new RelayCommand(ExecuteEditClient, CanExecuteEditClient);
            //SaveClientCommand = new RelayCommand(ExecuteSaveClient, CanExecuteSaveClient);
            AddTestData();
        }

        private void AddTestData()
        {
            ClientList.Add(new Client(1, "Бойко", "Владислав", "12.12.2005",213400));
            ClientList.Add(new Client(2, "Тестов", "Тестер", "01.01.2026",23523));
        }

        private void SelectedPage(string destination)
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

        private bool CanExecuteEditClient(object parameter)
        {
            return SelectedClient != null;
        }

        private void ExecuteAddClient(object parameter)
        {
            NewClient = new Client();
            var clientWindow = new ClientAddWindow();
            clientWindow.DataContext = this;
            clientWindow.ShowDialog();
        }

        private void ExecuteEditClient(object parameter)
        {
            NewClient = new Client();
            var clientWindow = new ClientEditWindow();
            clientWindow.DataContext = this;
            clientWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

}