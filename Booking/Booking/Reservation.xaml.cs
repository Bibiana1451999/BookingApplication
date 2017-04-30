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

      

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (d_destination element in db.d_destination)
                comboBoxCities.Items.Add(element.d_city+","+element.d_country);

            for(int i=1; i < 7; i++)
                comboBoxBeds.Items.Add(i);
            

            checkInDP.SelectedDate = DateTime.Today;
            checkOutDP.SelectedDate = DateTime.Today;
        }

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";

            if (comboBoxCities.SelectedValue == null || comboBoxBeds.SelectedValue == null)
                textBlock.Text = "Please do not leave anything empty!";
            else
            {
                string destination = comboBoxCities.SelectedItem.ToString();
                string[] arr = destination.Split(',');
                bool booked = false;
                List<r_room> lirooms = new List<r_room>();


                if ((DateTime.Compare((DateTime)checkInDP.SelectedDate,(DateTime)checkOutDP.SelectedDate)<0) && (DateTime.Compare((DateTime)checkInDP.SelectedDate, DateTime.Today)>=0))
                {

                  /*  listBoxRooms.ItemsSource = (from h in db.h_hotel
                              from r in h.r_room
                              from res in r.re_reservation
                              where h.h_d_city == arr[0] 
                              && h.h_d_country == arr[1] 
                              && r.r_beds == int.Parse(comboBoxBeds.SelectedItem.ToString())
                              && (checkInDP.SelectedDate.Value >= res.re_checkOut || checkOutDP.SelectedDate.Value <= res.re_checkIn)
                              select r)?.ToList();
                    listBoxRooms.ItemsSource = lirooms;
                    */


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
                                            lirooms.Add(room);
                                        else
                                            booked = false;
                                    }
                                }
                            }
                        }
                    }
                        listBoxRooms.ItemsSource = lirooms;
                }
                else
                {
                    if (checkInDP.SelectedDate.Value < DateTime.Today)
                        textBlock.Text = "You can only make reservations today and in the days to come!";
                    else
                        textBlock.Text = "Check in should be earlier than check out!";
                }

            }
        }

        private void ReserveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxRooms.SelectedItem != null)
            {
                r_room room = (r_room)listBoxRooms.SelectedItem;
                re_reservation reserv = new re_reservation();
                u_user user = (u_user)MainWindow.loggedInAcc; 
                reserv.re_r_room = room.r_number;
                reserv.re_checkIn = checkInDP.SelectedDate.Value;
                reserv.re_checkOut = checkOutDP.SelectedDate.Value;
                reserv.re_u_username = user.u_username;

                db.re_reservation.Add(reserv);
                db.SaveChanges();
                textBlock.Text = "Room reserved!";
            }
        }
    }
}
