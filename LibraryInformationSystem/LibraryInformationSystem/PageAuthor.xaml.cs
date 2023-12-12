using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibraryInformationSystem
{
    /// <summary>
    /// Логика взаимодействия для PageAuthor.xaml
    /// </summary>
    public partial class PageAuthor : Page
    {
        public PageAuthor()
        {
            InitializeComponent();
        }


        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Manager.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridAuthor.ItemsSource = Manager.GetContext().Author.ToList();
            }
        }
        //кнопка удаления имеет в себе проверку на ошибочность, что позволяет уберечь от случайного удаления
        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            var delAuthor = DGridAuthor.SelectedItems.Cast<Author>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {delAuthor.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Manager.GetContext().Author.RemoveRange(delAuthor);
                    Manager.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены...");
                    DGridAuthor.ItemsSource = Manager.GetContext().Author.ToList();
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
            Manager.MainFrame.Navigate(new PageEditAuthor(null, false));
        }
        //в кнопку редактирования передаём нужное поле для редактирования
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageEditAuthor((sender as Button).DataContext as Author, true));
        }



    }
}

