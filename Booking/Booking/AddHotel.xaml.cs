﻿using System;
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
    /// Interaction logic for AddHotel.xaml
    /// </summary>
    public partial class AddHotel : UserControl
    {
        Entities db = new Entities();
        public AddHotel()
        {
            InitializeComponent();
        }

       

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<d_destination> lidest = db.d_destination.ToList();
            foreach (d_destination element in lidest)
              comboBoxCities.Items.Add(element.d_city +","+ element.d_country);
            

           
        }

        private void AddHotelBN_Click(object sender, RoutedEventArgs e)
        {
            h_hotel hotel = new h_hotel();
            ho_host host = (ho_host)MainWindow.loggedInAcc;

            string destination = comboBoxCities.SelectedItem.ToString();
            string[] arr = destination.Split(',');

            hotel.h_name = textBoxName.Text;
            hotel.h_stars =Convert.ToInt16(textBoxStars.Text);
            hotel.h_address = textBoxAddress.Text;
            hotel.h_description = textBoxDescr.Text;
            hotel.h_zip =Convert.ToInt16(textBoxZip.Text);
            hotel.h_d_city = arr[0];
            hotel.h_d_country = arr[1];
            hotel.h_ho_host = host.ho_hostname;

            var checking = (from h in db.h_hotel
                           where h.h_name == hotel.h_name && h.h_d_city == hotel.h_d_city && h.h_d_country == hotel.h_d_country
                           select h).ToList();

            if (checking.Count() == 0)
            {
                db.h_hotel.Add(hotel);
                db.SaveChanges();
                errorBlock.Text = "Hotel added!";
            }
            else
            {
                errorBlock.Text = "A Hotel with that name already exists in " + hotel.h_d_city + ", please choose another location!";
            }
            
        }
    }
}
