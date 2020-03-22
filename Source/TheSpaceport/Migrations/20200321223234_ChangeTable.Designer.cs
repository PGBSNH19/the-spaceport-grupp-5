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
    [Migration("20200321223234_ChangeTable")]
    partial class ChangeTable
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

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ShipID")
                        .HasColumnType("int");

                    b.HasKey("PersonID");

                    b.HasIndex("ShipID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("TheSpaceport.Ship", b =>
                {
                    b.Property<int>("ShipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ShipLength")
                        .HasColumnType("int");

                    b.Property<string>("ShipName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShipID");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("TheSpaceport.Person", b =>
                {
                    b.HasOne("TheSpaceport.Ship", null)
                        .WithMany("Persons")
                        .HasForeignKey("ShipID");
                });
#pragma warning restore 612, 618
        }
    }
}
