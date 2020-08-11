﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Redirect.NET.Models;

namespace Redirect.NET.Migrations
{
    [DbContext(typeof(UrlContext))]
    partial class UrlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("Redirect.NET.Models.Url", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUsed")
                        .HasColumnType("TEXT");

                    b.Property<string>("RedirectUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("UsageCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Urls");
                });
#pragma warning restore 612, 618
        }
    }
}