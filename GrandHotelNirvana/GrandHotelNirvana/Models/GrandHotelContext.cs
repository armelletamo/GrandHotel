﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GrandHotelNirvana.Models
{
    public partial class GrandHotelContext : DbContext
    {
        public GrandHotelContext()
        {
        }

        public GrandHotelContext(DbContextOptions<GrandHotelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adresse> Adresse { get; set; }
        public virtual DbSet<Chambre> Chambre { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<Facture> Facture { get; set; }
        public virtual DbSet<LigneFacture> LigneFacture { get; set; }
        public virtual DbSet<ModePaiement> ModePaiement { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Tarif> Tarif { get; set; }
        public virtual DbSet<TarifChambre> TarifChambre { get; set; }
        public virtual DbSet<Telephone> Telephone { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GrandHotel;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.Property(e => e.IdClient).ValueGeneratedNever();

                entity.Property(e => e.CodePostal)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Complement).HasMaxLength(40);

                entity.Property(e => e.Rue)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Ville)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.IdClientNavigation)
                    .WithOne(p => p.Adresse)
                    .HasForeignKey<Adresse>(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Adresse_Client");
            });

            modelBuilder.Entity<Chambre>(entity =>
            {
                entity.HasKey(e => e.Numero);

                entity.Property(e => e.Numero).ValueGeneratedNever();

                entity.Property(e => e.Douche)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Wc)
                    .IsRequired()
                    .HasColumnName("WC")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.UtilisateurId);

                entity.Property(e => e.Societe).HasMaxLength(100);

                entity.HasOne(d => d.Utilisateur)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.UtilisateurId);
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasKey(e => e.Adresse);

                entity.HasIndex(e => e.IdClient);

                entity.Property(e => e.Adresse)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Email)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Email_Client");
            });

            modelBuilder.Entity<Facture>(entity =>
            {
                entity.HasIndex(e => e.CodeModePaiement);

                entity.HasIndex(e => e.IdClient);

                entity.Property(e => e.CodeModePaiement)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DateFacture).HasColumnType("date");

                entity.Property(e => e.DatePaiement).HasColumnType("date");

                entity.HasOne(d => d.CodeModePaiementNavigation)
                    .WithMany(p => p.Facture)
                    .HasForeignKey(d => d.CodeModePaiement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Facture_Paiement");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Facture)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Facture_Client");
            });

            modelBuilder.Entity<LigneFacture>(entity =>
            {
                entity.HasKey(e => new { e.IdFacture, e.NumLigne });

                entity.Property(e => e.MontantHt)
                    .HasColumnName("MontantHT")
                    .HasColumnType("decimal(12, 3)");

                entity.Property(e => e.Quantite).HasDefaultValueSql("((1))");

                entity.Property(e => e.TauxReduction).HasColumnType("decimal(6, 3)");

                entity.Property(e => e.TauxTva)
                    .HasColumnName("TauxTVA")
                    .HasColumnType("decimal(6, 3)");

                entity.HasOne(d => d.IdFactureNavigation)
                    .WithMany(p => p.LigneFacture)
                    .HasForeignKey(d => d.IdFacture)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LigneFacture_Facture");
            });

            modelBuilder.Entity<ModePaiement>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Libelle)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasIndex(e => e.IdClient)
                    .HasName("IDX_ReservationClient_FK");

                entity.HasIndex(e => e.Jour);

                entity.Property(e => e.HeureArrivee).HasDefaultValueSql("((17))");

                entity.Property(e => e.Jour).HasColumnType("date");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Client");

                entity.HasOne(d => d.NumChambreNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.NumChambre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Chambre");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Nom).IsRequired();
            });

            modelBuilder.Entity<Tarif>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DateDebut).HasColumnType("date");

                entity.Property(e => e.Prix).HasColumnType("decimal(12, 3)");
            });

            modelBuilder.Entity<TarifChambre>(entity =>
            {
                entity.HasKey(e => new { e.NumChambre, e.CodeTarif });

                entity.HasIndex(e => e.CodeTarif);

                entity.Property(e => e.CodeTarif)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeTarifNavigation)
                    .WithMany(p => p.TarifChambre)
                    .HasForeignKey(d => d.CodeTarif)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TarifChambre_Tarif");

                entity.HasOne(d => d.NumChambreNavigation)
                    .WithMany(p => p.TarifChambre)
                    .HasForeignKey(d => d.NumChambre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TarifChambre_Chambre");
            });

            modelBuilder.Entity<Telephone>(entity =>
            {
                entity.HasIndex(e => e.IdClient);

                entity.Property(e => e.CodeType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Telephone)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Telephone_Client");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.Civilite).IsRequired();

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.MotDePasse).IsRequired();

                entity.Property(e => e.Nom).IsRequired();

                entity.Property(e => e.Prenom).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Utilisateur)
                    .HasForeignKey(d => d.RoleId);
            });
        }
    }
}
