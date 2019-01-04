using System;
using System.Collections.Generic;
using System.Text;
using Entity.Model;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entity.Base
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        { }
        public ApplicationDbContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<UserRole> UserRoles{ get; set; }
        public DbSet<Hobby> Hobbies{ get; set; }
        public DbSet<Country> Countries{ get; set; }
        public DbSet<Configuration> Configurations{ get; set; }
        public DbSet<UserRelationShip> UserRelationShips{ get; set; }
        public DbSet<SignalR> SignalR { get; set; }
        public DbSet<SignalRoom> SignalRooms { get; set; }
        public DbSet<Message> Messages{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            var appSettings = ApplicationSetting.Get();
            var defaultConnection = appSettings.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(defaultConnection);
        }
    }
}
