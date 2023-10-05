using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Payzi.Model;

namespace Payzi.Context;

public partial class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Accion> Accions { get; set; }

    public virtual DbSet<Boletum> Boleta { get; set; }

    public virtual DbSet<Ciudad> Ciudads { get; set; }

    public virtual DbSet<Comuna> Comunas { get; set; }

    public virtual DbSet<CustomField> CustomFields { get; set; }

    public virtual DbSet<Error> Errors { get; set; }

    public virtual DbSet<ExtraDatum> ExtraData { get; set; }

    public virtual DbSet<FormaPago> FormaPagos { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Negocio> Negocios { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sexo> Sexos { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    public virtual DbSet<TransaccionSalidum> TransaccionSalida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accion>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Accion__06370DAD4E6042AC");

            entity.ToTable("Accion", "Membresia");

            entity.Property(e => e.Codigo).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MenuItem).WithMany(p => p.Accions)
                .HasForeignKey(d => d.MenuItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accion_MenuItem");
        });

        modelBuilder.Entity<Boletum>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Boleta__06370DAD973F0C49");

            entity.ToTable("Boleta", "Tesoreria");

            entity.Property(e => e.Codigo).ValueGeneratedNever();
        });

        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => new { e.Codigo, e.PaisCodigo, e.RegionCodigo }).HasName("PK_Ciudad_1");

            entity.ToTable("Ciudad");

            entity.Property(e => e.Codigo).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Region).WithMany(p => p.Ciudads)
                .HasForeignKey(d => new { d.RegionCodigo, d.PaisCodigo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ciudad_Region");
        });

        modelBuilder.Entity<Comuna>(entity =>
        {
            entity.HasKey(e => new { e.Codigo, e.PaisCodigo, e.RegionCodigo, e.CiudadCodigo });

            entity.ToTable("Comuna");

            entity.Property(e => e.Codigo).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudad).WithMany(p => p.Comunas)
                .HasForeignKey(d => new { d.CiudadCodigo, d.PaisCodigo, d.RegionCodigo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comuna_Ciudad");
        });

        modelBuilder.Entity<CustomField>(entity =>
        {
            entity.HasKey(e => e.IdCustomFields);

            entity.ToTable("CustomFields", "Tesoreria");

            entity.Property(e => e.IdCustomFields)
                .ValueGeneratedNever()
                .HasColumnName("idCustomFields");
            entity.Property(e => e.Name)
                .HasMaxLength(28)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Print).HasColumnName("print");
            entity.Property(e => e.Value)
                .HasMaxLength(28)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Error>(entity =>
        {
            entity.HasKey(e => e.ErrorCode);

            entity.ToTable("Error");

            entity.Property(e => e.ErrorCode)
                .ValueGeneratedNever()
                .HasColumnName("errorCode");
            entity.Property(e => e.ErrorCodeOnApp).HasColumnName("errorCodeOnApp");
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("errorMessage");
            entity.Property(e => e.ErrorMessageOnApp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("errorMessageOnApp");
        });

        modelBuilder.Entity<ExtraDatum>(entity =>
        {
            entity.ToTable("ExtraData", "Tesoreria");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ExemptAmount).HasColumnName("exemptAmount");
            entity.Property(e => e.NetAmount).HasColumnName("netAmount");
            entity.Property(e => e.SourceName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("sourceName");
            entity.Property(e => e.SourceVersion)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("sourceVersion");
            entity.Property(e => e.TaxIdnValidation)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("taxIdnValidation");

            entity.HasOne(d => d.CustomFields).WithMany(p => p.ExtraData)
                .HasForeignKey(d => d.CustomFieldsId)
                .HasConstraintName("FK_TransaccionDetalles_CustomFields");
        });

        modelBuilder.Entity<FormaPago>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("FormaPago", "Tesoreria");

            entity.Property(e => e.Codigo).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu", "Membresia");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.ToTable("MenuItem", "Membresia");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuItem_Menu");
        });

        modelBuilder.Entity<Negocio>(entity =>
        {
            entity.ToTable("Negocio");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rut)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Dueno).WithMany(p => p.Negocios)
                .HasForeignKey(d => d.DuenoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Negocio_Persona");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Negocio)
                .HasForeignKey<Negocio>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Negocio_Usuario");

            entity.HasOne(d => d.Comuna).WithMany(p => p.Negocios)
                .HasForeignKey(d => new { d.ComunaCodigo, d.PaisCodigo, d.RegionCodigo, d.CiudadCodigo })
                .HasConstraintName("FK_Negocio_Comuna");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.ToTable("Persona");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Direccion).HasColumnType("text");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombrePrimario)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.NombreSecundario)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones).HasColumnType("text");
            entity.Property(e => e.Rut)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.RutDigito)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.SexoCodigoNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.SexoCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona_Sexo");

            entity.HasOne(d => d.Comuna).WithMany(p => p.Personas)
                .HasForeignKey(d => new { d.ComunaCodigo, d.PaisCodigo, d.RegionCodigo, d.CiudadCodigo })
                .HasConstraintName("FK_Persona_Comuna");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => new { e.Codigo, e.PaisCodigo });

            entity.ToTable("Region");

            entity.Property(e => e.Codigo).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreOficial)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.PaisCodigoNavigation).WithMany(p => p.Regions)
                .HasForeignKey(d => d.PaisCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Region_Pais");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Rol");

            entity.Property(e => e.Codigo).ValueGeneratedNever();
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Menu).WithMany(p => p.Rols)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rol_Menu");
        });

        modelBuilder.Entity<Sexo>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Sexo");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion);

            entity.ToTable("Transaccion", "Tesoreria");

            entity.Property(e => e.IdTransaccion)
                .ValueGeneratedNever()
                .HasColumnName("idTransaccion");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Cashback).HasColumnName("cashback");
            entity.Property(e => e.DteType).HasColumnName("dteType");
            entity.Property(e => e.ExtraData).HasColumnName("extraData");
            entity.Property(e => e.InstallmentsQuantity).HasColumnName("installmentsQuantity");
            entity.Property(e => e.Method).HasColumnName("method");
            entity.Property(e => e.PrintVoucherOnApp).HasColumnName("printVoucherOnApp");
            entity.Property(e => e.Tip).HasColumnName("tip");

            entity.HasOne(d => d.AccionCodigoNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.AccionCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaccion_Accion");

            entity.HasOne(d => d.ExtraDataNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.ExtraData)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaccion_TransaccionDetalles");

            entity.HasOne(d => d.MethodNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.Method)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaccion_FormaPago");

            entity.HasOne(d => d.Voucher).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.VoucherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaccion_Voucher");
        });

        modelBuilder.Entity<TransaccionSalidum>(entity =>
        {
            entity.ToTable("TransaccionSalida", "Tesoreria");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ExtraData).HasColumnName("extraData");
            entity.Property(e => e.PrinterVoucherCommerce).HasColumnName("printerVoucherCommerce");
            entity.Property(e => e.SequenceNumber).HasColumnName("sequenceNumber");
            entity.Property(e => e.TransactionCashback).HasColumnName("transactionCashback");
            entity.Property(e => e.TransactionStatus).HasColumnName("transactionStatus");
            entity.Property(e => e.TransactionTip).HasColumnName("transactionTip");

            entity.HasOne(d => d.ExtraDataNavigation).WithMany(p => p.TransaccionSalida)
                .HasForeignKey(d => d.ExtraData)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransaccionSalida_TransaccionDetalles");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario", "Membresia");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Clave)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Creacion).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaIntentoFallido).HasColumnType("date");
            entity.Property(e => e.UltimoAcceso).HasColumnType("date");
            entity.Property(e => e.UltimoCambioPassword).HasColumnType("date");

            entity.HasOne(d => d.RolCodigoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.ToTable("Voucher", "Tesoreria");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaEmision).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(30, 0)");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NumeroTransaccion)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.MetodoPagoCodigoNavigation).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.MetodoPagoCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Voucher_FormaPago");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
