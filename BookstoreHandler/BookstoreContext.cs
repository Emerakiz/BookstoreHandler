using BookstoreHandler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BookstoreHandler;

public partial class BookstoreContext : DbContext
{
    internal object Böcker;

    public BookstoreContext()
    {
    }

    public BookstoreContext(DbContextOptions<BookstoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Butiker> Butikers { get; set; }

    public virtual DbSet<Böcker> Böckers { get; set; }

    public virtual DbSet<Författare> Författares { get; set; }

    public virtual DbSet<Förlag> Förlags { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Kunder> Kunders { get; set; }

    public virtual DbSet<LagerSaldo> LagerSaldos { get; set; }

    public virtual DbSet<Orderrad> Orderrads { get; set; }

    public virtual DbSet<Ordrar> Ordrars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Butiker>(entity =>
        {
            entity.HasKey(e => e.ButikId).HasName("PK__Butiker__B5D66BFA6082CCD2");

            entity.ToTable("Butiker");

            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.ButikNamn)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Webadress)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Böcker>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Böcker__3BF79E03FD9B87B3");

            entity.ToTable("Böcker");

            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN");
            entity.Property(e => e.FörfattareId).HasColumnName("FörfattareID");
            entity.Property(e => e.FörlagId).HasColumnName("FörlagID");
            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.Pris).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Språk)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Titel)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Författare).WithMany(p => p.Böckers)
                .HasForeignKey(d => d.FörfattareId)
                .HasConstraintName("FK__Böcker__Författa__3D5E1FD2");

            entity.HasOne(d => d.Förlag).WithMany(p => p.Böckers)
                .HasForeignKey(d => d.FörlagId)
                .HasConstraintName("FK__Böcker__FörlagID__3F466844");

            entity.HasOne(d => d.Genre).WithMany(p => p.Böckers)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Böcker__GenreID__3E52440B");
        });

        modelBuilder.Entity<Författare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Författa__3214EC27B54BF727");

            entity.ToTable("Författare");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Efternamn)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Förnamn)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Förlag>(entity =>
        {
            entity.HasKey(e => e.FörlagId).HasName("PK__Förlag__DE6A852CFE4D5B9D");

            entity.ToTable("Förlag");

            entity.Property(e => e.FörlagId).HasColumnName("FörlagID");
            entity.Property(e => e.Namn)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genre__0385055EA97D4CB5");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.Namn)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Kunder>(entity =>
        {
            entity.HasKey(e => e.KundId).HasName("PK__Kunder__F2B5DEAC3B7A17C4");

            entity.ToTable("Kunder");

            entity.Property(e => e.KundId).HasColumnName("KundID");
            entity.Property(e => e.Efternamn)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Förnamn)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TelefonNummer)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LagerSaldo>(entity =>
        {
            entity.HasKey(e => new { e.ButikId, e.Isbn }).HasName("PK__LagerSal__9669121AD958448B");

            entity.ToTable("LagerSaldo");

            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN");

            entity.HasOne(d => d.Butik).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.ButikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LagerSald__Butik__440B1D61");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LagerSald__ISBN1__44FF419A");
        });

        modelBuilder.Entity<Orderrad>(entity =>
        {
            entity.HasKey(e => e.OrderradId).HasName("PK__Orderrad__29F3F6851BB4042D");

            entity.ToTable("Orderrad");

            entity.Property(e => e.OrderradId).HasColumnName("OrderradID");
            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN13");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Pris).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Isbn13Navigation).WithMany(p => p.Orderrads)
                .HasForeignKey(d => d.Isbn13)
                .HasConstraintName("FK__Orderrad__ISBN13__4E88ABD4");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderrads)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Orderrad__OrderI__4D94879B");
        });

        modelBuilder.Entity<Ordrar>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Ordrar__C3905BAF8AEA21FF");

            entity.ToTable("Ordrar");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.KundId).HasColumnName("KundID");
            entity.Property(e => e.TotalBelopp).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Butik).WithMany(p => p.Ordrars)
                .HasForeignKey(d => d.ButikId)
                .HasConstraintName("FK__Ordrar__ButikID__4AB81AF0");

            entity.HasOne(d => d.Kund).WithMany(p => p.Ordrars)
                .HasForeignKey(d => d.KundId)
                .HasConstraintName("FK__Ordrar__KundID__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
