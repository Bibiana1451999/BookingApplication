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
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class Reservation : UserControl
    {
        Entities db = new Entities();
        public Reservation()
        {
            InitializeComponent();
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<d_destination> lidest = db.d_destination.ToList();
            foreach (d_destination element in lidest)
                comboBoxCities.Items.Add(element.d_city);

            for(int i=1; i < 7; i++)
            {
                comboBoxPersons.Items.Add(i);
                comboBoxRooms.Items.Add(i);
            }
        }
    }
}
