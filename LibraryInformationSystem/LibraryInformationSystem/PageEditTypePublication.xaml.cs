﻿using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace LibraryInformationSystem
{
    /// <summary>
    /// Логика взаимодействия для PageEditTypePublication.xaml
    /// </summary>
    public partial class PageEditTypePublication : Page
    {
        bool _edit;
        private TypePublication _currentTypePublication = new TypePublication();
        //в случае редактирования не позволяем трогать поле ID
        public PageEditTypePublication(TypePublication selectedTypePublication, bool edit)
        {
            InitializeComponent();
            _edit = edit;
            if (selectedTypePublication != null)
            {
                TbTypePublicationID.IsEnabled = false;
                _currentTypePublication = selectedTypePublication;
            }
            DataContext = _currentTypePublication;
        }
        //кнопка сохранения с проверками на заполненность полей
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentTypePublication.TPubName))
                errors.AppendLine("Отсутствует тип публикации!");
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
                    Manager.GetContext().TypePublication.Add(_currentTypePublication);
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
