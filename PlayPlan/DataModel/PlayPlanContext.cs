using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace PlayPlan.DataModel
{
    internal class PlayPlanContext : DbContext
    {
        
        //private const string DBaseName = "Playplan.db";
        private readonly string DBaseName = ConfigurationManager.AppSettings.Get("DBaseName") ?? "Playplan.db";
        public DbSet<SettingsData> Settings { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicComment> TopicComments { get; set; }
        public string DbPath { get; }

        public PlayPlanContext() //(DbContextOptions options) : base(options)
        {
            var folder = Environment.CurrentDirectory;
            DbPath = System.IO.Path.Join(folder, DBaseName);
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SettingsData>().ToTable("Settings").HasIndex(e => e.ID).IsUnique();
            modelBuilder.Entity<Person>().ToTable("Persons").HasIndex(e => e.ID).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
