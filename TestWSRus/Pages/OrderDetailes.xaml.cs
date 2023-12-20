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
    /// Логика взаимодействия для OrderDetailes.xaml
    /// </summary>
    public partial class OrderDetailes : Page
    {
        public OrderDetailes()
        {
            InitializeComponent();
            int id = (int)App.Current.Resources["OrderID"];
            try
            {
                decimal productPrice = 0;
                decimal price = 0;
                int quantity = 0;
                List<ProductOrder> productOrders = DbConn.db.ProductOrder.Where(p => p.OrderId == id).ToList();
                foreach (var products in productOrders)
                {
                    price = DbConn.db.Product.Where(p => p.IdProduct == products.ProductId).FirstOrDefault().Price;
                    quantity = products.Quantity;
                    productPrice = price * quantity;
                }
                lblCostOrder.Content = productPrice;
                DateTime dateTime = DbConn.db.Order.Where(p => p.IdOrder == id).FirstOrDefault().Date;
                lblDateOrder.Content = dateTime.ToString("dd.MM.yyyy");
                var ident = DbConn.db.Order.Where(p => p.IdOrder == id).FirstOrDefault();
                dgrdDetails.ItemsSource = DbConn.db.ProductOrder.Where(p => p.OrderId == ident.IdOrder).ToList();
            }
            catch
            {
                
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
