﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheSpaceport;

namespace TheSpaceport.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200322111146_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheSpaceport.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpaceshipShipID")
                        .HasColumnType("int");

                    b.HasKey("PersonID");

                    b.HasIndex("SpaceshipShipID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("TheSpaceport.Spaceship", b =>
                {
                    b.Property<int>("ShipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumberOfDays")
                        .HasColumnType("int");

                    b.Property<int>("PricePerDay")
                        .HasColumnType("int");

                    b.Property<string>("ShipName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShipID");

                    b.ToTable("Spaceships");
                });

            modelBuilder.Entity("TheSpaceport.Person", b =>
                {
                    b.HasOne("TheSpaceport.Spaceship", null)
                        .WithMany("Persons")
                        .HasForeignKey("SpaceshipShipID");
                });
#pragma warning restore 612, 618
        }
    }
}
