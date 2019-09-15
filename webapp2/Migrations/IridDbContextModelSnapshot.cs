﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using irid.Models;

namespace irid.Migrations
{
    [DbContext(typeof(IridDbContext))]
    partial class IridDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("irid.Models.Device", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Available");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<byte[]>("Data");

                    b.Property<string>("Name");

                    b.Property<byte>("Phi");

                    b.Property<byte>("Theta");

                    b.HasKey("ID");

                    b.ToTable("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
