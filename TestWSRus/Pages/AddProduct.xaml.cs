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
using TestWSRus.Windows;

namespace TestWSRus.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddTypeProduct.xaml
    /// </summary>
    public partial class AddTypeProduct : Page
    {
        public AddTypeProduct()
        {
            InitializeComponent();
            cmbProvider.ItemsSource = DbConn.db.Provider.ToList();
            cmbTypeProduct.ItemsSource = DbConn.db.TypeProduct.ToList();
            cmbWarehouse.ItemsSource = DbConn.db.Warehouse.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txbNameProduct.Text != null && txbPrice.Text != null && cmbWarehouse.SelectedItem != null && cmbTypeProduct.SelectedItem != null && cmbProvider.SelectedItem != null)
            {
                Product product = new Product();
                product.IdProduct = DbConn.db.Product.Max(p => p.IdProduct) + 1;
                product.NameProduct = txbNameProduct.Text.Trim();

                int price = Int32.Parse(txbPrice.Text.Trim());
                product.Price = price;

                int idProvider = (cmbProvider.SelectedItem as Provider).IdProvider;
                product.ProviderId = idProvider;

                int idTypeProduct = (cmbTypeProduct.SelectedItem as TypeProduct).IdTypeProduct;
                product.TypeProductId = idTypeProduct;


                product.WarehouseId = DbConn.db.Warehouse.Where(p => p.NameWarehouse == cmbWarehouse.Text).FirstOrDefault().IdWarehouse;
                product.AvailabilityId = 1;

                try
                {
                    DbConn.db.Product.Add(product);
                    DbConn.db.SaveChanges();
                    MessageBox.Show("Товар успешно добавлен!");
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnAddTypeProduct_Click(object sender, RoutedEventArgs e)
        {
            Window windowType = new AddType();
            windowType.ShowDialog();
            cmbTypeProduct.ItemsSource = null;
            cmbTypeProduct.ItemsSource = DbConn.db.TypeProduct.ToList();
        }

        
    }
}
