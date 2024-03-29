﻿// <auto-generated />
using System;
using MagicVilla_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_VillaAPI.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "Pool",
                            CreatedDate = new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(3935),
                            Details = "This is a 3 bedroom villa",
                            ImageUrl = "https://www.villarenters.com/content/images/properties/property_1/property_1_1.jpg",
                            Name = "Villa 1",
                            Occupancy = 6,
                            Rate = 1000.0,
                            Sqft = 2000,
                            UpdatedDate = new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(4037)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "Pool",
                            CreatedDate = new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(4043),
                            Details = "This is a 4 bedroom villa",
                            ImageUrl = "https://www.villarenters.com/content/images/properties/property_1/property_1_1.jpg",
                            Name = "Villa 2",
                            Occupancy = 8,
                            Rate = 2000.0,
                            Sqft = 3000,
                            UpdatedDate = new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(4047)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "Pool",
                            CreatedDate = new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(4050),
                            Details = "This is a 5 bedroom villa",
                            ImageUrl = "https://www.villarenters.com/content/images/properties/property_1/property_1_1.jpg",
                            Name = "Villa 3",
                            Occupancy = 10,
                            Rate = 3000.0,
                            Sqft = 4000,
                            UpdatedDate = new DateTime(2024, 1, 22, 18, 53, 57, 171, DateTimeKind.Local).AddTicks(4054)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
