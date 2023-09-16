using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Payzi.MySQL.Model;

public partial class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public virtual DbSet<DboCiudad> DboCiudads { get; set; }

    public virtual DbSet<DboComuna> DboComunas { get; set; }

    public virtual DbSet<DboPai> DboPais { get; set; }

    public virtual DbSet<DboPersona> DboPersonas { get; set; }

    public virtual DbSet<DboRegion> DboRegions { get; set; }

    public virtual DbSet<MembresiaTipousuario> MembresiaTipousuarios { get; set; }

    public virtual DbSet<MembresiaUsuario> MembresiaUsuarios { get; set; }

    public virtual DbSet<TesoreriaCustomfield> TesoreriaCustomfields { get; set; }

    public virtual DbSet<TesoreriaFormapago> TesoreriaFormapagos { get; set; }

    public virtual DbSet<TesoreriaTransaccion> TesoreriaTransaccions { get; set; }

    public virtual DbSet<TesoreriaTransacciondetalle> TesoreriaTransacciondetalles { get; set; }

    public virtual DbSet<TesoreriaVoucher> TesoreriaVouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DboCiudad>(entity =>
        {
            entity.HasKey(e => new { e.Codigo, e.RegionCodigo, e.PaisCodigo }).HasName("PRIMARY");

            entity.ToTable("dbo.ciudad");

            entity.HasIndex(e => new { e.RegionCodigo, e.PaisCodigo }, "ciudad_idx");

            entity.Property(e => e.NombreCiudad).HasMaxLength(45);

            entity.HasOne(d => d.DboRegion).WithMany(p => p.DboCiudads)
                .HasForeignKey(d => new { d.RegionCodigo, d.PaisCodigo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegionCodigo_Ciudad");
        });

        modelBuilder.Entity<DboComuna>(entity =>
        {
            entity.HasKey(e => new { e.Codigo, e.CiudadCodigo, e.RegionCodigo, e.PaisCodigo }).HasName("PRIMARY");

            entity.ToTable("dbo.comuna");

            entity.HasIndex(e => new { e.CiudadCodigo, e.RegionCodigo, e.PaisCodigo }, "RegionCodigo_idx");

            entity.Property(e => e.Nombre).HasMaxLength(45);

            entity.HasOne(d => d.DboCiudad).WithMany(p => p.DboComunas)
                .HasForeignKey(d => new { d.CiudadCodigo, d.RegionCodigo, d.PaisCodigo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CiudadCodigo_Comuna");
        });

        modelBuilder.Entity<DboPai>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("dbo.pais");

            entity.Property(e => e.NombrePais).HasMaxLength(45);
        });

        modelBuilder.Entity<DboPersona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("dbo.persona");

            entity.HasIndex(e => new { e.ComunaCodigo, e.CiudadCodigo, e.RegionCodigo, e.PaisCodigo }, "FK_ComunaCodigo_Persona");

            entity.HasIndex(e => e.Id, "idx_Id");

            entity.Property(e => e.ApellidoMaterno).HasMaxLength(25);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(25);
            entity.Property(e => e.Direccion).HasColumnType("text");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.NombreCompleto).HasMaxLength(200);
            entity.Property(e => e.NombrePrimario).HasMaxLength(45);
            entity.Property(e => e.NombreSecundario).HasMaxLength(45);
            entity.Property(e => e.Observaciones).HasColumnType("text");
            entity.Property(e => e.Rut).HasMaxLength(30);
            entity.Property(e => e.RutDigito)
                .HasMaxLength(1)
                .IsFixedLength();

            entity.HasOne(d => d.DboComuna).WithMany(p => p.DboPersonas)
                .HasForeignKey(d => new { d.ComunaCodigo, d.CiudadCodigo, d.RegionCodigo, d.PaisCodigo })
                .HasConstraintName("FK_Codigos_Persona");
        });

        modelBuilder.Entity<DboRegion>(entity =>
        {
            entity.HasKey(e => new { e.Codigo, e.PaisCodigo }).HasName("PRIMARY");

            entity.ToTable("dbo.region");

            entity.HasIndex(e => e.PaisCodigo, "CodigoPais_idx");

            entity.Property(e => e.NombreOficial).HasMaxLength(45);
            entity.Property(e => e.NombreSigla).HasMaxLength(45);

            entity.HasOne(d => d.PaisCodigoNavigation).WithMany(p => p.DboRegions)
                .HasForeignKey(d => d.PaisCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaisCodigo_Region");
        });

        modelBuilder.Entity<MembresiaTipousuario>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("membresia.tipousuario", tb => tb.HasComment("Tipo de cuenta que podrá elegir el cliente a pagar? tabla en espera de respuesta."));

            entity.Property(e => e.Nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<MembresiaUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("membresia.usuario", tb => tb.HasComment("Tabla usuarios registrados"));

            entity.HasIndex(e => e.TipoUsuarioCodigo, "FK_TipoUsuarioCodigo_TipoUsuario_idx");

            entity.Property(e => e.Clave).HasMaxLength(256);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(45);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.MembresiaUsuario)
                .HasForeignKey<MembresiaUsuario>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonaId");

            entity.HasOne(d => d.TipoUsuarioCodigoNavigation).WithMany(p => p.MembresiaUsuarios)
                .HasForeignKey(d => d.TipoUsuarioCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TipoUsuarioCodigo_TipoUsuario");
        });

        modelBuilder.Entity<TesoreriaCustomfield>(entity =>
        {
            entity.HasKey(e => e.IdCustomFields).HasName("PRIMARY");

            entity.ToTable("tesoreria.customfields");

            entity.Property(e => e.IdCustomFields).HasColumnName("idCustomFields");
            entity.Property(e => e.Name)
                .HasMaxLength(28)
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(28)
                .HasColumnName("value");
        });

        modelBuilder.Entity<TesoreriaFormapago>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("tesoreria.formapago", tb => tb.HasComment("Tabla de formas de pago que realizará el cliente."));

            entity.Property(e => e.Nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<TesoreriaTransaccion>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PRIMARY");

            entity.ToTable("tesoreria.transaccion");

            entity.HasIndex(e => e.Method, "FK_Codigo_FormaPago_idx");

            entity.HasIndex(e => e.VoucherId, "FK_VoucherId_Voucher_idx");

            entity.HasIndex(e => e.TransaccionDetallesId, "idTransaccionDetalles_idx");

            entity.Property(e => e.IdTransaccion).HasColumnName("idTransaccion");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Cashback).HasColumnName("cashback");
            entity.Property(e => e.DteType).HasColumnName("dteType");
            entity.Property(e => e.InstallmentsQuantity).HasColumnName("installmentsQuantity");
            entity.Property(e => e.Method).HasColumnName("method");
            entity.Property(e => e.PrintVoucherOnApp).HasColumnName("printVoucherOnApp");
            entity.Property(e => e.Tip).HasColumnName("tip");

            entity.HasOne(d => d.MethodNavigation).WithMany(p => p.TesoreriaTransaccions)
                .HasForeignKey(d => d.Method)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Codigo_FormaPago");

            entity.HasOne(d => d.TransaccionDetalles).WithMany(p => p.TesoreriaTransaccions)
                .HasForeignKey(d => d.TransaccionDetallesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Id_TransaccionDetalles");

            entity.HasOne(d => d.Voucher).WithMany(p => p.TesoreriaTransaccions)
                .HasForeignKey(d => d.VoucherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VoucherId_Voucher");
        });

        modelBuilder.Entity<TesoreriaTransacciondetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tesoreria.transacciondetalles");

            entity.HasIndex(e => e.CustomFieldsId, "idCustomFields_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExemptAmount).HasColumnName("exemptAmount");
            entity.Property(e => e.NetAmount)
                .HasPrecision(4)
                .HasColumnName("netAmount");
            entity.Property(e => e.Print).HasColumnName("print");
            entity.Property(e => e.SourceName)
                .HasMaxLength(60)
                .HasColumnName("sourceName");
            entity.Property(e => e.SourceVersion)
                .HasMaxLength(40)
                .HasColumnName("sourceVersion");
            entity.Property(e => e.TaxIdnValidation)
                .HasMaxLength(30)
                .HasColumnName("taxIdnValidation");

            entity.HasOne(d => d.CustomFields).WithMany(p => p.TesoreriaTransacciondetalles)
                .HasForeignKey(d => d.CustomFieldsId)
                .HasConstraintName("idCustomFields");
        });

        modelBuilder.Entity<TesoreriaVoucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tesoreria.voucher", tb => tb.HasComment("Tabla de Voucher generado para el cliente después de una compra."));

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaEmision).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasPrecision(30);
            entity.Property(e => e.NombreCliente).HasMaxLength(45);
            entity.Property(e => e.NumeroDocumento).HasMaxLength(30);
            entity.Property(e => e.NumeroTransaccion).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
