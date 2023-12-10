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
    /// Логика взаимодействия для PageRegestration.xaml
    /// </summary>
    public partial class PageRegestration : Page
    {
        private Librarian _currentLibrarian = new Librarian();
        public PageRegestration()
        {
            InitializeComponent();
            DataContext = _currentLibrarian;
        }
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentLibrarian.LibSurname))
                errors.AppendLine("Пустое поле фамилии читателя!");
            if (string.IsNullOrWhiteSpace(_currentLibrarian.LibName))
                errors.AppendLine("Пустое поле имени читателя!");
            if (string.IsNullOrWhiteSpace(TB_Pass.Password))
            {
                errors.AppendLine("Пустое поле пароля читателя!");

            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                var crypt = System.Security.Cryptography.SHA256.Create();
                var notfinal = crypt.ComputeHash(Encoding.UTF8.GetBytes(TB_Pass.Password));
                var final = Convert.ToBase64String(notfinal);
                _currentLibrarian.LibPassword = final;

                Manager.GetContext().Librarian.Add(_currentLibrarian);
                Manager.GetContext().SaveChanges();
                MessageBox.Show("сохранено ...");
                Manager.mainWindow.VisibleMain();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainWindow.VisibleMain();
        }
    }
}
