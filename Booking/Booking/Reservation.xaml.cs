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
                comboBoxCities.Items.Add(element.d_city+","+element.d_country);

            for(int i=1; i < 7; i++)
            {
                comboBoxBeds.Items.Add(i);
            }
        }

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            string destination = comboBoxCities.SelectedItem.ToString();
            string[] arr = destination.Split(',');
            bool booked = false;
            List<h_hotel> lihotels = new List<h_hotel>();
            List<r_room> lirooms = new List<r_room>();

            foreach (h_hotel element in db.h_hotel)
            {
                if (element.h_d_city == arr[0] && element.h_d_country == arr[1])
                {
                    foreach (r_room room in db.r_room)
                    {
                        if (room.r_beds == int.Parse(comboBoxBeds.SelectedItem.ToString()))
                        {
                            if (room.r_h_hotel == element.h_hotelid)
                            {
                                foreach (re_reservation reservation in db.re_reservation)
                                {
                                    if (reservation.re_r_room == room.r_number)
                                    {
                                        if ((checkInDP.SelectedDate.Value < reservation.re_checkOut && checkInDP.SelectedDate.Value > reservation.re_checkIn) || (checkOutDP.SelectedDate.Value > reservation.re_checkIn && checkOutDP.SelectedDate.Value < reservation.re_checkOut))
                                        {
                                            booked = true;
                                        }
                                    }
                                }
                                if (booked == false)
                                {
                                    lirooms.Add(room);
                                    lihotels.Add(element);
                                }
                            }
                        }
                    }
                }


                listBoxHotels.ItemsSource = lihotels;
                listBoxRooms.ItemsSource = lirooms;
            }

        }

        private void ReserveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxRooms.SelectedItem != null)
            {
                r_room room = (r_room)listBoxRooms.SelectedItem;
                re_reservation reserv = new re_reservation();

                reserv.re_r_room = room.r_number;
                reserv.re_checkIn = checkInDP.SelectedDate.Value;
                reserv.re_checkOut = checkOutDP.SelectedDate.Value;

                db.re_reservation.Add(reserv);
                db.SaveChanges();
                textBlock.Text = "Room reserved!";
            }
        }
    }
}
