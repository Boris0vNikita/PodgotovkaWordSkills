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
using TestWSRus.Infractucture;

namespace TestWSRus.Pages
{
    /// <summary>
    /// Логика взаимодействия для PagePerson.xaml
    /// </summary>
    public partial class PagePerson : Page
    {
        public PagePerson()
        {
            InitializeComponent();
            cmbRole.ItemsSource = DbConn.db.Role.ToList();
            dgrdListPeople.ItemsSource = DbConn.db.People.ToList();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var nameCmb = (cmbRole.SelectedItem as Role).NameRole;
            var role = DbConn.db.Role.Where(p => p.NameRole == nameCmb).FirstOrDefault();
            
            List<People> peoples = DbConn.db.People.Where(p => p.RoleId == role.IdRole).ToList();
            dgrdListPeople.ItemsSource = peoples;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (ckBox.IsChecked == true)
            {
                idData.Visibility = Visibility.Hidden;
            }
            
        }

        private void ckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ckBox.IsChecked == false)
            {
                idData.Visibility = Visibility.Visible;
            }
        }
    }
}
