using Microsoft.EntityFrameworkCore;

namespace Models 
{
    public class EvidencijaContext : DbContext 
    {
        public EvidencijaContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Igrac> Igraci { get; set; }
        public DbSet<Turnir> Turniri { get; set; }
        public DbSet<Ucesnik> Ucesnici { get; set; }
        public DbSet<Partija> Partije { get; set; }
        public DbSet<Drzava> Drzave { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Igrac>()
                        .HasMany<Partija>(p => p.PBeli)
                        .WithOne(p => p.Beli);
            modelBuilder.Entity<Igrac>()
                        .HasMany<Partija>(p => p.PCrni)
                        .WithOne(p => p.Crni);
            modelBuilder.Entity<Partija>()
                        .HasOne<Turnir>(p => p.Turnir)
                        .WithMany(t => t.Partije)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.SetNull);
            // modelBuilder.Entity<Partija>()
            //             .HasOne<Igrac>(p => p.Beli)
            //             .WithMany(t => t.PBeli)
            //             .IsRequired(false)
            //             .OnDelete(DeleteBehavior.SetNull);
            // modelBuilder.Entity<Partija>()
            //             .HasOne<Igrac>(p => p.Crni)
            //             .WithMany(t => t.PCrni)
            //             .IsRequired(false)
            //             .OnDelete(DeleteBehavior.SetNull);
        }
    }
}