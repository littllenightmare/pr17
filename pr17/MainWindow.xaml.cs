using Microsoft.IdentityModel.Tokens;
using pr17.database;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr17
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            Data.monthZp = null;
            AddRed ar = new AddRed();
            ar.Owner = this;
            ar.ShowDialog();
            LoadDBInDataGrid();
        }

        private void ReClick(object sender, RoutedEventArgs e)
        {
            if(dg.SelectedItems != null)
            {
                Data.monthZp = (MonthZp)dg.SelectedItem;
                AddRed ar = new AddRed();
                ar.Owner = this;
                ar.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void DelClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                try
                {
                    MonthZp row = (MonthZp)dg.SelectedItem;
                    if (row != null)
                    {
                        using (WorkerZpContext _db = new WorkerZpContext())
                        {
                            _db.MonthZps.Remove(row);
                            _db.SaveChanges();
                        }
                        
                        LoadDBInDataGrid();
                    }
                }

                catch
                {
                    MessageBox.Show("Ошибка удаления");
                }
            }
            else dg.Focus();
        }

        private void InfoClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Создать однотабличную базу данных, в которой отобразятся сведения о месячной зарплате рабочих.\r\n Разработать к ней интерфейс для работы пользователя.\r\n Выполнила студентка группы ИСП-31 Кулькова Ангелина");
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }
        void LoadDBInDataGrid()
        {
            using (WorkerZpContext _db = new WorkerZpContext())
            {
                int SelectedIndex = dg.SelectedIndex;
                dg.ItemsSource = _db.MonthZps.ToList();
                if(SelectedIndex !=-1)
                {
                    if (SelectedIndex == dg.Items.Count) SelectedIndex--;
                    dg.SelectedIndex = SelectedIndex;
                    dg.ScrollIntoView(dg.SelectedItem);
                }
                dg.Focus();
            }
        }

        private void FilterClick(object sender, RoutedEventArgs e)
        {
            if (filtertb.Text.IsNullOrEmpty() == false)
            {
                using (WorkerZpContext _db = new WorkerZpContext())
                {
                    var filtered = _db.MonthZps.Where(p => p.Surname.Contains(filtertb.Text) ||
                    p.Name.Contains(filtertb.Text) ||
                    p.DadsName.Contains(filtertb.Text) ||
                    p.Zeh.Contains(filtertb.Text) ||
                    p.Razryad.ToString().Contains(filtertb.Text) ||
                    p.Profession.Contains(filtertb.Text) ||
                    p.Zp.ToString().Contains(filtertb.Text) ||
                    p.Years.ToString().Contains(filtertb.Text));
                    dg.ItemsSource = filtered.ToList();
                }
            }
            else
            {
                LoadDBInDataGrid();
            }
        }

        private void FindClick(object sender, RoutedEventArgs e)
        {
            List<MonthZp> listItem = (List<MonthZp>)dg.ItemsSource;
            var finded = listItem.Where(p => p.Surname.Contains(findtb.Text) ||
                p.Name.Contains(findtb.Text) ||
                p.DadsName.Contains(findtb.Text) ||
            p.Zeh.Contains(findtb.Text) ||
            p.Razryad.ToString().Contains(findtb.Text) ||
            p.Profession.Contains(findtb.Text) ||
             p.Zp.ToString().Contains(findtb.Text) ||
             p.Years.ToString().Contains(findtb.Text));
            if (finded.Count() > 0)
            {
                MonthZp item = finded.First();
                dg.SelectedItem = item;
                dg.ScrollIntoView(item);
                dg.Focus();
            }
        }
    }
}