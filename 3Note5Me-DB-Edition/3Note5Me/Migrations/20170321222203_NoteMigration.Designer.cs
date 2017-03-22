using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using _3Note5Me.Model;

namespace _3Note5Me.Migrations
{
    [DbContext(typeof(NoteContext))]
    [Migration("20170321222203_NoteMigration")]
    partial class NoteMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("_3Note5Me.Model.Note", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("id");

                    b.ToTable("Notes");
                });
        }
    }
}
