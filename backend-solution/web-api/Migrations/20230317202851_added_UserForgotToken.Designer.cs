﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using web_api;

#nullable disable

namespace web_api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230317202851_added_UserForgotToken")]
    partial class added_UserForgotToken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("web_api.GameModel.Creatures.Creature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Agility")
                        .HasColumnType("int");

                    b.Property<int>("Alignment")
                        .HasColumnType("int");

                    b.Property<int>("Charisma")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("Discipline")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Intellect")
                        .HasColumnType("int");

                    b.Property<int>("Lucidity")
                        .HasColumnType("int");

                    b.Property<int>("Morale")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PartyId")
                        .HasColumnType("int");

                    b.Property<int>("Power")
                        .HasColumnType("int");

                    b.Property<int>("Temperament")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartyId");

                    b.ToTable("Creatures");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Creature");
                });

            modelBuilder.Entity("web_api.GameModel.Items.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatureId")
                        .HasColumnType("int");

                    b.Property<int?>("PartyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatureId");

                    b.HasIndex("PartyId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("web_api.GameModel.OGIs.OngoingGameplayInteraction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PartyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduledAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ScheduledFor")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartyId");

                    b.ToTable("OGIs");

                    b.HasDiscriminator<int>("Type");
                });

            modelBuilder.Entity("web_api.GameModel.Party", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ActionId")
                        .HasColumnType("int");

                    b.Property<int?>("DestinationAxialQ")
                        .HasColumnType("int");

                    b.Property<int?>("DestinationAxialR")
                        .HasColumnType("int");

                    b.Property<int>("Electrum")
                        .HasColumnType("int");

                    b.Property<int?>("LeaderId")
                        .HasColumnType("int");

                    b.Property<int>("LocationAxialQ")
                        .HasColumnType("int");

                    b.Property<int>("LocationAxialR")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAxialQ", "DestinationAxialR");

                    b.HasIndex("LocationAxialQ", "LocationAxialR");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("web_api.GameModel.Sites.SiteBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SiteBase");

                    b.HasDiscriminator<int>("Type").HasValue(0);
                });

            modelBuilder.Entity("web_api.GameModel.Worldmap.HexTile", b =>
                {
                    b.Property<int>("AxialQ")
                        .HasColumnType("int");

                    b.Property<int>("AxialR")
                        .HasColumnType("int");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("SiteId")
                        .HasColumnType("int");

                    b.Property<int>("TravelTime")
                        .HasColumnType("int");

                    b.HasKey("AxialQ", "AxialR");

                    b.HasIndex("SiteId");

                    b.ToTable("HexTiles");
                });

            modelBuilder.Entity("web_api.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AvatarId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("RegistrationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("TokenCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("TokenExpires")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("web_api.UserConfirmation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConfirmationId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Confirmations");
                });

            modelBuilder.Entity("web_api.UserResetToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UserResetTokens");
                });

            modelBuilder.Entity("web_api.GameModel.Creatures.Avatar", b =>
                {
                    b.HasBaseType("web_api.GameModel.Creatures.Creature");

                    b.HasDiscriminator().HasValue("Avatar");
                });

            modelBuilder.Entity("web_api.GameModel.OGIs.TravelOGI", b =>
                {
                    b.HasBaseType("web_api.GameModel.OGIs.OngoingGameplayInteraction");

                    b.Property<int>("AxialQ")
                        .HasColumnType("int");

                    b.Property<int>("AxialR")
                        .HasColumnType("int");

                    b.ToTable("OGIs");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("web_api.GameModel.Sites.AncientTree", b =>
                {
                    b.HasBaseType("web_api.GameModel.Sites.SiteBase");

                    b.HasDiscriminator().HasValue(206);
                });

            modelBuilder.Entity("web_api.GameModel.Sites.Banner", b =>
                {
                    b.HasBaseType("web_api.GameModel.Sites.SiteBase");

                    b.HasDiscriminator().HasValue(201);
                });

            modelBuilder.Entity("web_api.GameModel.Sites.Camp", b =>
                {
                    b.HasBaseType("web_api.GameModel.Sites.SiteBase");

                    b.HasDiscriminator().HasValue(202);
                });

            modelBuilder.Entity("web_api.GameModel.Sites.Chest", b =>
                {
                    b.HasBaseType("web_api.GameModel.Sites.SiteBase");

                    b.Property<int>("ElectrumAmount")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue(204);
                });

            modelBuilder.Entity("web_api.GameModel.Sites.Crystal", b =>
                {
                    b.HasBaseType("web_api.GameModel.Sites.SiteBase");

                    b.HasDiscriminator().HasValue(205);
                });

            modelBuilder.Entity("web_api.GameModel.Sites.Forest", b =>
                {
                    b.HasBaseType("web_api.GameModel.Sites.SiteBase");

                    b.HasDiscriminator().HasValue(101);
                });

            modelBuilder.Entity("web_api.GameModel.Sites.House", b =>
                {
                    b.HasBaseType("web_api.GameModel.Sites.SiteBase");

                    b.HasDiscriminator().HasValue(203);
                });

            modelBuilder.Entity("web_api.GameModel.Sites.SiteInteractive", b =>
                {
                    b.HasBaseType("web_api.GameModel.Sites.SiteBase");

                    b.HasDiscriminator().HasValue(200);
                });

            modelBuilder.Entity("web_api.GameModel.Sites.SiteObstacle", b =>
                {
                    b.HasBaseType("web_api.GameModel.Sites.SiteBase");

                    b.Property<int>("testProperty")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue(100);
                });

            modelBuilder.Entity("web_api.GameModel.Creatures.Creature", b =>
                {
                    b.HasOne("web_api.GameModel.Party", null)
                        .WithMany("Members")
                        .HasForeignKey("PartyId");
                });

            modelBuilder.Entity("web_api.GameModel.Items.Item", b =>
                {
                    b.HasOne("web_api.GameModel.Creatures.Creature", null)
                        .WithMany("Backpack")
                        .HasForeignKey("CreatureId");

                    b.HasOne("web_api.GameModel.Party", null)
                        .WithMany("Inventory")
                        .HasForeignKey("PartyId");
                });

            modelBuilder.Entity("web_api.GameModel.OGIs.OngoingGameplayInteraction", b =>
                {
                    b.HasOne("web_api.GameModel.Party", null)
                        .WithMany()
                        .HasForeignKey("PartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("web_api.GameModel.Party", b =>
                {
                    b.HasOne("web_api.GameModel.Worldmap.HexTile", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationAxialQ", "DestinationAxialR");

                    b.HasOne("web_api.GameModel.Worldmap.HexTile", "Location")
                        .WithMany()
                        .HasForeignKey("LocationAxialQ", "LocationAxialR")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("web_api.GameModel.Worldmap.HexTile", b =>
                {
                    b.HasOne("web_api.GameModel.Sites.SiteBase", "Site")
                        .WithMany()
                        .HasForeignKey("SiteId");

                    b.Navigation("Site");
                });

            modelBuilder.Entity("web_api.User", b =>
                {
                    b.HasOne("web_api.GameModel.Creatures.Avatar", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId");

                    b.Navigation("Avatar");
                });

            modelBuilder.Entity("web_api.GameModel.Creatures.Creature", b =>
                {
                    b.Navigation("Backpack");
                });

            modelBuilder.Entity("web_api.GameModel.Party", b =>
                {
                    b.Navigation("Inventory");

                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
