using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pr17.database;

public partial class WorkerZpContext : DbContext
{
    public WorkerZpContext()
    {
    }

    public WorkerZpContext(DbContextOptions<WorkerZpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MonthZp> MonthZps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAB17-13\\SQLEXPRESS; Database=worker zp; User=ИСП-31; Password=1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MonthZp>(entity =>
        {
            entity.HasKey(e => new { e.Surname, e.Name });

            entity.ToTable("Month_Zp");

            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.DadsName).HasMaxLength(50);
            entity.Property(e => e.Profession).HasMaxLength(50);
            entity.Property(e => e.Zeh).HasMaxLength(50);
            entity.Property(e => e.Zp)
                .HasColumnType("money")
                .HasColumnName("ZP");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
