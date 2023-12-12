using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace LibraryInformationSystem
{
    /// <summary>
    /// Логика взаимодействия для PageLibrarian.xaml
    /// </summary>
    public partial class PageLibrarian : Page
    {
        public PageLibrarian()
        {
            InitializeComponent();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Manager.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridLibrarian.ItemsSource = Manager.GetContext().Librarian.ToList();
            }
        }
        //кнопка удаления имеет в себе проверку на ошибочность, что позволяет уберечь от случайного удаления
        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            var delLibrarian = DGridLibrarian.SelectedItems.Cast<Librarian>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {delLibrarian.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Manager.GetContext().Librarian.RemoveRange(delLibrarian);
                    Manager.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены...");
                    DGridLibrarian.ItemsSource = Manager.GetContext().Librarian.ToList();
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
            Manager.AdminFrame.Navigate(new PageEditLibrarian(null, false));
        }
        //в кнопку редактирования передаём нужное поле для редактирования
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Manager.AdminFrame.Navigate(new PageEditLibrarian((sender as Button).DataContext as Librarian, true));
        }
    }
}
