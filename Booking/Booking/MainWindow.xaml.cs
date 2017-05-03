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
        public static object loggedInAcc;
        Entities db = new Entities();
        public MainWindow()
        {
            InitializeComponent();
            loggedInAcc = null;
       }
        static string sha256(string password)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        private void SignUpBTN_Click(object sender, RoutedEventArgs e)
        {
            blockError.Text = "";
            if (textUsername.Text == "" || textFirstname.Text == "" || textLastname.Text == "" || textPassword.Password == "" || repPass.Password == "")
                blockError.Text = "Please do not leave anything empty!";
            else
            {
                if (checkBoxSignup.IsChecked == true)
                {
                    ho_host host = new ho_host();
                    host.ho_hostname = textUsername.Text;
                    host.ho_fistname = textFirstname.Text;
                    host.ho_lastname = textLastname.Text;
                    host.ho_password = sha256(textPassword.Password);

                    List<ho_host> lihosts = db.ho_host.ToList();
                    foreach (ho_host element in lihosts)
                    {
                        if (element.ho_hostname == host.ho_hostname)
                            blockError.Text = "This host already exists, please choose another username!";
                    }

                    if (textPassword.Password == repPass.Password)
                    {
                        if (blockError.Text == "")
                        {
                            db.ho_host.Add(host);
                            db.SaveChanges();

                            addHotel.Visibility = Visibility.Visible;
                            reservation.Visibility = Visibility.Hidden;
                            loggedInAcc = host;
                            contentchange.Children.Clear();
                            var nc = new Home();
                            contentchange.Children.Add(nc);
                        }
                    }
                    else
                        blockError.Text = "The passwords do not match, rewrite them please!";
                }
                else
                {
                    u_user user = new u_user();
                    user.u_firstName = textFirstname.Text;
                    user.u_lastName = textLastname.Text;
                    user.u_username = textUsername.Text;
                    user.u_password = sha256(textPassword.Password);

                    List<u_user> liusers = db.u_user.ToList();
                    foreach (u_user element in liusers)
                    {
                        if (element.u_username == user.u_username)
                            blockError.Text = "This user already exists, please choose another username!";
                    }
                    if (textPassword.Password == repPass.Password)
                    {
                        if (blockError.Text == "")
                        {
                            db.u_user.Add(user);
                            db.SaveChanges();

                            loggedInAcc = user;
                            contentchange.Children.Clear();
                            var nc = new Home();
                            contentchange.Children.Add(nc);
                        }
                    }
                    else
                        blockError.Text = "The passwords do not match, rewrite them please!";
                }
            }
        }

        private void LogInBTN_Click(object sender, RoutedEventArgs e)
        {
            blockError.Text = "";
            if (logInUsername.Text == "" || logInPassword.Password == "")
                blockError.Text = "Please do not leave anything empty!";
            else
            {
                if (checkBoxLogin.IsChecked == true)
                {
                    List<ho_host> lihosts = db.ho_host.ToList();
                    foreach (ho_host element in lihosts)
                    {
                        if (element.ho_hostname == logInUsername.Text && element.ho_password == sha256(logInPassword.Password))
                        {
                            contentchange.Children.Clear();
                            var nc = new Home();
                            contentchange.Children.Add(nc);
                            loggedInAcc = element;
                            addHotel.Visibility = Visibility.Visible;
                            reservation.Visibility = Visibility.Hidden;
                        }
                    }
                    if (loggedInAcc == null)
                        blockError.Text = "This host does not exist, make sure the username/password is correct, or sign up if you haven't already!";
                }
                else
                {
                    List<u_user> liusers = db.u_user.ToList();
                    foreach (u_user element in liusers)
                    {
                        if (element.u_username == logInUsername.Text && element.u_password.Trim() == sha256(logInPassword.Password))
                        {
                            contentchange.Children.Clear();
                            var nc = new Home();
                            contentchange.Children.Add(nc);
                            loggedInAcc = element;
                        }

                    }
                    if (loggedInAcc == null)
                        blockError.Text = "This user does not exist, make sure the username/password is correct, or sign up if you haven't already!";
                }
            }
        }

        private void addHotel_Click(object sender, RoutedEventArgs e)
        {
            if (loggedInAcc!=null)
            {
                contentchange.Children.Clear();
                var nc = new AddHotel();
                contentchange.Children.Add(nc);
            }
        }


        private void reservation_Click(object sender, RoutedEventArgs e)
        {
            if (loggedInAcc != null)
            {
                contentchange.Children.Clear();
                var nc = new Reservation();
                contentchange.Children.Add(nc);
            }
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (loggedInAcc != null)
            {
                contentchange.Children.Clear();
                var nc = new Search();
                contentchange.Children.Add(nc);
            }
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            if (loggedInAcc != null)
            {
                contentchange.Children.Clear();
                var nc = new Home();
                contentchange.Children.Add(nc);
            }
        }

    }
}
