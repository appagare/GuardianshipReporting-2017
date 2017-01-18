namespace GFR.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GuardianshipDB : DbContext
    {
        public GuardianshipDB()
            : base("name=GuardianshipDB")
        {
        }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<DefaultCategory> DefaultCategories { get; set; }
        public virtual DbSet<DefaultSetting> DefaultSettings { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportDetail> ReportDetails { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserCategory> UserCategories { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.ReportDetails)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.UserCategories)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.UserSettings)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Wards)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DefaultCategory>()
                .Property(e => e.StateCode)
                .IsUnicode(false);

            modelBuilder.Entity<DefaultCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<DefaultSetting>()
                .Property(e => e.SystemGroup)
                .IsUnicode(false);

            modelBuilder.Entity<DefaultSetting>()
                .Property(e => e.SystemParameter)
                .IsUnicode(false);

            modelBuilder.Entity<DefaultSetting>()
                .Property(e => e.SystemValue)
                .IsUnicode(false);

            modelBuilder.Entity<DefaultSetting>()
                .Property(e => e.SystemFriendlyName)
                .IsUnicode(false);

            modelBuilder.Entity<DefaultSetting>()
                .Property(e => e.SystemDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Report>()
                .Property(e => e.StateCode)
                .IsUnicode(false);

            modelBuilder.Entity<Report>()
                .HasMany(e => e.ReportDetails)
                .WithRequired(e => e.Report)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReportDetail>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ReportDetail>()
                .Property(e => e.Value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<UserCategory>()
                .Property(e => e.StateCode)
                .IsUnicode(false);

            modelBuilder.Entity<UserCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<UserCategory>()
                .HasMany(e => e.ReportDetails)
                .WithRequired(e => e.UserCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserSetting>()
                .Property(e => e.Group)
                .IsUnicode(false);

            modelBuilder.Entity<UserSetting>()
                .Property(e => e.Setting)
                .IsUnicode(false);

            modelBuilder.Entity<UserSetting>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<UserSetting>()
                .Property(e => e.FriendlyName)
                .IsUnicode(false);

            modelBuilder.Entity<UserSetting>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Ward>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Ward>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<Ward>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Ward>()
                .Property(e => e.Suffix)
                .IsUnicode(false);

            modelBuilder.Entity<Ward>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.Ward)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ward>()
                .HasMany(e => e.ReportDetails)
                .WithRequired(e => e.Ward)
                .WillCascadeOnDelete(false);
        }
    }
}
