﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tinyBank.Core.Data;

namespace tinyBank.app.Migrations
{
    [DbContext(typeof(BankDbContext))]
    partial class BankDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("tinyBank.Core.Model.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("AccountBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("CustomerId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("tinyBank.Core.Model.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerPaymentMethod")
                        .HasColumnType("int");

                    b.Property<string>("CustomerType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("tinyBank.Core.Model.CustomerTypes", b =>
                {
                    b.Property<string>("CustomerTypeName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CustomerTypeName");

                    b.HasIndex("CustomerTypeName")
                        .IsUnique();

                    b.ToTable("CustomerTypes");
                });

            modelBuilder.Entity("tinyBank.Core.Model.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("TransactionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTimeOffset>("TransactionCreated")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccountId");

                    b.HasIndex("TransactionId")
                        .IsUnique();

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("tinyBank.Core.Model.Account", b =>
                {
                    b.HasOne("tinyBank.Core.Model.Customer", null)
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("tinyBank.Core.Model.Transaction", b =>
                {
                    b.HasOne("tinyBank.Core.Model.Account", null)
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("tinyBank.Core.Model.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("tinyBank.Core.Model.Customer", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
