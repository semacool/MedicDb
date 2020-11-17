namespace Medic.DataSource
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MedicModel : DbContext
    {
        public MedicModel()
            : base("name=MedicModel")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AnalizeBlood> AnalizeBloods { get; set; }
        public virtual DbSet<AnalizeMocha> AnalizeMochas { get; set; }
        public virtual DbSet<AnalizeShit> AnalizeShits { get; set; }
        public virtual DbSet<Analizi> Analizis { get; set; }
        public virtual DbSet<AnalizType> AnalizTypes { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Laborant> Laborants { get; set; }
        public virtual DbSet<Laboratory> Laboratories { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<BarcodeChar> BarcodeChars { get; set; }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Analizi>()
                .HasMany(e => e.AnalizeBloods)
                .WithRequired(e => e.Analizi)
                .HasForeignKey(e => e.IdAnalize);

            modelBuilder.Entity<Analizi>()
                .HasMany(e => e.AnalizeMochas)
                .WithRequired(e => e.Analizi)
                .HasForeignKey(e => e.IdAnalize);

            modelBuilder.Entity<Analizi>()
                .HasMany(e => e.AnalizeShits)
                .WithRequired(e => e.Analizi)
                .HasForeignKey(e => e.IdAnalize);

            modelBuilder.Entity<AnalizType>()
                .HasMany(e => e.Analizis)
                .WithRequired(e => e.AnalizType)
                .HasForeignKey(e => e.IdAnalizType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Analizis)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.IdClient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Laborant>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Laborant>()
                .HasMany(e => e.Analizis)
                .WithRequired(e => e.Laborant)
                .HasForeignKey(e => e.IdLaborant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Laboratory>()
                .HasMany(e => e.Laborants)
                .WithRequired(e => e.Laboratory)
                .HasForeignKey(e => e.IdLaboratory);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Admins)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Laborants)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser);

            modelBuilder.Entity<UserType>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserType)
                .HasForeignKey(e => e.IdType)
                .WillCascadeOnDelete(false);
        }
    }
}
