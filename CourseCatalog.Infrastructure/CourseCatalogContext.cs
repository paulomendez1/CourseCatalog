using System;
using System.Collections.Generic;
using CourseCatalog.Domain.Contracts;
using CourseCatalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalog.Persistence;

public partial class CourseCatalogContext : DbContext, IUnitOfWork
{
    public CourseCatalogContext()
    {
    }

    public CourseCatalogContext(DbContextOptions<CourseCatalogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public async Task<bool> SaveEntitiesAsync(CancellationToken token = default)
    {
        await SaveChangesAsync();
        return true;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-GFOAKVJ;Database=CourseCatalog;Integrated Security = true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProfessorEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProfessorName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
