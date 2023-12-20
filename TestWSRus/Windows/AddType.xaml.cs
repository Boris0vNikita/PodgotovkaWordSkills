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
using TestWSRus.Infractucture;

namespace TestWSRus.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddType.xaml
    /// </summary>
    public partial class AddType : Window
    {
        public AddType()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txbNameTypeProduct.Text!=null)
            {
                TypeProduct type = new TypeProduct();

                type.IdTypeProduct = DbConn.db.TypeProduct.Max(p => p.IdTypeProduct) + 1;
                type.NameTypeProduct = txbNameTypeProduct.Text.Trim();
                try
                {
                    DbConn.db.TypeProduct.Add(type);
                    DbConn.db.SaveChanges();
                    MessageBox.Show("Тип товара добавлен");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
