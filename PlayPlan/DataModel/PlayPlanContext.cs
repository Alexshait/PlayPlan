﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PlayPlan.DataModel
{
    internal class PlayPlanContext : DbContext
    {
        private const string DBaseName = "Playplan.db";
        public DbSet<SettingsData> Settings { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicComment> TopicComments { get; set; }
        public string DbPath { get; }

        public PlayPlanContext()
        {
            var folder = Environment.CurrentDirectory;
            DbPath = System.IO.Path.Join(folder, DBaseName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SettingsData>(entity =>
            {
                // Set key for entity
                entity.HasKey(p => p.ApiID);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
