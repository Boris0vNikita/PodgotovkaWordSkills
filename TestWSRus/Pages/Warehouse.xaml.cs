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
    /// Логика взаимодействия для Warehouse.xaml
    /// </summary>
    public partial class Warehouse : Page
    {
        public Warehouse()
        {
            InitializeComponent();
            dgrdWarehouse.ItemsSource = DbConn.db.Product.ToList();

            List<string> availability = new List<string>();
            availability.Add("Все");
            availability.Add("Есть в наличии");
            availability.Add("Нет в наличии");

            cbmAvailability.ItemsSource = availability;
            cbmProvider.ItemsSource = DbConn.db.Provider.ToList();
            cbmTypeProduct.ItemsSource = DbConn.db.TypeProduct.ToList();
        }


        private void btnChangeAvailability_Click(object sender, RoutedEventArgs e)
        {
            if(dgrdWarehouse.SelectedItem as Product == null)
            {
                MessageBox.Show("Выбери в таблице элемент");
            }
            else
            {
                int idElement = (dgrdWarehouse.SelectedItem as Product).IdProduct;
                Product product = DbConn.db.Product.Where(p => p.IdProduct == idElement).FirstOrDefault();
                if (product.AvailabilityId == 2)
                {
                    product.AvailabilityId = 1;
                }
                else
                {
                    product.AvailabilityId = 2;
                }


                try
                {
                    DbConn.db.SaveChanges();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                dgrdWarehouse.ItemsSource = DbConn.db.Product.ToList();
            }
        }

        private void btnAddTypeProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Uri("/Pages/AddProduct.xaml", UriKind.RelativeOrAbsolute));
        }


        private void dgrdWarehouse_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Product product = (Product)(e.Row.DataContext);

            if(product.AvailabilityId == 2)
            {
                e.Row.Background = new SolidColorBrush(Colors.Pink);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cbmProvider.ItemsSource = "";
            cbmProvider.ItemsSource = DbConn.db.Provider.ToList();
            cbmTypeProduct.ItemsSource = "";
            cbmTypeProduct.ItemsSource = DbConn.db.TypeProduct.ToList();
            dgrdWarehouse.ItemsSource = DbConn.db.Product.ToList();
        }

        private void cbmAvailability_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Если выбран 1
            if (cbmAvailability.SelectedItem != null && cbmProvider.SelectedItem == null && cbmTypeProduct.SelectedItem == null)
            {
                if (cbmAvailability.SelectedItem.ToString() != "Все")
                {
                    string nameAvail = cbmAvailability.SelectedItem.ToString();
                    dgrdWarehouse.ItemsSource = DbConn.db.Product.Where(p => p.Availability.NameAvailability == nameAvail).ToList();
                }
                else
                {
                    dgrdWarehouse.ItemsSource = DbConn.db.Product.ToList();
                }
            }
            //Выбран 2
            if (cbmAvailability.SelectedItem == null && cbmProvider.SelectedItem != null && cbmTypeProduct.SelectedItem == null)
            {
                string nameProvider = (sender as ComboBox).SelectedItem.ToString();
                MessageBox.Show(nameProvider);
                dgrdWarehouse.ItemsSource = DbConn.db.Product.Where(p => p.Provider.NameProvider == nameProvider).ToList();
            }
            //Выбран 3
            if (cbmAvailability.SelectedItem == null && cbmProvider.SelectedItem == null && cbmTypeProduct.SelectedItem != null)
            {
                string nameTypeProduct = cbmTypeProduct.SelectedItem.ToString();
                dgrdWarehouse.ItemsSource = DbConn.db.Product.Where(p => p.TypeProduct.NameTypeProduct == nameTypeProduct).ToList();
            }
            //1 and 2
            if (cbmAvailability.SelectedItem != null && cbmProvider.SelectedItem != null && cbmTypeProduct.SelectedItem == null)
            {
                string nameAvail = cbmAvailability.SelectedItem.ToString();
                string nameProvider = cbmProvider.SelectedItem.ToString();

                if (cbmAvailability.SelectedItem.ToString() != "Все")
                {
                    dgrdWarehouse.ItemsSource = DbConn.db.Product.Where(p => p.Provider.NameProvider == nameProvider && p.Availability.NameAvailability == nameAvail).ToList();
                }
                else
                {
                    dgrdWarehouse.ItemsSource = DbConn.db.Product.Where(p => p.Provider.NameProvider == nameProvider).ToList();
                }
            }
            // 1 and 3
            if (cbmAvailability.SelectedItem != null && cbmProvider.SelectedItem == null && cbmTypeProduct.SelectedItem != null)
            {
                string nameAvail = cbmAvailability.SelectedItem.ToString();
                string nameTypeProduct = cbmTypeProduct.SelectedItem.ToString();
                if (cbmAvailability.SelectedItem.ToString() != "Все")
                {
                    dgrdWarehouse.ItemsSource = DbConn.db.Product.Where(p => p.TypeProduct.NameTypeProduct == nameTypeProduct && p.Availability.NameAvailability == nameAvail).ToList();
                }
                else
                {
                    dgrdWarehouse.ItemsSource = DbConn.db.Product.Where(p => p.TypeProduct.NameTypeProduct == nameTypeProduct).ToList();
                }
            }
            // 2 and 3
            if (cbmAvailability.SelectedItem == null && cbmProvider.SelectedItem != null && cbmTypeProduct.SelectedItem != null)
            {
                string nameProvider = cbmProvider.SelectedItem.ToString();
                string nameTypeProduct = cbmTypeProduct.SelectedItem.ToString();

                dgrdWarehouse.ItemsSource = DbConn.db.Product.Where(p => p.TypeProduct.NameTypeProduct == nameTypeProduct && p.Provider.NameProvider == nameProvider).ToList();
            }
            //выбраны все
            if (cbmAvailability.SelectedItem != null && cbmProvider.SelectedItem != null && cbmTypeProduct.SelectedItem != null)
            {
                string nameAvail = cbmAvailability.SelectedItem.ToString();
                string nameProvider = cbmProvider.SelectedItem.ToString();
                string nameTypeProduct = cbmTypeProduct.SelectedItem.ToString();
                if (cbmAvailability.SelectedItem.ToString() != "Все")
                {
                    dgrdWarehouse.ItemsSource = DbConn.db.Product.Where(p => p.TypeProduct.NameTypeProduct == nameTypeProduct && p.Provider.NameProvider == nameProvider && p.Availability.NameAvailability == nameAvail).ToList();
                }
                else
                {
                    dgrdWarehouse.ItemsSource = DbConn.db.Product.Where(p => p.TypeProduct.NameTypeProduct == nameTypeProduct && p.Provider.NameProvider == nameProvider).ToList();
                }

            }
        }
    }
}
