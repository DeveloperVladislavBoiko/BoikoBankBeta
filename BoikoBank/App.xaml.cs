using System.Windows;

namespace BoikoBank
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                await DbHelper.CreateDbAsync();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Возникла ошибка подключения к БД", ex.Message.ToString());
                MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}", "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                //MessageBox.Show("Не удалось подключиться к базе данных!", "Ошибка БД", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}