using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Booking;
using System.Windows.Forms;

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
            /*
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;*/
        }


        private void searchBTN_Click(object sender, RoutedEventArgs e)
        {
            h_hotel seHotel = null;
            List<h_hotel> lihotels = db.h_hotel.ToList();
            foreach (h_hotel element in lihotels)
            {
                if (element.h_name.ToLower().Contains(searchBox.Text.ToLower()))
                    seHotel = element;
            }

            textBlockName.Content = seHotel.h_name;
            textBlockAddress.Content = seHotel.h_address;
            textBlockDescr.Text= seHotel.h_description;
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
                if(i != services.Count()-1)
                    ServicesText.Text = ServicesText.Text + services.ToArray()[i] + ", ";
                else
                    ServicesText.Text = ServicesText.Text + services.ToArray()[i] + ".";
            }
        }

    }

}
