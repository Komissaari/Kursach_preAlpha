using System.Windows;

namespace LibraryInformationSystem
{
    /// <summary>
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        public AdminMenu()
        {
            InitializeComponent();
            Manager.AdminFrame = AdminFrame;
        }

        private void Button_Click_Librarian(object sender, RoutedEventArgs e)
        {
            Manager.AdminFrame.Navigate(new PageLibrarian());
        }

    }
}
