using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryInformationSystem
{
    /// <summary>
    /// Логика взаимодействия для PageGenre.xaml
    /// </summary>
    public partial class PageGenre : Page
    {
        public PageGenre()
        {
            InitializeComponent();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Manager.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridGenre.ItemsSource = Manager.GetContext().Genre.ToList();
            }
        }
        //кнопка удаления имеет в себе проверку на ошибочность, что позволяет уберечь от случайного удаления
        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            var delGenre = DGridGenre.SelectedItems.Cast<Genre>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {delGenre.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Manager.GetContext().Genre.RemoveRange(delGenre);
                    Manager.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены...");
                    DGridGenre.ItemsSource = Manager.GetContext().Librarian.ToList();
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
            Manager.MainFrame.Navigate(new PageEditGenre(null, false));
        }
        //в кнопку редактирования передаём нужное поле для редактирования
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageEditGenre((sender as Button).DataContext as Genre, true));
        }
    }
}
