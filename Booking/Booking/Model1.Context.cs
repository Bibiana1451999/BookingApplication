﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Booking
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<d_destination> d_destination { get; set; }
        public virtual DbSet<h_hotel> h_hotel { get; set; }
        public virtual DbSet<r_room> r_room { get; set; }
        public virtual DbSet<ra_rating> ra_rating { get; set; }
        public virtual DbSet<re_reservation> re_reservation { get; set; }
        public virtual DbSet<se_services> se_services { get; set; }
        public virtual DbSet<tr_typeOfRoom> tr_typeOfRoom { get; set; }
        public virtual DbSet<u_user> u_user { get; set; }
    }
}
