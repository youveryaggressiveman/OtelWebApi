﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OtelApi.GlobalEntity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OtelEntities : DbContext
    {
        public OtelEntities()
            : base("name=OtelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AddressOfOtel> AddressOfOtel { get; set; }
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Discription> Discription { get; set; }
        public virtual DbSet<ImageOfOtel> ImageOfOtel { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Otel> Otel { get; set; }
        public virtual DbSet<Passport> Passport { get; set; }
        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<TypeRoom> TypeRoom { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
