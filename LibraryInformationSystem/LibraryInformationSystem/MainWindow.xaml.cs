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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RegisterFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            Manager.mainWindow = this;
            Manager.RegisterFrame = RegisterFrame;
        }
        //кнопка авторизации
        private void Button_Click_Autorization(object sender, RoutedEventArgs e)
        {
            if (TB_Login.Text == null || TB_Pass.Password == null)
            {
                MessageBox.Show("Заполните поля!");
            }
            else
            {
                //процесс расшифровки хэшированного пароля
                var crypt = System.Security.Cryptography.SHA256.Create();
                var notfinal = crypt.ComputeHash(Encoding.UTF8.GetBytes(TB_Pass.Password));
                var final = Convert.ToBase64String(notfinal);
                Librarian user = Manager.GetContext().Librarian.FirstOrDefault(p => p.LibLogin == TB_Login.Text && (p.LibPassword.ToString() ==
                final));
                //если есть пользователь имеет флаг администратора, то открывается специальное меню
                if (user != null)
                {
                    if (user.LibAdmin == true)
                    {
                        AdminMenu windadmin = new AdminMenu();
                        windadmin.Show();
                        Close();
                    }
                    else
                    {
                        Manager.Login = user.LibLogin;
                        LibMenu wind = new LibMenu();
                        wind.Show();
                        Close();
                    }
                }
            }
        }
        //кнопка регистрации
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Visibility = Visibility.Collapsed;
            RegisterFrame.Navigate(new PageRegestration());
        }
        //метод отображения формы регистрации
        public void VisibleMain()
        {
            RegisterFrame.Content = null;
            GridMain.Visibility = Visibility.Visible;
        }
    }
}
