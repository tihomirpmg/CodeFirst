namespace HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using HospitalDatabase.Models;

    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions options):base(options)
        {

        }

        public HospitalContext()
        {

        }

        DbSet<Patient> Patients { get; set; }

        DbSet<Visitation> Visitations { get; set; }

        DbSet<Diagnose> Diagnoses { get; set; }

        DbSet<Medicament> Medicaments { get; set; }

        DbSet<PatientMedicament> PatientMedicaments { get; set; }

        DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-IUBR93H\TIHOMIR1705;Initial Catalog=HospitalDB;Integrated Security=true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patients");

                entity.HasKey(e => e.PatientId);

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(true);

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Visitation>(entity =>
            {
                entity.ToTable("Visitations");

                entity.HasKey(e => e.VisitationId);

                entity.Property(e => e.VisitationId).HasColumnName("VisitationID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(250)
                    .IsUnicode(true);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Visitations_Patients");
            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.ToTable("Diagnoses");

                entity.HasKey(e => e.DiagnoseId);

                entity.Property(e => e.DiagnoseId).HasColumnName("DiagnoseID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.Comments)
                    .HasMaxLength(250)
                    .IsUnicode(true);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Diagnoses_Patients");
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.ToTable("Medicaments");

                entity.HasKey(e => e.MedicamentId);

                entity.Property(e => e.MedicamentId).HasColumnName("MedicamentID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.MedicamentId });

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PatientMedicament_Patients");

                entity.HasOne(e => e.Medicament)
                    .WithMany(m => m.Prescriptions)
                    .HasForeignKey(e => e.MedicamentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PatientMedicament_Medicaments");
            });
        }
    }
}
