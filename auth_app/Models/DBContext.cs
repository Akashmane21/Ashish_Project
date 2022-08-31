using Microsoft.EntityFrameworkCore;

namespace auth_app.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Auth> all_users { get; set; }
        public virtual DbSet<Login> all_login { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Auth>(entity =>
            {
                entity.Property(e => e.UserName)
                      .HasMaxLength(50)
                      .IsUnicode(false);
                entity.Property(e => e.Password)
                     .HasMaxLength(50)
                     .IsUnicode(false);
                entity.Property(e => e.Email)
                     .HasMaxLength(50)
                     .IsUnicode(false);
                entity.Property(e => e.location)
                     .HasMaxLength(50)
                     .IsUnicode(false);
                entity.Property(e => e.language)
                     .HasMaxLength(50)
                     .IsUnicode(false);
                entity.Property(e => e.userpin)
                     .HasMaxLength(50)
                     .IsUnicode(false);
                entity.Property(e => e.Role)
                     .HasMaxLength(50)
                     .IsUnicode(false);

            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.UserName)
                      .HasMaxLength(50)
                      .IsUnicode(false);
                entity.Property(e => e.Password)
                     .HasMaxLength(50)
                     .IsUnicode(false);

            });

            OnModelCreatingPartial(modelBuilder);
        }

       

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }



}
