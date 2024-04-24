using System;
using System.Collections.Generic;
using Altium_Component_Creator.Models;
using Microsoft.EntityFrameworkCore;

namespace Altium_Component_Creator.Data;

public partial class TestAltiumDBContext : DbContext
{
    public TestAltiumDBContext()
    {
    }

    public TestAltiumDBContext(DbContextOptions<TestAltiumDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CapacitorTable> CapacitorTables { get; set; }

    public virtual DbSet<ResistorTable> ResistorTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-42N0E2T\\SQLEXPRESS;Initial Catalog=Test_Altium_dB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CapacitorTable>(entity =>
        {
            entity.HasKey(e => e.PartNumber).HasName("PK__Capacito__025D30D8AF0B6273");

            entity.ToTable("CapacitorTable");

            entity.Property(e => e.PartNumber)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ComponentLink1Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.ComponentLink1Url)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("ComponentLink1URL");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Dialectric)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FoorprintRef)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Foorprint Ref");
            entity.Property(e => e.FootprintPath)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Footprint Path");
            entity.Property(e => e.LibraryPath)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Library Path");
            entity.Property(e => e.LibraryRef)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Library Ref");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Package)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tolerance)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ResistorTable>(entity =>
        {
            entity.HasKey(e => e.PartNumber).HasName("PK__Resistor__025D30D87260D141");

            entity.ToTable("ResistorTable");

            entity.Property(e => e.PartNumber)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ComponentLink1Description)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.ComponentLink1Url)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("ComponentLink1URL");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FoorprintRef)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Foorprint Ref");
            entity.Property(e => e.FootprintPath)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Footprint Path");
            entity.Property(e => e.LibraryPath)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Library Path");
            entity.Property(e => e.LibraryRef)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Library Ref");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Package)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
