﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlayPlan.DataModel;

#nullable disable

namespace PlayPlan.Migrations
{
    [DbContext(typeof(PlayPlanContext))]
    [Migration("20220408120059_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("PlayPlan.DataModel.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ParsePhrases")
                        .HasColumnType("TEXT");

                    b.Property<string>("PersonName")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.ToTable("Persons", (string)null);
                });

            modelBuilder.Entity("PlayPlan.DataModel.SettingsData", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ApiID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("GroupID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GroupName")
                        .HasColumnType("TEXT");

                    b.Property<string>("VkApiVer")
                        .HasColumnType("TEXT");

                    b.Property<string>("WindowHeigth")
                        .HasColumnType("TEXT");

                    b.Property<string>("WindowWidth")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.ToTable("Settings", (string)null);
                });

            modelBuilder.Entity("PlayPlan.DataModel.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("TopicCommentsAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TopicCreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TopicDateCreate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TopicDateUpdate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TopicID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TopicTitle")
                        .HasColumnType("TEXT");

                    b.Property<int>("TopicUpdatedBy")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("PlayPlan.DataModel.TopicComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<string>("CommentFrom")
                        .HasColumnType("TEXT");

                    b.Property<int>("CommentID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateComment")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateInput")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Participants")
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TopicId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Topic_ID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicComments");
                });

            modelBuilder.Entity("PlayPlan.DataModel.TopicComment", b =>
                {
                    b.HasOne("PlayPlan.DataModel.Topic", null)
                        .WithMany("TopicComments")
                        .HasForeignKey("TopicId");
                });

            modelBuilder.Entity("PlayPlan.DataModel.Topic", b =>
                {
                    b.Navigation("TopicComments");
                });
#pragma warning restore 612, 618
        }
    }
}