using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Логика взаимодействия для PageEditLibrarian.xaml
    /// </summary>
    public partial class PageEditLibrarian : Page
    {
        bool _edit;
        private Librarian _currentLibrarian = new Librarian();
        public PageEditLibrarian(Librarian selectedLibrarian, bool edit)
        {
            InitializeComponent();
            _edit = edit;
            if (selectedLibrarian != null)
            {
                LibPassword.IsEnabled = false;
                _currentLibrarian = selectedLibrarian;
            }
            DataContext = _currentLibrarian;
        }
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentLibrarian.LibSurname))
                errors.AppendLine("Пустое поле фамилии библиотекаря!");
            if (string.IsNullOrWhiteSpace(_currentLibrarian.LibName))
                errors.AppendLine("Пустое поле имени библиотекаря!");
            if (string.IsNullOrWhiteSpace(_currentLibrarian.LibPassword))
                errors.AppendLine("Пустое поле пароля библиотекаря!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (!_edit)
                {
                    Manager.GetContext().Librarian.Add(_currentLibrarian);
                }
                Manager.GetContext().SaveChanges();
                MessageBox.Show("сохранено ...");
                Manager.MainFrame.GoBack();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }
    }
}
