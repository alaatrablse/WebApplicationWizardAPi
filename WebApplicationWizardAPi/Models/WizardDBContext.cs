using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplicationWizardAPi
{
    public partial class WizardDBContext : DbContext
    {
        public WizardDBContext()
        {
        }

        public WizardDBContext(DbContextOptions<WizardDBContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Page> Pages { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Wizard> Wizards { get; set; } = null!;
        public virtual DbSet<WizardDatum> WizardData { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=WizardDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.Answer1)
                    .HasColumnType("text")
                    .HasColumnName("answer");

                entity.Property(e => e.UserEmail).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.WizardDatumId).HasColumnName("WizaedDataID");

               /* entity.HasOne(d => d.WizaedData)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.WizaedDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Answers__WizaedD__3C34F16F");*/
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.WizardId).HasColumnName("WizardID");

                entity.Property(e => e.WizardType).HasMaxLength(50);

                /*entity.HasOne(d => d.Wizard)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.WizardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pages__WizardID__66603565");*/
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("NAME");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Wizard>(entity =>
            {
                entity.Property(e => e.Titel).HasMaxLength(50);

                /*entity.HasOne(d => d.User)
                    .WithMany(p => p.Wizards)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Wizards__UserId__3F115E1A");*/
            });

            modelBuilder.Entity<WizardDatum>(entity =>
            {
                entity.Property(e => e.PageId).HasColumnName("PageID");

                entity.Property(e => e.WizardIndex).HasMaxLength(50);

                /*entity.HasOne(d => d.Page)
                    .WithMany(p => p.WizardData)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WizardDat__PageI__32AB8735");*/
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
