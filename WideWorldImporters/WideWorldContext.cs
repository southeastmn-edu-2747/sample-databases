using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using mswanson2747_2017_ex1c2.Models;

namespace mswanson2747_2017_ex1c2.Data
{
    public class WideWorldContext : DbContext
    {
        // Constructor added for dependency injection
        public WideWorldContext(DbContextOptions<WideWorldContext> options) : base(options)
        { }

        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<StateProvinces> StateProvinces { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<RawSqlReturn> RawSqlReturn { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.ToTable("Countries", "Application");

                entity.HasIndex(e => e.CountryName)
                    .HasName("UQ_Application_Countries_CountryName")
                    .IsUnique();

                entity.HasIndex(e => e.FormalName)
                    .HasName("UQ_Application_Countries_FormalName")
                    .IsUnique();
                //entity.Property(e => e.CountryId)
                //    .HasColumnName("CountryID")
                //    .HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[CountryID])");

                entity.Property(e => e.CountryId)
                    .HasColumnName("CountryID"); ;

                entity.Property(e => e.Continent)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.CountryType).HasMaxLength(20);

                entity.Property(e => e.FormalName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.IsoAlpha3Code).HasMaxLength(3);

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Subregion)
                    .IsRequired()
                    .HasMaxLength(30);

                //entity.HasOne(d => d.LastEditedByNavigation)
                //    .WithMany(p => p.Countries)
                //    .HasForeignKey(d => d.LastEditedBy)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Application_Countries_Application_People");
            });

            modelBuilder.Entity<StateProvinces>(entity =>
            {
                entity.HasKey(e => e.StateProvinceId);

                entity.ToTable("StateProvinces", "Application");

                entity.HasIndex(e => e.CountryId)
                    .HasName("FK_Application_StateProvinces_CountryID");

                entity.HasIndex(e => e.SalesTerritory)
                    .HasName("IX_Application_StateProvinces_SalesTerritory");

                entity.HasIndex(e => e.StateProvinceName)
                    .HasName("UQ_Application_StateProvinces_StateProvinceName")
                    .IsUnique();

                //entity.Property(e => e.StateProvinceId)
                //    .HasColumnName("StateProvinceID")
                //    .HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[StateProvinceID])");
                entity.Property(e => e.StateProvinceId)
                    .HasColumnName("StateProvinceID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.SalesTerritory)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StateProvinceCode)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.StateProvinceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.StateProvinces)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Application_StateProvinces_CountryID_Application_Countries");

                //entity
                //    .HasOne(d => d.LastEditedByNavigation)
                //    .WithMany(p => p.StateProvinces)
                //    .HasForeignKey(d => d.LastEditedBy)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Application_StateProvinces_Application_People");
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("Cities", "Application");

                entity.HasIndex(e => e.StateProvinceId)
                    .HasName("FK_Application_Cities_StateProvinceID");

                //entity.Property(e => e.CityId)
                //    .HasColumnName("CityID")
                //    .HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[CityID])");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StateProvinceId).HasColumnName("StateProvinceID");

                //entity.HasOne(d => d.LastEditedByNavigation)
                //    .WithMany(p => p.Cities)
                //    .HasForeignKey(d => d.LastEditedBy)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Application_Cities_Application_People");

                entity.HasOne(d => d.StateProvince)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Application_Cities_StateProvinceID_Application_StateProvinces");
            });
        }
    }
}
