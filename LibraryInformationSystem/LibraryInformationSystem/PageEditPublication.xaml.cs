using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
            ComboAuthor.SelectedItem = Manager.GetContext().Author.Find(_currentPublication.AuthorID);
            
            ComboTypePublication.ItemsSource = Manager.GetContext().TypePublication.ToList();
            ComboTypePublication.SelectedItem = Manager.GetContext().TypePublication.Find(_currentPublication.TypePublicationID);

            ComboPublisher.ItemsSource = Manager.GetContext().Publisher.ToList();
            ComboPublisher.SelectedItem = Manager.GetContext().Publisher.Find(_currentPublication.PublisherID);

            ComboGenre.ItemsSource = Manager.GetContext().Genre.ToList();
            ComboGenre.SelectedItem = Manager.GetContext().Genre.Find(_currentPublication.GenreID);
            
            ComboAddressStorekeepers.ItemsSource = Manager.GetContext().AddressStorekeepers.ToList();
            ComboAddressStorekeepers.SelectedItem = Manager.GetContext().AddressStorekeepers.Find(_currentPublication.AdressStorekeepID);
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
                    var CurrentAdSt = ComboAddressStorekeepers.SelectedItem as AddressStorekeepers;
                    _currentPublication.AdressStorekeepID = CurrentAdSt.AdressStoreroomID;

                    var CurrentGenre = ComboGenre.SelectedItem as Genre;
                    _currentPublication.GenreID = CurrentGenre.GenreID;

                    var CurrentAuthor = ComboAuthor.SelectedItem as Author;
                    _currentPublication.AuthorID = CurrentAuthor.AuthorID;

                    var CurrentTypePublication = ComboTypePublication.SelectedItem as TypePublication;
                    _currentPublication.TypePublicationID = CurrentTypePublication.TypePublicationID;

                    var CurrentPublisher = ComboPublisher.SelectedItem as Publisher;
                    _currentPublication.PublisherID = CurrentPublisher.PublisherID;

                    Manager.GetContext().Publication.Add(_currentPublication);
                }
                else
                {
                    var CurrentAdSt = ComboAddressStorekeepers.SelectedItem as AddressStorekeepers;
                    _currentPublication.AdressStorekeepID = CurrentAdSt.AdressStoreroomID;
                    
                    var CurrentGenre = ComboGenre.SelectedItem as Genre;
                    _currentPublication.GenreID = CurrentGenre.GenreID;

                    var CurrentAuthor = ComboAuthor.SelectedItem as Author;
                    _currentPublication.AuthorID = CurrentAuthor.AuthorID;

                    var CurrentTypePublication = ComboTypePublication.SelectedItem as TypePublication;
                    _currentPublication.TypePublicationID = CurrentTypePublication.TypePublicationID;

                    var CurrentPublisher = ComboPublisher.SelectedItem as Publisher;
                    _currentPublication.PublisherID = CurrentPublisher.PublisherID;
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
