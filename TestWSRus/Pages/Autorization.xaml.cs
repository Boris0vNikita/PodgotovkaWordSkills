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
using TestWSRus.options;

namespace TestWSRus.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Page
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = txbLogin.Text.Trim();
            string password = psbPass.Password.Trim();
            string link = "";


            HashPassword hash = new HashPassword();
            byte[] salt = hash.GeneralSalt();
            byte[] sha256Hash = hash.GenerationHash(password, salt);
            string sha256HashString = Convert.ToBase64String(sha256Hash);

            People person = authPeople(login, sha256HashString);

            if (person !=null &&login !=null && password != null)
            {
                switch (person.RoleId)
                {
                    case 1:
                        {
                            link = "/Pages/PageAdmin.xaml";
                            App.Current.Resources["RoleID"] = person.RoleId;
                            break;
                        }
                    case 2:
                        {
                            link = "/Pages/PagePerson.xaml";
                            App.Current.Resources["RoleID"] = person.RoleId;
                            break;
                        }
                }
                int peopleID = DbConn.db.People.Where(p => p.Login == login && p.Password == password).FirstOrDefault().IdPeople;
                App.Current.Resources["PeopleID"] = peopleID;
                NavigationService.GetNavigationService(this).Navigate(new Uri(link, UriKind.RelativeOrAbsolute));
            }
            else
            {
                if (login == "" && password == "")
                {
                    txbLogin.Background = Brushes.LightCoral;
                    psbPass.Background = Brushes.LightCoral;
                    MessageBox.Show("Для входа в систему надо ввести и пароль, и логин!");
                }
                if (password == "" && login != "")
                {
                    psbPass.Background = Brushes.LightCoral;
                    MessageBox.Show("Для входа в систему надо ввести и пароль!");
                }
                if (login == "" && password != "")
                {
                    txbLogin.Background = Brushes.LightCoral;
                    MessageBox.Show("Для входа в систему надо ввести и логин!");
                }
            }
        }

        private People authPeople(string login, string password)
        {
            try
            {
                List<People> listPeople = DbConn.db.People.ToList();

                if (login != null && password != null)
                {
                    foreach (People people in listPeople)
                    {
                        if (people.Login == login && people.Password == password)
                        {
                            return people;
                        }
                    }
                }
                
                MessageBox.Show("ПОльзователь не найден");
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Потеряна связь с БД {0}",e));
                return null;
            }
        }
    }
}
