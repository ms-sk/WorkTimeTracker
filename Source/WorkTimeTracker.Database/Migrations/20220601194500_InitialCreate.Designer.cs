﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkTimeTracker.Database;

#nullable disable

namespace WorkTimeTracker.Database.Migrations
{
    [DbContext(typeof(WttContext))]
    [Migration("20220601194500_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("WorkTimeTracker.Database.Entity.DayEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Break")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("End")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Time")
                        .HasColumnType("REAL");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("WorkTimeTracker.Database.Entity.TaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DayEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Worktime")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("DayEntityId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("WorkTimeTracker.Database.Entity.TaskEntity", b =>
                {
                    b.HasOne("WorkTimeTracker.Database.Entity.DayEntity", null)
                        .WithMany("Tasks")
                        .HasForeignKey("DayEntityId");
                });

            modelBuilder.Entity("WorkTimeTracker.Database.Entity.DayEntity", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
