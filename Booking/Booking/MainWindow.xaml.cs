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

namespace Booking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities db = new Entities();
        public MainWindow()
        {
            InitializeComponent();
        }


        private void reservation_Click(object sender, RoutedEventArgs e)
        {
            content.Children.Clear();

           
            var nc = new Reservation();

            content.Children.Add(nc);




        }

      
        private void search_Click(object sender, RoutedEventArgs e)
        {
            content.Children.Clear();


            var nc = new Search();

            content.Children.Add(nc);

        }

        private void home_Click(object sender, RoutedEventArgs e)
        {



            content.Children.Clear();


            var nc = new Home();

            content.Children.Add(nc);
        }

        private void addHotel_Click(object sender, RoutedEventArgs e)
        {

            content.Children.Clear();


            var nc = new AddHotel();

            content.Children.Add(nc);
            //

        }

        private void SignUpBTN_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSignup.IsChecked == true)
            {
                ho_host host = new ho_host();
                host.ho_hostname = textUsername.Text;
                host.ho_fistname = textFirstname.Text;
                host.ho_lastname = textLastname.Text;
                host.ho_password = textPassword.Text;

                if (db.ho_host.Contains(host))
                    blockError.Text = "This host already exists, please choose another username!";
                else
                {
                    db.ho_host.Add(host);
                    db.SaveChanges();
                }
            }
            else
            {
                u_user user = new u_user();
                user.u_firstName = textFirstname.Text;
                user.u_lastName = textLastname.Text;
                user.u_username = textUsername.Text;
                user.u_password = textPassword.Text;

                 if (db.u_user.Contains(user))
                     blockError.Text = "This user already exists, please choose another username!";
                 else
                 {
                     db.u_user.Add(user);
                     db.SaveChanges();
                 }
            }
        }

        private void LogInBTN_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxLogin.IsChecked == true)
            {
                List<ho_host> lihosts = db.ho_host?.ToList();
                foreach(ho_host element in lihosts)
                {
                    if(element.ho_hostname == logInUsername.Text && element.ho_password == logInPassword.Text)
                    {
                        content.Children.Clear();
                        var nc = new Home();
                        content.Children.Add(nc);
                    }
                    else
                    {
                        blockError.Text = "This host does not exist, make sure the username/password is correct, or sign up if you haven't already!";
                    }
                }
            }
            else
            {
                List<u_user> liusers = db.u_user?.ToList();
                foreach (u_user element in liusers)
                {
                    if (element.u_username == logInUsername.Text && element.u_password == logInPassword.Text)
                    {
                        content.Children.Clear();
                        var nc = new Home();
                        content.Children.Add(nc);
                    }
                    else
                    {
                        blockError.Text = "This user does not exist, make sure the username/password is correct, or sign up if you haven't already!";
                    }
                }
            }

        }
    }
}
