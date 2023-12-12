using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace LibraryInformationSystem
{
    /// <summary>
    /// Логика взаимодействия для PageEditGenre.xaml
    /// </summary>
    public partial class PageEditGenre : Page
    {
        bool _edit;
        private Genre _currentGenre = new Genre();
        //в случае редактирования не позволяем трогать поле ID
        public PageEditGenre(Genre selectedGenre, bool edit)
        {
            InitializeComponent();
            _edit = edit;
            if (selectedGenre != null)
            {
                TbGenreID.IsEnabled = false;
                _currentGenre = selectedGenre;
            }
            DataContext = _currentGenre;
        }
        //кнопка сохранения с проверками на заполненность полей
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentGenre.GenerName))
                errors.AppendLine("Отсутствует имя жанра!");
            //если есть ошибки то отменяем сохранение
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //задаём логику сохранения при добавлении и сохраненни изменения при редактировании с помощью 
            //булевской переменной
            try
            {
                if (!_edit)
                {
                    Manager.GetContext().Genre.Add(_currentGenre);
                }
                Manager.GetContext().SaveChanges();
                MessageBox.Show("Сохранено!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }
    }
}
