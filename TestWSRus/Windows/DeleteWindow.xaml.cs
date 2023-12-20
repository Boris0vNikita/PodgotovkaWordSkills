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

namespace TestWSRus.Windows
{
    /// <summary>
    /// Логика взаимодействия для DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        public DeleteWindow()
        {
            InitializeComponent();
            lblIdOrder.Content = App.Current.Resources["OrderID"];
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            int choice;

            if ((sender as Button).Content.ToString() == "Да")
            {
                choice = 1;
                App.Current.Resources["ChoiceId"] = choice;
            }

            if ((sender as Button).Content.ToString() == "Нет")
            {
                choice = 2;
                App.Current.Resources["ChoiceId"] = choice;
            }
            this.Close();
        }
    }
}
