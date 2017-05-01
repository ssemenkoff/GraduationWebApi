using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Core_Server.Models.Data;

namespace CoreServer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170403171436_Images")]
    partial class Images
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Core_Server.Models.Data.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core_Server.Models.Data.Image", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Path");

                    b.Property<string>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Core_Server.Models.Data.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("PreviewImageId");

                    b.Property<double>("Price");

                    b.Property<string>("ProductId");

                    b.Property<string>("ShortDescription");

                    b.Property<int>("SubCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("PreviewImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Core_Server.Models.Data.SubCategory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryID");

                    b.Property<string>("Description");

                    b.Property<string>("ImageId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryID");

                    b.HasIndex("ImageId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("Core_Server.Models.Data.Category", b =>
                {
                    b.HasOne("Core_Server.Models.Data.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");
                });

            modelBuilder.Entity("Core_Server.Models.Data.Image", b =>
                {
                    b.HasOne("Core_Server.Models.Data.Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Core_Server.Models.Data.Product", b =>
                {
                    b.HasOne("Core_Server.Models.Data.Image", "PreviewImage")
                        .WithMany()
                        .HasForeignKey("PreviewImageId");

                    b.HasOne("Core_Server.Models.Data.Product")
                        .WithMany("RelatedProducts")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Core_Server.Models.Data.SubCategory", b =>
                {
                    b.HasOne("Core_Server.Models.Data.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryID");

                    b.HasOne("Core_Server.Models.Data.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");
                });
        }
    }
}
