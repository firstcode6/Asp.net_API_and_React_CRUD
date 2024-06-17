using Microsoft.EntityFrameworkCore;
using ReactAppTestOil.Models;

namespace ReactAppTestOil.Data
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Company>()
            //    .HasMany(c => c.Wells) // Company can enroll in many Wells
            //    .WithMany(w => w.Companies) // Well can have many Companies
            //    .UsingEntity(j => j.ToTable("CompanyWell"));  //Explicitly set the join table name
            
            // Configure many-to-many relationship
            modelBuilder.Entity<CompanyWell>()
                    .HasKey(cw => new { cw.CompanyId, cw.WellId });

            modelBuilder.Entity<CompanyWell>()
                    .HasOne(cw => cw.Company)
                    .WithMany(c => c.CompanyWells)
                    .HasForeignKey(cw => cw.CompanyId);

            modelBuilder.Entity<CompanyWell>()
                    .HasOne(cw => cw.Well)
                    .WithMany(w => w.CompanyWells)
                    .HasForeignKey(cw => cw.WellId);


            // Configure one-to-many relationship
            modelBuilder.Entity<Well>()
                .HasOne(w => w.Telemetry)
                .WithMany(t => t.Wells)
                .HasForeignKey(w => w.TelemetryId);

            // Seed data
            modelBuilder.Entity<Telemetry>().HasData(
                new Telemetry { Id = 1, CustomDate = new DateTime(2023, 6, 1), Depth = 100.5f },
                new Telemetry { Id = 2, CustomDate = new DateTime(2023, 6, 2), Depth = 200.0f }
            );

            modelBuilder.Entity<Well>().HasData(
                new Well { Id = 1, Name = "Well 1", Active = true, TelemetryId = 1 },
                new Well { Id = 2, Name = "Well 2", Active = false, TelemetryId = 2 }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Company A" },
                new Company { Id = 2, Name = "Company B" }
            );

            modelBuilder.Entity<CompanyWell>().HasData(
                new CompanyWell { CompanyId = 1, WellId = 1 },
                new CompanyWell { CompanyId = 1, WellId = 2 },
                new CompanyWell { CompanyId = 2, WellId = 1 }
            );


        }
  

        /// <summary>
        /// Company entities from DB
        /// </summary>
        public DbSet<Company> Companies { get; set; }

        /// <summary>
        /// Telemetry entities from DB
        /// </summary>
        public DbSet<Telemetry> Telemetries { get; set; }

        /// <summary>
        /// Well entities from DB
        /// </summary>
        public DbSet<Well> Wells { get; set; }

        /// <summary>
        /// Many to many
        /// </summary>
        public DbSet<CompanyWell> CompanyWells { get; set; }
    }
}
