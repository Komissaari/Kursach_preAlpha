using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryInformationSystem
{
    /// <summary>
    /// Логика взаимодействия для PagePublication.xaml
    /// </summary>
    public partial class PagePublication : Page
    {
        public PagePublication()
        {
            InitializeComponent();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Manager.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridPublication.ItemsSource = Manager.GetContext().Publication.ToList();
            }
        }
        //кнопка удаления имеет в себе проверку на ошибочность, что позволяет уберечь от случайного удаления
        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            var delPublication = DGridPublication.SelectedItems.Cast<Publication>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {delPublication.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Manager.GetContext().Publication.RemoveRange(delPublication);
                    Manager.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены...");
                    DGridPublication.ItemsSource = Manager.GetContext().Publication.ToList();
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
            Manager.MainFrame.Navigate(new PageEditPublication(null, false));
        }
        //в кнопку редактирования передаём нужное поле для редактирования
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageEditPublication((sender as Button).DataContext as Publication, true));
        }
    }
}
