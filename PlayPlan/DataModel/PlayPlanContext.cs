using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.IO;
using System.Windows;

namespace PlayPlan.DataModel
{
    internal class PlayPlanContext : DbContext
    {
        

        public DbSet<SettingsData> Settings { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicComment> TopicComments { get; set; }
        private string _dbPath;

        public PlayPlanContext(string dbPath)
        {
            _dbPath = dbPath;
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={_dbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SettingsData>().ToTable("Settings").HasIndex(e => e.ID).IsUnique();
            modelBuilder.Entity<Person>().ToTable("Persons").HasIndex(e => e.ID).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
