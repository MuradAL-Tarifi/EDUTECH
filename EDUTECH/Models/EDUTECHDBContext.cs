using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EDUTECH.Models
{
    public partial class EDUTECHDBContext : DbContext
    {
        public EDUTECHDBContext()
        {
        }

        public EDUTECHDBContext(DbContextOptions<EDUTECHDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<College> Colleges { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Requirement> Requirements { get; set; } = null!;
        public virtual DbSet<ScientificDegree> ScientificDegrees { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Specialty> Specialties { get; set; } = null!;
        public virtual DbSet<University> Universities { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MURADAWAD;Database=EDUTECHDB;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_Book_University");
            });

            modelBuilder.Entity<College>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Colleges)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_Colleges_University");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.Cost).HasMaxLength(250);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.OrganizerEmail).HasMaxLength(250);

                entity.Property(e => e.OrganizerPhone).HasMaxLength(250);

                entity.Property(e => e.Output).HasMaxLength(250);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Topic).HasMaxLength(250);

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_EVENT_University");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Requirement>(entity =>
            {
                entity.HasOne(d => d.University)
                    .WithMany(p => p.Requirements)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_Requirements_University");
            });

            modelBuilder.Entity<ScientificDegree>(entity =>
            {
                entity.ToTable("ScientificDegree");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("Section");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.College)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.CollegeId)
                    .HasConstraintName("FK_Section_ScientificDegree");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.ToTable("Specialty");

                entity.HasOne(d => d.ScientificDegree)
                    .WithMany(p => p.Specialties)
                    .HasForeignKey(d => d.ScientificDegreeId)
                    .HasConstraintName("FK_Specialty_ScientificDegree");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Specialties)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_Specialty_Section");
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.ToTable("University");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Universities)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_University_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
