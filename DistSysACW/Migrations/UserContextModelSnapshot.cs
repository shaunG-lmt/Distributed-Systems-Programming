﻿// <auto-generated />
using System;
using DistSysACW.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DistSysACW.Migrations
{
    [DbContext(typeof(UserContext))]
    partial class UserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DistSysACW.Models.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LogDateTime");

                    b.Property<string>("LogString");

                    b.Property<string>("UserApiKey");

                    b.HasKey("LogId");

                    b.HasIndex("UserApiKey");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("DistSysACW.Models.Logs_Archive", b =>
                {
                    b.Property<int>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LogApiKey");

                    b.Property<DateTime>("LogDateTime");

                    b.Property<string>("LogString");

                    b.HasKey("LogID");

                    b.ToTable("Logs_Archives");
                });

            modelBuilder.Entity("DistSysACW.Models.User", b =>
                {
                    b.Property<string>("ApiKey")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Role");

                    b.Property<string>("UserName");

                    b.HasKey("ApiKey");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DistSysACW.Models.Log", b =>
                {
                    b.HasOne("DistSysACW.Models.User")
                        .WithMany("Logs")
                        .HasForeignKey("UserApiKey");
                });
#pragma warning restore 612, 618
        }
    }
}
