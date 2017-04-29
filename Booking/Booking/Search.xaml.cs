using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Booking;

namespace Booking
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl
    {
        Entities db = new Entities();
        public Search()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (h_hotel element in db.h_hotel)
            {
                searchCB.Items.Add(element.h_name+","+element.h_d_city+ "," +element.h_d_country);
            }
        }


        private void searchBTN_Click(object sender, RoutedEventArgs e)
        {
            if (searchCB.Items.Contains(searchCB.SelectedValue))
            {
                h_hotel seHotel = new h_hotel();
                string destination = searchCB.SelectedItem.ToString();
                string[] arr = destination.Split(',');

                foreach (h_hotel element in db.h_hotel)
                {
                    if (element.h_name == arr[0] && element.h_d_city == arr[1] && element.h_d_country == arr[2])
                        seHotel = element;
                }

                if (seHotel != null)
                {

                    textBlockName.Content = seHotel.h_name;
                    textBlockAddress.Content = seHotel.h_address;
                    textBlockDescr.Text = seHotel.h_description;
                    TextCity.Content = seHotel.h_d_city;
                    TextCountry.Content = seHotel.h_d_country;
                    TextStars.Content = seHotel.h_stars;
                    TextZIP.Content = seHotel.h_zip;

                    var services = from h in db.h_hotel
                                   from s in h.se_services
                                   where h.h_hotelid == seHotel.h_hotelid
                                   select s.se_services1;

                    for (int i = 0; i < services.Count(); i++)
                    {
                        if (i != services.Count() - 1)
                            ServicesText.Text = ServicesText.Text + services.ToArray()[i] + ", ";
                        else
                            ServicesText.Text = ServicesText.Text + services.ToArray()[i] + ".";
                    }
                }
            }
        }

    }

}
