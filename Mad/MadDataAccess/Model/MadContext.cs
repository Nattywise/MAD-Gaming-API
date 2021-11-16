using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MadDataAccess.Model
{
    public partial class MadContext : DbContext
    {
        public MadContext()
        {
        }

        public MadContext(DbContextOptions<MadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Competition> Competition { get; set; }
        public virtual DbSet<CompetitionQuestion> CompetitionQuestion { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Question> Question { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Mad;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Competition)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Competition_To_Genre");
            });

            modelBuilder.Entity<CompetitionQuestion>(entity =>
            {
                entity.HasOne(d => d.Competition)
                    .WithMany(p => p.CompetitionQuestion)
                    .HasForeignKey(d => d.CompetitionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionQuestion_To_Competition");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.CompetitionQuestion)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetitionQuestion_To_Question");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(255);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Option1).HasMaxLength(50);

                entity.Property(e => e.Option2).HasMaxLength(50);

                entity.Property(e => e.Option3).HasMaxLength(50);

                entity.Property(e => e.Option4).HasMaxLength(50);

                entity.Property(e => e.QuestionPhrase).HasMaxLength(255);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_Question_To_Genre");
            });
        }
    }
}
