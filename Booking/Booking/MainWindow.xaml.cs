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
                
            }
            else
            {
                u_user user = new u_user();
                user.u_firstName = textFirstname.Text;
                user.u_lastName = textLastname.Text;
                //   user.u_username = textUsername.Text;
                user.u_password = textPassword.Text;

                List<u_user> liusers = db.u_user.ToList();
                if (liusers.Contains(user))
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

            }
            else
            {
                List<u_user> liusers = db.u_user.ToList();
                foreach (u_user element in liusers)
                {
                    if (element.u_password == logInPassword.Text) // element.u_username == logInUsername.Text &&
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
