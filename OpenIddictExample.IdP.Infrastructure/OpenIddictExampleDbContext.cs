using Microsoft.EntityFrameworkCore;
using OpenIddictExample.IdP.Infrastructure.Models;
using System;

namespace OpenIddictExample.IdP.Infrastructure;

public class OpenIddictExampleDbContext<TKey> : DbContext where TKey : struct, IEquatable<TKey>
{
    public OpenIddictExampleDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        modelBuilder.Entity<UserEntity<TKey>>().ToTable("USERS");
        modelBuilder.Entity<UserEntity<TKey>>().HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity<TKey>>().Property(x => x.Id).HasColumnName("USER_ID");
        modelBuilder.Entity<UserEntity<TKey>>().Property(x => x.Name).HasColumnName("NAME");
        modelBuilder.Entity<UserEntity<TKey>>().Property(x => x.LastName).HasColumnName("LAST_NAME");
        modelBuilder.Entity<UserEntity<TKey>>().Property(x => x.Email).HasColumnName("EMAIL");
        modelBuilder.Entity<UserEntity<TKey>>().Property(x => x.Password).HasColumnName("PASSWORD");
        modelBuilder.Entity<UserEntity<TKey>>().Property(x => x.RegistrationDate).HasColumnName("REGISTERED_DATE");
        modelBuilder.Entity<UserEntity<TKey>>().Property(x => x.Guid).HasColumnName("GUID");

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<UserEntity<TKey>> Users { get; set; }
}
