﻿// <auto-generated />
using System;
using HomeService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeService.Migrations
{
    [DbContext(typeof(HomeServiceContext))]
    [Migration("20220227105020_addRemarkToRequest")]
    partial class addRemarkToRequest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HomeService.Models.Area", b =>
                {
                    b.Property<int>("AreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AreaTlAr")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AreaTlEn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.HasKey("AreaId");

                    b.HasIndex("CityId");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("HomeService.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityTlAr")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CityTlEn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CityId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("HomeService.Models.Configuration", b =>
                {
                    b.Property<int>("ConfigurationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instgram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkedIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Twitter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConfigurationId");

                    b.ToTable("Configuration");
                });

            modelBuilder.Entity("HomeService.Models.ContactUs", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Msg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TransDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ContactId");

                    b.ToTable("ContactUs");
                });

            modelBuilder.Entity("HomeService.Models.Contract", b =>
                {
                    b.Property<int>("ContractId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("ContractSerial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("ContractId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UnitId");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("HomeService.Models.ContractService", b =>
                {
                    b.Property<int>("ContractServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("ContractServiceId");

                    b.HasIndex("ContractId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ContractService");
                });

            modelBuilder.Entity("HomeService.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Avenue")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Block")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BuildingNo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CivilId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Flat")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Floor")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullNameAr")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullNameEn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Mobile")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("NationalityId")
                        .HasColumnType("int");

                    b.Property<string>("PassportNo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Pic")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Tele1")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Tele2")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CustomerId");

                    b.HasIndex("AreaId");

                    b.HasIndex("NationalityId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("HomeService.Models.FAQ", b =>
                {
                    b.Property<int>("FAQId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnswerEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FAQId");

                    b.ToTable("FAQ");
                });

            modelBuilder.Entity("HomeService.Models.Nationality", b =>
                {
                    b.Property<int>("NationalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NationalityTlAr")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NationalityTlEn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("NationalityId");

                    b.ToTable("Nationality");
                });

            modelBuilder.Entity("HomeService.Models.Newsletter", b =>
                {
                    b.Property<int>("NewsletterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NewsletterId");

                    b.ToTable("Newsletter");
                });

            modelBuilder.Entity("HomeService.Models.PageContent", b =>
                {
                    b.Property<int>("PageContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PageContentId");

                    b.ToTable("PageContent");
                });

            modelBuilder.Entity("HomeService.Models.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PaymentMethodTlAr")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaymentMethodTlEn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("HomeService.Models.Receipt", b =>
                {
                    b.Property<int>("ReceiptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Amount")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ContractId")
                        .HasColumnType("int");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("int");

                    b.Property<string>("ReceiptSerial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReceiptServiceId")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VDate")
                        .HasColumnType("date");

                    b.HasKey("ReceiptId");

                    b.HasIndex("ContractId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("ReceiptServiceId");

                    b.ToTable("Receipt");
                });

            modelBuilder.Entity("HomeService.Models.ReceiptService", b =>
                {
                    b.Property<int>("ReceiptServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ReceiptServiceTlAr")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ReceiptServiceTlEn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ReceiptServiceId");

                    b.ToTable("ReceiptService");
                });

            modelBuilder.Entity("HomeService.Models.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContractId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsClosd")
                        .HasColumnType("bit");

                    b.Property<string>("IssueDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RequestDate")
                        .HasColumnType("datetime");

                    b.Property<int>("RequestStateId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ScheduleDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("ServiceCategoryId")
                        .HasColumnType("int");

                    b.Property<double?>("SparePartsCost")
                        .HasColumnType("float");

                    b.Property<string>("SparePartsDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TechDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TechDiagnosis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TechFixes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TechnicianId")
                        .HasColumnType("int");

                    b.Property<double?>("VisitCost")
                        .HasColumnType("float");

                    b.HasKey("RequestId");

                    b.HasIndex("ContractId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RequestStateId");

                    b.HasIndex("ServiceCategoryId");

                    b.HasIndex("TechnicianId");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("HomeService.Models.RequestLog", b =>
                {
                    b.Property<int>("RequestLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("VDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RequestLogId");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestLog");
                });

            modelBuilder.Entity("HomeService.Models.RequestSpareParts", b =>
                {
                    b.Property<int>("RequestSparePartsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int>("QTY")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<int>("SparePartId")
                        .HasColumnType("int");

                    b.Property<double?>("Total")
                        .HasColumnType("float");

                    b.HasKey("RequestSparePartsId");

                    b.HasIndex("RequestId");

                    b.HasIndex("SparePartId");

                    b.ToTable("RequestSpareParts");
                });

            modelBuilder.Entity("HomeService.Models.RequestState", b =>
                {
                    b.Property<int>("RequestStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RequestStateAr")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RequestStateEn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RequestStateId");

                    b.ToTable("RequestState");
                });

            modelBuilder.Entity("HomeService.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ServiceTlAr")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ServiceTlEn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ServiceId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("HomeService.Models.ServiceCategory", b =>
                {
                    b.Property<int>("ServiceCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ServiceCategoryTlAr")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ServiceCategoryTlEn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ServiceCategoryId");

                    b.ToTable("ServiceCategory");
                });

            modelBuilder.Entity("HomeService.Models.SparePart", b =>
                {
                    b.Property<int>("SparePartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("SparePartDescrition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SparePartTlAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SparePartTlEn")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SparePartId");

                    b.ToTable("SparePart");
                });

            modelBuilder.Entity("HomeService.Models.Technician", b =>
                {
                    b.Property<int>("TechnicianId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CivilId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullAddress")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("FullNameAr")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullNameEn")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Mobile")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("NationalityId")
                        .HasColumnType("int");

                    b.Property<string>("PassportNo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Pic")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tele")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TechnicianId");

                    b.HasIndex("NationalityId");

                    b.ToTable("Technician");
                });

            modelBuilder.Entity("HomeService.Models.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Avenue")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Block")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BuildingNo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Flat")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Floor")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Remarks")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Street")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UnitGroupId")
                        .HasColumnType("int");

                    b.Property<string>("UnitTile")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnitId");

                    b.HasIndex("AreaId");

                    b.HasIndex("UnitGroupId");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("HomeService.Models.UnitGroup", b =>
                {
                    b.Property<int>("UnitGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UnitGroupOrderIndex")
                        .HasColumnType("int");

                    b.Property<string>("UnitGroupTlAr")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UnitGroupTlEn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UnitGroupId");

                    b.ToTable("UnitGroup");
                });

            modelBuilder.Entity("HomeService.Models.Area", b =>
                {
                    b.HasOne("HomeService.Models.City", "City")
                        .WithMany("Area")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK_Area_City");

                    b.Navigation("City");
                });

            modelBuilder.Entity("HomeService.Models.Contract", b =>
                {
                    b.HasOne("HomeService.Models.Customer", "Customer")
                        .WithMany("Contract")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Contract_Customer")
                        .IsRequired();

                    b.HasOne("HomeService.Models.Unit", "Unit")
                        .WithMany("Contract")
                        .HasForeignKey("UnitId")
                        .HasConstraintName("FK_Contract_Unit")
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("HomeService.Models.ContractService", b =>
                {
                    b.HasOne("HomeService.Models.Contract", "Contract")
                        .WithMany("ContractService")
                        .HasForeignKey("ContractId")
                        .HasConstraintName("FK_ContractService_Contract")
                        .IsRequired();

                    b.HasOne("HomeService.Models.Service", "Service")
                        .WithMany("ContractService")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("FK_ContractService_Service")
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("HomeService.Models.Customer", b =>
                {
                    b.HasOne("HomeService.Models.Area", "Area")
                        .WithMany("Customer")
                        .HasForeignKey("AreaId")
                        .HasConstraintName("FK_Customer_Area");

                    b.HasOne("HomeService.Models.Nationality", "Nationality")
                        .WithMany("Customer")
                        .HasForeignKey("NationalityId")
                        .HasConstraintName("FK_Customer_Nationality");

                    b.Navigation("Area");

                    b.Navigation("Nationality");
                });

            modelBuilder.Entity("HomeService.Models.Receipt", b =>
                {
                    b.HasOne("HomeService.Models.Contract", "Contract")
                        .WithMany("Receipt")
                        .HasForeignKey("ContractId")
                        .HasConstraintName("FK_Receipt_Contract");

                    b.HasOne("HomeService.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Receipt")
                        .HasForeignKey("PaymentMethodId")
                        .HasConstraintName("FK_Receipt_PaymentMethod");

                    b.HasOne("HomeService.Models.ReceiptService", "ReceiptService")
                        .WithMany("Receipt")
                        .HasForeignKey("ReceiptServiceId")
                        .HasConstraintName("FK_Receipt_ReceiptService");

                    b.Navigation("Contract");

                    b.Navigation("PaymentMethod");

                    b.Navigation("ReceiptService");
                });

            modelBuilder.Entity("HomeService.Models.Request", b =>
                {
                    b.HasOne("HomeService.Models.Contract", "Contract")
                        .WithMany("Request")
                        .HasForeignKey("ContractId");

                    b.HasOne("HomeService.Models.Customer", "Customer")
                        .WithMany("Request")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Request_Customer")
                        .IsRequired();

                    b.HasOne("HomeService.Models.RequestState", "RequestState")
                        .WithMany("Request")
                        .HasForeignKey("RequestStateId")
                        .HasConstraintName("FK_Request_RequestState")
                        .IsRequired();

                    b.HasOne("HomeService.Models.ServiceCategory", "ServiceCategory")
                        .WithMany("Request")
                        .HasForeignKey("ServiceCategoryId");

                    b.HasOne("HomeService.Models.Technician", "Technician")
                        .WithMany("Request")
                        .HasForeignKey("TechnicianId");

                    b.Navigation("Contract");

                    b.Navigation("Customer");

                    b.Navigation("RequestState");

                    b.Navigation("ServiceCategory");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("HomeService.Models.RequestLog", b =>
                {
                    b.HasOne("HomeService.Models.Request", "Request")
                        .WithMany("RequestLog")
                        .HasForeignKey("RequestId")
                        .HasConstraintName("FK_RequestLog_Request")
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("HomeService.Models.RequestSpareParts", b =>
                {
                    b.HasOne("HomeService.Models.Request", "Request")
                        .WithMany("RequestSpareParts")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomeService.Models.SparePart", "SparePart")
                        .WithMany("RequestSpareParts")
                        .HasForeignKey("SparePartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");

                    b.Navigation("SparePart");
                });

            modelBuilder.Entity("HomeService.Models.Technician", b =>
                {
                    b.HasOne("HomeService.Models.Nationality", "Nationality")
                        .WithMany("Technician")
                        .HasForeignKey("NationalityId");

                    b.Navigation("Nationality");
                });

            modelBuilder.Entity("HomeService.Models.Unit", b =>
                {
                    b.HasOne("HomeService.Models.Area", "Area")
                        .WithMany("Unit")
                        .HasForeignKey("AreaId")
                        .HasConstraintName("FK_Unit_Area");

                    b.HasOne("HomeService.Models.UnitGroup", "UnitGroup")
                        .WithMany("Unit")
                        .HasForeignKey("UnitGroupId")
                        .HasConstraintName("FK_Unit_UnitGroup")
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("UnitGroup");
                });

            modelBuilder.Entity("HomeService.Models.Area", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("HomeService.Models.City", b =>
                {
                    b.Navigation("Area");
                });

            modelBuilder.Entity("HomeService.Models.Contract", b =>
                {
                    b.Navigation("ContractService");

                    b.Navigation("Receipt");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("HomeService.Models.Customer", b =>
                {
                    b.Navigation("Contract");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("HomeService.Models.Nationality", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("HomeService.Models.PaymentMethod", b =>
                {
                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("HomeService.Models.ReceiptService", b =>
                {
                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("HomeService.Models.Request", b =>
                {
                    b.Navigation("RequestLog");

                    b.Navigation("RequestSpareParts");
                });

            modelBuilder.Entity("HomeService.Models.RequestState", b =>
                {
                    b.Navigation("Request");
                });

            modelBuilder.Entity("HomeService.Models.Service", b =>
                {
                    b.Navigation("ContractService");
                });

            modelBuilder.Entity("HomeService.Models.ServiceCategory", b =>
                {
                    b.Navigation("Request");
                });

            modelBuilder.Entity("HomeService.Models.SparePart", b =>
                {
                    b.Navigation("RequestSpareParts");
                });

            modelBuilder.Entity("HomeService.Models.Technician", b =>
                {
                    b.Navigation("Request");
                });

            modelBuilder.Entity("HomeService.Models.Unit", b =>
                {
                    b.Navigation("Contract");
                });

            modelBuilder.Entity("HomeService.Models.UnitGroup", b =>
                {
                    b.Navigation("Unit");
                });
#pragma warning restore 612, 618
        }
    }
}