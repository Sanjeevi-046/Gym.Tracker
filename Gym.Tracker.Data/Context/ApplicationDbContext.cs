using System;
using System.Collections.Generic;
using Gym.Tracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gym.Tracker.Data.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationPermission> ApplicationPermissions { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<RoleType> RoleTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC07D9BA28D6");

            entity.ToTable("ApplicationPermission");

            entity.HasIndex(e => e.PermissionCode, "UQ__Applicat__91FE5750CE46DB46").IsUnique();

            entity.Property(e => e.PermissionName).HasMaxLength(100);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RolePerm__3214EC079DE34C82");

            entity.ToTable("RolePermission");

            entity.HasOne(d => d.ApplicationPermission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.ApplicationPermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationPermissionId");

            entity.HasOne(d => d.RoleType).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleTypeId");
        });

        modelBuilder.Entity<RoleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleType__3214EC0755B10C1D");

            entity.ToTable("RoleType");

            entity.HasIndex(e => e.RoleName, "UQ__RoleType__8A2B61604C82CB0D").IsUnique();

            entity.HasIndex(e => e.RoleCode, "UQ__RoleType__D62CB59C628B5AF5").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0799E94DF3");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105348DE8892B").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLoginAt).HasColumnType("datetime");
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RestoredAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
