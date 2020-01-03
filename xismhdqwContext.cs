using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ASPNetCoreAPI
{
    public partial class xismhdqwContext : DbContext
    {
        public xismhdqwContext()
        {
        }

        public xismhdqwContext(DbContextOptions<xismhdqwContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessoryId> AccessoryId { get; set; }
        public virtual DbSet<Auth> Auth { get; set; }
        public virtual DbSet<Boat> Boat { get; set; }
        public virtual DbSet<BoatType> BoatType { get; set; }
        public virtual DbSet<Colours> Colours { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Details> Details { get; set; }
        public virtual DbSet<DocumentName> DocumentName { get; set; }
        public virtual DbSet<Fit> Fit { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Partners> Partners { get; set; }
        public virtual DbSet<PgStatStatements> PgStatStatements { get; set; }
        public virtual DbSet<ProductionProcess> ProductionProcess { get; set; }
        public virtual DbSet<SalesPerson> SalesPerson { get; set; }
        public virtual DbSet<Vat> Vat { get; set; }
        public virtual DbSet<Wood> Wood { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=packy.db.elephantsql.com;Port=5432;Database=xismhdqw;Username=xismhdqw;Password=cenFKu8gn1_aVRPtm2FDysZVyaOIrBT9");
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
                .HasPostgresExtension("plv8")
                .HasPostgresExtension("tablefunc")
                .HasPostgresExtension("unaccent")
                .HasPostgresExtension("uuid-ossp")
                .HasPostgresExtension("xml2");

            modelBuilder.Entity<AccessoryId>(entity =>
            {
                entity.HasKey(e => e.AccessoryId1)
                    .HasName("Accessory_id_pkey");

                entity.ToTable("Accessory_id");

                entity.Property(e => e.AccessoryId1)
                    .HasColumnName("accessory_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccName)
                    .IsRequired()
                    .HasColumnName("accName");

                entity.Property(e => e.DescriptionOfAccessory)
                    .IsRequired()
                    .HasColumnName("descriptionOfAccessory");

                entity.Property(e => e.Inventory).HasColumnName("inventory");

                entity.Property(e => e.PartnerId).HasColumnName("partner_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Vat).HasColumnName("VAT");
            });

            modelBuilder.Entity<Auth>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("Auth_pkey");

                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<Boat>(entity =>
            {
                entity.Property(e => e.BoatId)
                    .HasColumnName("boat_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.Vat).HasColumnName("VAT");
            });

            modelBuilder.Entity<BoatType>(entity =>
            {
                entity.Property(e => e.BoatTypeId)
                    .HasColumnName("boat_type_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BoatType1)
                    .IsRequired()
                    .HasColumnName("boat_type");
            });

            modelBuilder.Entity<Colours>(entity =>
            {
                entity.HasKey(e => e.ColourId)
                    .HasName("Colour_pkey");

                entity.Property(e => e.ColourId)
                    .HasColumnName("colour_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Colour).IsRequired();
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.ContractId)
                    .HasColumnName("contract_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ContractTotalPrice).HasColumnName("contractTotalPrice");

                entity.Property(e => e.ContractTotalPriceIncVat).HasColumnName("contractTotalPrice_incVAT");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.DateDepositPayed).HasColumnName("dateDepositPayed");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductionProcess).HasColumnName("productionProcess");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("Customers_pkey");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName");

                entity.Property(e => e.IdDocumentName).HasColumnName("idDocumentName");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasColumnName("idNumber");

                entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasColumnName("secondName");
            });

            modelBuilder.Entity<Details>(entity =>
            {
                entity.HasKey(e => e.DetailId)
                    .HasName("Details_pkey");

                entity.Property(e => e.DetailId)
                    .HasColumnName("detail_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccessoryId).HasColumnName("accessory_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");
            });

            modelBuilder.Entity<DocumentName>(entity =>
            {
                entity.Property(e => e.DocumentNameId)
                    .HasColumnName("document_name_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DocumentName1)
                    .IsRequired()
                    .HasColumnName("document_name");
            });

            modelBuilder.Entity<Fit>(entity =>
            {
                entity.Property(e => e.FitId)
                    .HasColumnName("fit_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccessoryId).HasColumnName("accessory_id");

                entity.Property(e => e.BoatId).HasColumnName("boat_id");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("Orders_pkey");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BoatId).HasColumnName("boat_id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.DeliveryAddress)
                    .IsRequired()
                    .HasColumnName("deliveryAddress");

                entity.Property(e => e.SelesPersonId).HasColumnName("selesPerson_id");
            });

            modelBuilder.Entity<Partners>(entity =>
            {
                entity.HasKey(e => e.PartnerId)
                    .HasName("Partners_pkey");

                entity.Property(e => e.PartnerId)
                    .HasColumnName("partner_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
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

            modelBuilder.Entity<ProductionProcess>(entity =>
            {
                entity.Property(e => e.ProductionProcessId)
                    .HasColumnName("production_process_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductionProcess1)
                    .IsRequired()
                    .HasColumnName("production_process");
            });

            modelBuilder.Entity<SalesPerson>(entity =>
            {
                entity.Property(e => e.SalesPersonId)
                    .HasColumnName("sales_person_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName");

                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasColumnName("secondName");
            });

            modelBuilder.Entity<Vat>(entity =>
            {
                entity.Property(e => e.VatId)
                    .HasColumnName("vat_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Vat1).HasColumnName("VAT");
            });

            modelBuilder.Entity<Wood>(entity =>
            {
                entity.Property(e => e.WoodId)
                    .HasColumnName("wood_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Wood1)
                    .IsRequired()
                    .HasColumnName("wood");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
