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

        private void home_Click(object sender, RoutedEventArgs e)
        {



            content.Children.Clear();


            var nc = new Home();

            content.Children.Add(nc);
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<d_destination> lidest = db.d_destination.ToList();
            foreach (d_destination element in lidest)
              comboBoxCities.Items.Add(element.d_city);
            

           
        }

        private void AddHotelBN_Click(object sender, RoutedEventArgs e)
        {
            h_hotel hotel = new h_hotel();

            hotel.h_name = textBoxName.Text;
            hotel.h_stars =Convert.ToInt16(textBoxStars.Text);
            hotel.h_address = textBoxAddress.Text;
            hotel.h_description = textBoxDescr.Text;
            hotel.h_zip =Convert.ToInt16(textBoxZip.Text);
            hotel.h_d_city = comboBoxCities.SelectedValue.ToString();
            hotel.h_d_country = comboBoxCountries.SelectedValue.ToString();

        }
    }
}
