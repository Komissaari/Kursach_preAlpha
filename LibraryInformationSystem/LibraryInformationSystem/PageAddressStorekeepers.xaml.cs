using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryInformationSystem
{
    /// <summary>
    /// Логика взаимодействия для PageAddressStorekeepers.xaml
    /// </summary>
    public partial class PageAddressStorekeepers : Page
    {
        public PageAddressStorekeepers()
        {
            InitializeComponent();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Manager.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridAddressStorekeepers.ItemsSource = Manager.GetContext().AddressStorekeepers.ToList();
            }
        }
        //кнопка удаления имеет в себе проверку на ошибочность, что позволяет уберечь от случайного удаления
        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            var delAddressStorekeepers = DGridAddressStorekeepers.SelectedItems.Cast<AddressStorekeepers>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {delAddressStorekeepers.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Manager.GetContext().AddressStorekeepers.RemoveRange(delAddressStorekeepers);
                    Manager.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены...");
                    DGridAddressStorekeepers.ItemsSource = Manager.GetContext().AddressStorekeepers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        //кнопка добавления элимента
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageEditAddressStorekeepers(null, false));
        }
        //в кнопку редактирования передаём нужное поле для редактирования
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageEditAddressStorekeepers((sender as Button).DataContext as AddressStorekeepers, true));
        }
    }
}
