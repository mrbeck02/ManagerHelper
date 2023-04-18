using ManagerHelper.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagerHelper.Data
{
    /// <summary>
    /// WARNING!!!: Guids are not supported in SQLite, they use a TEXT type.  When translating the Guid uses toUpper.  So if you insert
    /// entries into the database manually, be sure to make the IDs upper case.
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<IssueStatus> IssueStatuses { get; set; }
        public DbSet<Quarter> Quarters { get; set; }
        public DbSet<Sprint> Sprints { get; set; }

        public DbSet<Data.Entities.Entry> Entries { get; set; }

        public DbSet<Commitment> Commitments { get; set; }

        public DbSet<JiraIssue> JiraIssues { get; set; }

        public DbSet<JiraProject> JiraProjects { get; set; }

        public DbSet<Developer> Developers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<JiraSupportIssue> JiraSupportIssues { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=c:\\Temp\\mydb.db");
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IssueStatus>().HasData(
                    new IssueStatus {  Id = (int)IssueStatusEnum.inprogress, Name = "In Progress" },
                    new IssueStatus { Id = (int)IssueStatusEnum.open, Name = "Open" },
                    new IssueStatus { Id = (int)IssueStatusEnum.readyforrelease, Name = "Ready for Release" },
                    new IssueStatus { Id = (int)IssueStatusEnum.readyfortest, Name = "Ready for Test" },
                    new IssueStatus { Id = (int)IssueStatusEnum.intest, Name = "In Test" },
                    new IssueStatus { Id = (int)IssueStatusEnum.done, Name = "Done" },
                    new IssueStatus { Id = (int)IssueStatusEnum.todo, Name = "To Do" },
                    new IssueStatus { Id = (int)IssueStatusEnum.unknown, Name = "Unknown" }
                    );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = (int)ProductEnum.CARA, Name = "CARA" },
                new Product { Id = (int)ProductEnum.CM, Name = "Crisis Management" },
                new Product { Id = (int)ProductEnum.CRT, Name = "Critical Resource Tracker" },
                new Product { Id = (int)ProductEnum.EPMM, Name = "EPMM" },
                new Product { Id = (int)ProductEnum.PPS, Name = "OpenBeds" },
                new Product { Id = (int)ProductEnum.PFS, Name = "Treatment Connection" },
                new Product { Id = (int)ProductEnum.SMARTonFHIR, Name = "SMART on FHIR" },
                new Product { Id = (int)ProductEnum.AvailabilityAPI, Name = "Availability API" },
                new Product { Id = (int)ProductEnum.ReferralAPI, Name = "Referral API" },
                new Product { Id = (int)ProductEnum.Cognito, Name = "Cognito" },
                new Product { Id = (int)ProductEnum.Launcher, Name = "Launcher" },
                new Product { Id = (int)ProductEnum.Dynatrace, Name = "Dynatrace" },
                new Product { Id = (int)ProductEnum.Other, Name = "Other" },
                new Product { Id = (int)ProductEnum.CRAPI, Name = "Create Referral API" },
                new Product { Id = (int)ProductEnum.Research, Name = "Research" },
                new Product { Id = (int)ProductEnum.JI, Name = "JI" }
            );
        }
    }
}
