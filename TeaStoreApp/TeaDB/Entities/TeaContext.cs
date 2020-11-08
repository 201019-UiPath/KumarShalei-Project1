using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TeaDB.Entities
{
    public partial class TeaContext : DbContext
    {
        public TeaContext()
        {
        }

        public TeaContext(DbContextOptions<TeaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Orderitems> Orderitems { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PgStatStatements> PgStatStatements { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionString = configuration.GetConnectionString("TeaDB");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("btree_gin")
                .HasPostgresExtension("btree_gist")
                .HasPostgresExtension("citext")
                .HasPostgresExtension("cube")
                .HasPostgresExtension("dblink")
                .HasPostgresExtension("dict_int")
                .HasPostgresExtension("dict_xsyn")
                .HasPostgresExtension("earthdistance")
                .HasPostgresExtension("fuzzystrmatch")
                .HasPostgresExtension("hstore")
                .HasPostgresExtension("intarray")
                .HasPostgresExtension("ltree")
                .HasPostgresExtension("pg_stat_statements")
                .HasPostgresExtension("pg_trgm")
                .HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("pgrowlocks")
                .HasPostgresExtension("pgstattuple")
                .HasPostgresExtension("tablefunc")
                .HasPostgresExtension("unaccent")
                .HasPostgresExtension("uuid-ossp")
                .HasPostgresExtension("xml2");

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.Customerid)
                    .HasName("customers_pkey");

                entity.ToTable("customers");

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Customeremail)
                    .HasColumnName("customeremail")
                    .HasMaxLength(100);

                entity.Property(e => e.Customerfirstname)
                    .IsRequired()
                    .HasColumnName("customerfirstname")
                    .HasMaxLength(100);

                entity.Property(e => e.Customerlastname)
                    .IsRequired()
                    .HasColumnName("customerlastname")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("inventory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Locationid).HasColumnName("locationid");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.Locationid)
                    .HasConstraintName("inventory_locationid_fkey");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("inventory_productid_fkey");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.Locationid)
                    .HasName("locations_pkey");

                entity.ToTable("locations");

                entity.Property(e => e.Locationid).HasColumnName("locationid");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(20);

                entity.Property(e => e.Stateacronym)
                    .HasColumnName("stateacronym")
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<Orderitems>(entity =>
            {
                entity.ToTable("orderitems");

                entity.Property(e => e.Orderitemsid).HasColumnName("orderitemsid");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Totalprice)
                    .HasColumnName("totalprice")
                    .HasColumnType("numeric(10,2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.Orderid)
                    .HasConstraintName("orderitems_orderid_fkey");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("orderitems_productid_fkey");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("orders_pkey");

                entity.ToTable("orders");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Customerid).HasColumnName("customerid");

                entity.Property(e => e.Locationid).HasColumnName("locationid");

                entity.Property(e => e.Payed).HasColumnName("payed");

                entity.Property(e => e.Totalprice)
                    .HasColumnName("totalprice")
                    .HasColumnType("numeric(10,2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("orders_customerid_fkey");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Locationid)
                    .HasConstraintName("orders_locationid_fkey");
            });

            modelBuilder.Entity<PgStatStatements>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pg_stat_statements");

                entity.Property(e => e.BlkReadTime).HasColumnName("blk_read_time");

                entity.Property(e => e.BlkWriteTime).HasColumnName("blk_write_time");

                entity.Property(e => e.Calls).HasColumnName("calls");

                entity.Property(e => e.Dbid)
                    .HasColumnName("dbid")
                    .HasColumnType("oid");

                entity.Property(e => e.LocalBlksDirtied).HasColumnName("local_blks_dirtied");

                entity.Property(e => e.LocalBlksHit).HasColumnName("local_blks_hit");

                entity.Property(e => e.LocalBlksRead).HasColumnName("local_blks_read");

                entity.Property(e => e.LocalBlksWritten).HasColumnName("local_blks_written");

                entity.Property(e => e.MaxTime).HasColumnName("max_time");

                entity.Property(e => e.MeanTime).HasColumnName("mean_time");

                entity.Property(e => e.MinTime).HasColumnName("min_time");

                entity.Property(e => e.Query).HasColumnName("query");

                entity.Property(e => e.Queryid).HasColumnName("queryid");

                entity.Property(e => e.Rows).HasColumnName("rows");

                entity.Property(e => e.SharedBlksDirtied).HasColumnName("shared_blks_dirtied");

                entity.Property(e => e.SharedBlksHit).HasColumnName("shared_blks_hit");

                entity.Property(e => e.SharedBlksRead).HasColumnName("shared_blks_read");

                entity.Property(e => e.SharedBlksWritten).HasColumnName("shared_blks_written");

                entity.Property(e => e.StddevTime).HasColumnName("stddev_time");

                entity.Property(e => e.TempBlksRead).HasColumnName("temp_blks_read");

                entity.Property(e => e.TempBlksWritten).HasColumnName("temp_blks_written");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("oid");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.Productid)
                    .HasName("products_pkey");

                entity.ToTable("products");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(250);

                entity.Property(e => e.Numberofteabags).HasColumnName("numberofteabags");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.Productname)
                    .IsRequired()
                    .HasColumnName("productname")
                    .HasMaxLength(20);
            });

            

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
