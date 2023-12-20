
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using TestWSRus.Model;

namespace TestWSRus.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page
    {
        int idOrder = 0;
        List<ProductOut> productOuts = new List<ProductOut>();
        
        //создание листа товаров из базы
        List<Product> listProduct = DbConn.db.Product.ToList();
        public AddOrderPage()
        {
            InitializeComponent();
            
            cmbProduct.ItemsSource = DbConn.db.Product.ToList();
            dgrdOrder.ItemsSource = productOuts;
        }

        private void bntPlus_Click(object sender, RoutedEventArgs e)
        {
            string name = (dgrdOrder.SelectedItem as ProductOut).Name;
            var selectedProduct = productOuts.Find(p => p.Name.Contains(name));
            selectedProduct.Quantity++;

            dgrdOrder.ItemsSource = null;
            dgrdOrder.ItemsSource = productOuts;
        }

        private void bntMinus_Click(object sender, RoutedEventArgs e)
        {
            string name = (dgrdOrder.SelectedItem as ProductOut).Name;
            var selectedProduct = productOuts.Find(p => p.Name.Contains(name));
            selectedProduct.Quantity--;
            if (selectedProduct.Quantity < 1)
            {
                productOuts.Remove(selectedProduct);
            }
            dgrdOrder.ItemsSource = null;
            dgrdOrder.ItemsSource = productOuts;
        }

        private void bntCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            decimal priceProduct = 0;
            Infractucture.Order order = new Infractucture.Order();

            order.IdOrder = DbConn.db.Order.Max(p => p.IdOrder) + 1;
            order.Date = DateTime.Now;
            order.StatusId = 1;
            order.PeopleId = (int)App.Current.Resources["PeopleID"];

            DbConn.db.Order.Add(order);
            DbConn.db.SaveChanges();

            foreach (ProductOut productOut in dgrdOrder.Items)
            {
                priceProduct = DbConn.db.Product.Where(p => p.NameProduct == productOut.Name).FirstOrDefault().Price;

                ProductOrder productOrder = new ProductOrder();
                productOrder.IdProductOrder = DbConn.db.ProductOrder.Max(p => p.IdProductOrder) + 1;
                productOrder.OrderId = DbConn.db.Order.Max(o => o.IdOrder);
                productOrder.ProductId = DbConn.db.Product.Where(p => p.NameProduct == productOut.Name).FirstOrDefault().IdProduct;
                productOrder.Quantity = productOut.Quantity;

                try
                {
                    DbConn.db.ProductOrder.Add(productOrder);
                    DbConn.db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        lblEror.Content = string.Format("Object: " + validationError.Entry.Entity.ToString());
                        lblEror.Content = ("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            lblEror.Content = (err.ErrorMessage + "");
                        }
                    }
                }
            }
            MessageBox.Show("Заказ  сформирован!");

        }

        private void bntCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProduct.SelectedItem != null)
            {
                Product product = (cmbProduct.SelectedItem as Product);
                var findProduct = productOuts.Find(p => p.Name == product.NameProduct);
                if (findProduct!=null)
                {
                    findProduct.Quantity++;
                }
                else
                {
                    productOuts.Add(new ProductOut(product.NameProduct , 1 , idOrder));
                    idOrder++;
                }
                dgrdOrder.ItemsSource = null;
                dgrdOrder.ItemsSource = productOuts;

            }
            else
            {
                MessageBox.Show("Товар не выбран");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(dgrdOrder.SelectedItem != null)
            {
                string nameChoose = (dgrdOrder.SelectedItem as ProductOut).Name;
                var productDelete = productOuts.Find(p => p.Name.Contains(nameChoose));
                productOuts.Remove(productDelete);

                dgrdOrder.ItemsSource = null;
                dgrdOrder.ItemsSource = productOuts;
            }
            else
            {
                MessageBox.Show("Выьерите товар");
            }
        }

        private void btnCalculated_Click(object sender, RoutedEventArgs e)
        {
            if (dgrdOrder.Items != null)
            {
                decimal priceProduct = 0;
                decimal priceProducts = 0;
                decimal AllPriceProducts = 0;
                int quantity = 0;

                foreach(ProductOut product in dgrdOrder.Items)
                {
                    priceProduct = DbConn.db.Product.Where(p => p.NameProduct == product.Name).FirstOrDefault().Price;
                    quantity = product.Quantity;
                    priceProducts = quantity * priceProduct;
                    AllPriceProducts += priceProducts;
                    
                }
                txbResult.Text = AllPriceProducts.ToString();
            }
            else
            {
                MessageBox.Show("Не выбран товар");
            }
        }
    }
}
