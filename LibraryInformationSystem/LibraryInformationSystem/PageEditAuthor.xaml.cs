using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace LibraryInformationSystem
{
    /// <summary>
    /// Логика взаимодействия для PageEditAuthor.xaml
    /// </summary>
    public partial class PageEditAuthor : Page
    {
        bool _edit;
        private Author _currentAuthor = new Author();
        //в случае редактирования не позволяем трогать поле ID
        public PageEditAuthor(Author selectedAuthor, bool edit)
        {
            InitializeComponent();
            _edit = edit;
            if (selectedAuthor != null)
            {
                TbAuthorID.IsEnabled = false;
                _currentAuthor = selectedAuthor;
            }
            DataContext = _currentAuthor;
        }
        //кнопка сохранения с проверками на заполненность полей
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAuthor.AuSurname))
                errors.AppendLine("Отсутствует фамилия или псевдоним!");
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
                    Manager.GetContext().Author.Add(_currentAuthor);
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
