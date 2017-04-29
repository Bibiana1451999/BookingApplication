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
        }
    }

}
