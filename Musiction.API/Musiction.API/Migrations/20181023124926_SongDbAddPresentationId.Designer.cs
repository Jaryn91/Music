﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Musiction.API.Entities;
using System;

namespace Musiction.API.Migrations
{
    [DbContext(typeof(SongContext))]
    [Migration("20181023124926_SongDbAddPresentationId")]
    partial class SongDbAddPresentationId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Musiction.API.Entities.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("PresentationId")
                        .HasMaxLength(50);

                    b.Property<string>("YouTubeUrl")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}