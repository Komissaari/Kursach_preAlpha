using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryInformationSystem
{
    /// <summary>
    /// Логика взаимодействия для PageEditPublication.xaml
    /// </summary>
    public partial class PageEditPublication : Page
    {
        bool _edit;
        private Publication _currentPublication = new Publication();
        //в случае редактирования не позволяем трогать поле ID
        public PageEditPublication(Publication selectedPublication, bool edit)
        {
            InitializeComponent();
            _edit = edit;
            if (selectedPublication != null)
            {
                TbPublicationID.IsEnabled = false;
                _currentPublication = selectedPublication;
            }
            DataContext = _currentPublication;
            ComboAuthor.ItemsSource = Manager.GetContext().Author.ToList();
            ComboTypePublication.ItemsSource = Manager.GetContext().TypePublication.ToList();
            ComboPublisher.ItemsSource = Manager.GetContext().Publisher.ToList();
            ComboGenre.ItemsSource = Manager.GetContext().Genre.ToList();

        }
        //кнопка сохранения с проверками на заполненность полей
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentPublication.PublicationName))
                errors.AppendLine("Отсутствует название публикации!");
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
                    Manager.GetContext().Publication.Add(_currentPublication);
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
