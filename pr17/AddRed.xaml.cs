using pr17.database;
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
using System.Windows.Shapes;

namespace pr17
{
    /// <summary>
    /// Логика взаимодействия для AddRed.xaml
    /// </summary>
    public partial class AddRed : Window
    {
        WorkerZpContext _db = new WorkerZpContext();
        MonthZp _monthZp;
        public AddRed()
        {
            InitializeComponent();
        }

        private void AddEditClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbsur.Text.Length == 0) errors.AppendLine("Введите фамилию");
            if (tbname.Text.Length == 0) errors.AppendLine("Введите имя");
            if (tbdadsname.Text.Length == 0) errors.AppendLine("Введите отчество");
            if (calfd.Text == "") errors.AppendLine("Введите дату поступления");
            if (Int32.TryParse(tbrazryad.Text, out int b) == false || b<=0) errors.AppendLine("Введите корректный разряд");
            if (Int32.TryParse(tbyears.Text, out int a) == false || a <= 0) errors.AppendLine("Введите корректный стаж");
            if (Int32.TryParse(tbzp.Text, out a) == false || a <= 0) errors.AppendLine("Введите корректную зарплату");
            if (cbprof.Text != "Мастер" && cbprof.Text != "Рабочий" && cbprof.Text != "Помощник") errors.AppendLine("Выберите должность");
            if (cbzeh.Text != "Оловянный" && cbzeh.Text != "Деревянный" && cbzeh.Text != "Пробковый" && cbzeh.Text != "Стеклянный" && cbzeh.Text != "Металлический") errors.AppendLine("Выберите цех");

            if (errors.Length > 0) 
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (Data.monthZp == null)
                {
                    _db.MonthZps.Add(_monthZp);
                    _db.SaveChanges();
                }
                else
                {
                    _db.SaveChanges();
                }
                MessageBox.Show("Информация сохранена");
                this.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (Data.monthZp == null)
            {
                WindowAddEdit.Title = "Добавление записи";
                btnadded.Content = "Добавить";
                _monthZp = new MonthZp();
            }
            else
            {
                WindowAddEdit.Title = "Редактирование записи";
                btnadded.Content = "Редактировать";
                _monthZp = _db.MonthZps.Find(Data.monthZp.Surname, Data.monthZp.Name);
            }
            WindowAddEdit.DataContext = _monthZp;
        }
    }
}
