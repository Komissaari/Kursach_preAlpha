using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LibraryInformationSystem
{
    /// <summary>
    /// Логика взаимодействия для LibMenu.xaml
    /// </summary>
    public partial class LibMenu : Window
    {
        public LibMenu()
        {
            InitializeComponent();
            Manager.MainFrame = MainFrame;
            
        }
        /*void UpdateDispaly()
        {
            LibraryInformationSystemEntities model = new LibraryInformationSystemEntities();
            List<Publication> allPublication = model.Publication.ToList();
            foreach (var book in allPublication)
            {
                if (Poisk.Text != "")
                {
                    if (book.PublicationName.ToLower().IndexOf(Poisk.Text.ToLower()) == -1)
                    {
                        if (book.Author.AuSurname.ToLower().IndexOf(Poisk.Text.ToLower()) == -1)
                        {
                            if (book.Author.AuName.ToLower().IndexOf(Poisk.Text.ToLower()) == -1)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }*/
        private void Button_Click_Publication(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PagePublication());
        }

        private void Button_Click_Authors(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageAuthor());
        }

        private void Button_Click_Publisher(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PagePublisher());
        }

        private void Button_Click_Genre(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageGenre());
        }

        private void Button_Click_TypePublication(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageTypePublication());
        }

        private void Button_Click_City(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageCity());
        }

        private void Button_Click_AdressStorekeepers(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageAddressStorekeepers());
        }
    }
}
