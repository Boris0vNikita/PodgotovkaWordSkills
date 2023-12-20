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
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        public Order()
        {
            InitializeComponent();
            dgrdOrders.ItemsSource = DbConn.db.Order.ToList();
            lblQuatitiOrders.Content = DbConn.db.Order.Count();
        }

        

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgrdOrders.SelectedItem != null)
            {
                int idClickElement = (dgrdOrders.SelectedItem as Infractucture.Order).IdOrder;

                App.Current.Resources["OrderID"] = idClickElement;

                Window deleteWindow = new DeleteWindow();

                deleteWindow.ShowDialog();

                if ((int)App.Current.Resources["ChoiceId"] == 1)
                {
                    var orderDelete = DbConn.db.Order.Where(p => p.IdOrder == idClickElement).FirstOrDefault();
                    List<ProductOrder> productOrders = DbConn.db.ProductOrder.Where(t => t.OrderId == idClickElement).ToList();

                    foreach (var product in productOrders)
                    {
                        try
                        {
                            DbConn.db.ProductOrder.Remove(product);
                            DbConn.db.SaveChanges();
                            DbConn.db.Order.Remove(orderDelete);
                            DbConn.db.SaveChanges();
                        }
                        catch
                        {
                        }
                    }
                    MessageBox.Show("Заказ удален");
                    dgrdOrders.ItemsSource = null;
                    dgrdOrders.ItemsSource = DbConn.db.Order.ToList();
                }
                if ((int)App.Current.Resources["ChoiceId"] == 2)
                {
                    MessageBox.Show("Заказ не удален");
                }

            }
            else
            {
                MessageBox.Show("Не выбран элемент для удаления");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnOderDetails_Click(object sender, RoutedEventArgs e)
        {
            //идентификатор выбранного элемента из таблицы грида
            int id = (dgrdOrders.SelectedItem as Infractucture.Order).IdOrder;
            //добавление идентификатора заказа в ресурсы для переноса на другую страницу
            App.Current.Resources["OrderID"] = id;
            //переход на страницу просмотра детализации заказа
            NavigationService.GetNavigationService(this).Navigate(new Uri("/Pages/OrderDetailes.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
