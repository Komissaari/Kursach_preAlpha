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
    /// Логика взаимодействия для PageEditAddressStorekeepers.xaml
    /// </summary>
    public partial class PageEditAddressStorekeepers : Page
    {
        bool _edit;
        private AddressStorekeepers _currentAddressStorekeepers = new AddressStorekeepers();
        //в случае редактирования не позволяем трогать поле ID
        public PageEditAddressStorekeepers(AddressStorekeepers selectedAddressStorekeepers, bool edit)
        {
            InitializeComponent();
            _edit = edit;
            if (selectedAddressStorekeepers != null)
            {
                TbAddressStorekeepersID.IsEnabled = false;
                _currentAddressStorekeepers = selectedAddressStorekeepers;
            }
            DataContext = _currentAddressStorekeepers;
        }
        //кнопка сохранения с проверками на заполненность полей
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAddressStorekeepers.AdStCity))
                errors.AppendLine("Пустое поле города!");
            //если есть ошибки то отменяем сохранение
            if (string.IsNullOrWhiteSpace(_currentAddressStorekeepers.AdStStreet))
                errors.AppendLine("Пустое поле улицы!");
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
                    Manager.GetContext().AddressStorekeepers.Add(_currentAddressStorekeepers);
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
