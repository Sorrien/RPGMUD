﻿// <auto-generated />
using System;
using MUDService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MUDService.Migrations
{
    [DbContext(typeof(MUDContext))]
    partial class MUDContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MUDService.Models.Cell", b =>
                {
                    b.Property<Guid>("CellId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<Guid>("InventoryId");

                    b.Property<Guid?>("WorldId");

                    b.Property<int>("X");

                    b.Property<int>("Y");

                    b.Property<int>("Z");

                    b.HasKey("CellId");

                    b.HasIndex("InventoryId");

                    b.HasIndex("WorldId");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("MUDService.Models.CharacterStats", b =>
                {
                    b.Property<Guid>("CharacterStatsId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BaseMaximumActionPoints");

                    b.Property<double>("BaseMaximumHealthPoints");

                    b.Property<double>("CurrentActionPoints");

                    b.Property<double>("CurrentHealthPoints");

                    b.Property<int>("Level");

                    b.HasKey("CharacterStatsId");

                    b.ToTable("CharacterStats");
                });

            modelBuilder.Entity("MUDService.Models.ChatUser", b =>
                {
                    b.Property<Guid>("ChatUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsMuted");

                    b.Property<DateTime>("LastMessageDate");

                    b.Property<string>("Platform");

                    b.Property<Guid>("PlayerEntityId");

                    b.Property<string>("UserId");

                    b.Property<string>("Username");

                    b.HasKey("ChatUserId");

                    b.HasIndex("PlayerEntityId");

                    b.ToTable("ChatUsers");
                });

            modelBuilder.Entity("MUDService.Models.Effect", b =>
                {
                    b.Property<Guid>("EffectId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AffectedStat");

                    b.Property<Guid?>("CharacterStatsId");

                    b.Property<TimeSpan>("Duration");

                    b.Property<string>("EffectName");

                    b.Property<DateTime>("EffectStarted");

                    b.Property<double>("EffectValue");

                    b.Property<Guid?>("ItemEntityId");

                    b.Property<int>("ModifyType");

                    b.Property<int>("TimingType");

                    b.HasKey("EffectId");

                    b.HasIndex("CharacterStatsId");

                    b.HasIndex("ItemEntityId");

                    b.ToTable("Effects");
                });

            modelBuilder.Entity("MUDService.Models.Entity", b =>
                {
                    b.Property<Guid>("EntityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid?>("InventoryId");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.HasKey("EntityId");

                    b.HasIndex("InventoryId");

                    b.ToTable("Entities");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Entity");
                });

            modelBuilder.Entity("MUDService.Models.Inventory", b =>
                {
                    b.Property<Guid>("InventoryId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("InventoryId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("MUDService.Models.World", b =>
                {
                    b.Property<Guid>("WorldId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("WorldId");

                    b.ToTable("Worlds");
                });

            modelBuilder.Entity("MUDService.Models.Character", b =>
                {
                    b.HasBaseType("MUDService.Models.Entity");

                    b.Property<Guid?>("EquippedWeaponEntityId");

                    b.Property<Guid?>("InventoryId1");

                    b.Property<Guid?>("StatsCharacterStatsId");

                    b.HasIndex("EquippedWeaponEntityId");

                    b.HasIndex("InventoryId1");

                    b.HasIndex("StatsCharacterStatsId");

                    b.ToTable("Character");

                    b.HasDiscriminator().HasValue("Character");
                });

            modelBuilder.Entity("MUDService.Models.Item", b =>
                {
                    b.HasBaseType("MUDService.Models.Entity");


                    b.ToTable("Item");

                    b.HasDiscriminator().HasValue("Item");
                });

            modelBuilder.Entity("MUDService.Models.NonPlayerCharacter", b =>
                {
                    b.HasBaseType("MUDService.Models.Character");


                    b.ToTable("NonPlayerCharacter");

                    b.HasDiscriminator().HasValue("NonPlayerCharacter");
                });

            modelBuilder.Entity("MUDService.Models.Player", b =>
                {
                    b.HasBaseType("MUDService.Models.Character");


                    b.ToTable("Player");

                    b.HasDiscriminator().HasValue("Player");
                });

            modelBuilder.Entity("MUDService.Models.Equipment", b =>
                {
                    b.HasBaseType("MUDService.Models.Item");

                    b.Property<Guid?>("CharacterEntityId");

                    b.Property<double>("ResistAmount");

                    b.Property<int>("ResistType");

                    b.Property<int>("SlotType");

                    b.HasIndex("CharacterEntityId");

                    b.ToTable("Equipment");

                    b.HasDiscriminator().HasValue("Equipment");
                });

            modelBuilder.Entity("MUDService.Models.Weapon", b =>
                {
                    b.HasBaseType("MUDService.Models.Item");

                    b.Property<double>("BaseAPCost");

                    b.Property<double>("BaseDamageValue");

                    b.Property<int>("DamageType");

                    b.ToTable("Weapon");

                    b.HasDiscriminator().HasValue("Weapon");
                });

            modelBuilder.Entity("MUDService.Models.Cell", b =>
                {
                    b.HasOne("MUDService.Models.Inventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MUDService.Models.World", "World")
                        .WithMany("Cells")
                        .HasForeignKey("WorldId");
                });

            modelBuilder.Entity("MUDService.Models.ChatUser", b =>
                {
                    b.HasOne("MUDService.Models.Player")
                        .WithMany("ChatUsers")
                        .HasForeignKey("PlayerEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MUDService.Models.Effect", b =>
                {
                    b.HasOne("MUDService.Models.CharacterStats")
                        .WithMany("Effects")
                        .HasForeignKey("CharacterStatsId");

                    b.HasOne("MUDService.Models.Item")
                        .WithMany("Effects")
                        .HasForeignKey("ItemEntityId");
                });

            modelBuilder.Entity("MUDService.Models.Entity", b =>
                {
                    b.HasOne("MUDService.Models.Inventory")
                        .WithMany("Entities")
                        .HasForeignKey("InventoryId");
                });

            modelBuilder.Entity("MUDService.Models.Character", b =>
                {
                    b.HasOne("MUDService.Models.Weapon", "EquippedWeapon")
                        .WithMany()
                        .HasForeignKey("EquippedWeaponEntityId");

                    b.HasOne("MUDService.Models.Inventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId1");

                    b.HasOne("MUDService.Models.CharacterStats", "Stats")
                        .WithMany()
                        .HasForeignKey("StatsCharacterStatsId");
                });

            modelBuilder.Entity("MUDService.Models.Equipment", b =>
                {
                    b.HasOne("MUDService.Models.Character")
                        .WithMany("WornEquipment")
                        .HasForeignKey("CharacterEntityId");
                });
#pragma warning restore 612, 618
        }
    }
}
