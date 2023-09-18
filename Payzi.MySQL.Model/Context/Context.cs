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

    public virtual DbSet<Accion> Accions { get; set; }

    public virtual DbSet<Ciudad> Ciudads { get; set; }

    public virtual DbSet<Comuna> Comunas { get; set; }

    public virtual DbSet<Customfield> Customfields { get; set; }

    public virtual DbSet<Error> Errors { get; set; }

    public virtual DbSet<Formapago> Formapagos { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Menuitem> Menuitems { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Teclado> Teclados { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    public virtual DbSet<Transacciondetalle> Transacciondetalles { get; set; }

    public virtual DbSet<Transaccionsalidum> Transaccionsalida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accion>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("accion");

            entity.HasIndex(e => e.MenuItemId, "FK_MenuItemId_MenuItem_idx");

            entity.Property(e => e.Nombre).HasMaxLength(45);

            entity.HasOne(d => d.MenuItem).WithMany(p => p.Accions)
                .HasForeignKey(d => d.MenuItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuItemId_MenuItem");
        });

        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => new { e.Codigo, e.RegionCodigo, e.PaisCodigo }).HasName("PRIMARY");

            entity.ToTable("ciudad");

            entity.HasIndex(e => new { e.RegionCodigo, e.PaisCodigo }, "ciudad_idx");

            entity.Property(e => e.NombreCiudad).HasMaxLength(45);

            entity.HasOne(d => d.Region).WithMany(p => p.Ciudads)
                .HasForeignKey(d => new { d.RegionCodigo, d.PaisCodigo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegionCodigo_Ciudad");
        });

        modelBuilder.Entity<Comuna>(entity =>
        {
            entity.HasKey(e => new { e.Codigo, e.CiudadCodigo, e.RegionCodigo, e.PaisCodigo }).HasName("PRIMARY");

            entity.ToTable("comuna");

            entity.HasIndex(e => new { e.CiudadCodigo, e.RegionCodigo, e.PaisCodigo }, "RegionCodigo_idx");

            entity.Property(e => e.Nombre).HasMaxLength(45);

            entity.HasOne(d => d.Ciudad).WithMany(p => p.Comunas)
                .HasForeignKey(d => new { d.CiudadCodigo, d.RegionCodigo, d.PaisCodigo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CiudadCodigo_Comuna");
        });

        modelBuilder.Entity<Customfield>(entity =>
        {
            entity.HasKey(e => e.IdCustomFields).HasName("PRIMARY");

            entity.ToTable("customfields", tb => tb.HasComment("CustomFields es la información adicional de la transacción, opcional."));

            entity.Property(e => e.IdCustomFields).HasColumnName("idCustomFields");
            entity.Property(e => e.Name)
                .HasMaxLength(28)
                .HasColumnName("name");
            entity.Property(e => e.Value)
                .HasMaxLength(28)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Error>(entity =>
        {
            entity.HasKey(e => e.ErrorCode).HasName("PRIMARY");

            entity.ToTable("error");

            entity.Property(e => e.ErrorCode).HasColumnName("errorCode");
            entity.Property(e => e.ErrorCodeOnApp).HasColumnName("errorCodeOnApp");
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(100)
                .HasColumnName("errorMessage");
            entity.Property(e => e.ErrorMessageOnApp)
                .HasMaxLength(100)
                .HasColumnName("errorMessageOnApp");
        });

        modelBuilder.Entity<Formapago>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("formapago", tb => tb.HasComment("Tabla de formas de pago que realizará el cliente."));

            entity.Property(e => e.Nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("menu");

            entity.Property(e => e.Clave).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<Menuitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("menuitem", tb => tb.HasComment("Menuitem son todas los items que puede traer un tipo especificado de menu."));

            entity.HasIndex(e => e.MenuId, "FK_MenuId_Menu_idx");

            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.Titulo).HasMaxLength(45);

            entity.HasOne(d => d.Menu).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MenuId_Menu");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("pais");

            entity.Property(e => e.NombrePais).HasMaxLength(45);
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("persona");

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

            entity.HasOne(d => d.Comuna).WithMany(p => p.Personas)
                .HasForeignKey(d => new { d.ComunaCodigo, d.CiudadCodigo, d.RegionCodigo, d.PaisCodigo })
                .HasConstraintName("FK_Codigos_Persona");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => new { e.Codigo, e.PaisCodigo }).HasName("PRIMARY");

            entity.ToTable("region");

            entity.HasIndex(e => e.PaisCodigo, "CodigoPais_idx");

            entity.Property(e => e.NombreOficial).HasMaxLength(45);
            entity.Property(e => e.NombreSigla).HasMaxLength(45);

            entity.HasOne(d => d.PaisCodigoNavigation).WithMany(p => p.Regions)
                .HasForeignKey(d => d.PaisCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaisCodigo_Region");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PRIMARY");

            entity.ToTable("rol", tb => tb.HasComment("Tipo de cuenta que podrá elegir el cliente a pagar? tabla en espera de respuesta."));

            entity.HasIndex(e => e.MenuId, "FK_MenuAccion_Accion_idx");

            entity.Property(e => e.Clave).HasMaxLength(40);
            entity.Property(e => e.Nombre).HasMaxLength(70);

            entity.HasOne(d => d.Menu).WithMany(p => p.Rols)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuAccion_Accion");
        });

        modelBuilder.Entity<Teclado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("teclado");

            entity.Property(e => e.Sigla).HasMaxLength(10);
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PRIMARY");

            entity.ToTable("transaccion", tb => tb.HasComment("Transaccion es la boleta a facturar."));

            entity.HasIndex(e => e.TransaccionDetallesId, "FK_Id_TransaccionDetalles_idx");

            entity.HasIndex(e => e.Method, "FK_Method_FormaPago_idx");

            entity.HasIndex(e => e.VoucherId, "FK_VoucherId_Voucher_idx");

            entity.Property(e => e.IdTransaccion).HasColumnName("idTransaccion");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Cashback).HasColumnName("cashback");
            entity.Property(e => e.DteType).HasColumnName("dteType");
            entity.Property(e => e.InstallmentsQuantity).HasColumnName("installmentsQuantity");
            entity.Property(e => e.Method).HasColumnName("method");
            entity.Property(e => e.PrintVoucherOnApp).HasColumnName("printVoucherOnApp");
            entity.Property(e => e.Tip).HasColumnName("tip");

            entity.HasOne(d => d.MethodNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.Method)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Method_FormaPago");

            entity.HasOne(d => d.TransaccionDetalles).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.TransaccionDetallesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Id_TransaccionDetalles");

            entity.HasOne(d => d.Voucher).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.VoucherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VoucherId_Voucher");
        });

        modelBuilder.Entity<Transacciondetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("transacciondetalles", tb => tb.HasComment("Detalles de la transacción."));

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

            entity.HasOne(d => d.CustomFields).WithMany(p => p.Transacciondetalles)
                .HasForeignKey(d => d.CustomFieldsId)
                .HasConstraintName("idCustomFields");
        });

        modelBuilder.Entity<Transaccionsalidum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("transaccionsalida", tb => tb.HasComment("Parametros de salida después de una transacción."));

            entity.HasIndex(e => e.ExtraData, "test1_idx");

            entity.Property(e => e.ExtraData).HasColumnName("extraData");
            entity.Property(e => e.PrinterVoucherCommerce).HasColumnName("printerVoucherCommerce");
            entity.Property(e => e.SequenceNumber).HasColumnName("sequenceNumber");
            entity.Property(e => e.TransactionCashback).HasColumnName("transactionCashback");
            entity.Property(e => e.TransactionStatus).HasColumnName("transactionStatus");
            entity.Property(e => e.TransactionTip).HasColumnName("transactionTip");

            entity.HasOne(d => d.ExtraDataNavigation).WithMany(p => p.Transaccionsalida)
                .HasForeignKey(d => d.ExtraData)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_extraData_TransaccionDetalles");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario", tb => tb.HasComment("Tabla usuarios registrados"));

            entity.HasIndex(e => e.RolCodigo, "FK_TipoUsuarioCodigo_TipoUsuario_idx");

            entity.Property(e => e.Clave).HasMaxLength(256);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(45);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonaId");

            entity.HasOne(d => d.RolCodigoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TipoUsuarioCodigo_TipoUsuario");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("voucher", tb => tb.HasComment("Tabla de Voucher generado para el cliente después de una compra."));

            entity.HasIndex(e => e.UsuarioId, "FK_Id_Usuario_idx");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaEmision).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasPrecision(30);
            entity.Property(e => e.NombreCliente).HasMaxLength(45);
            entity.Property(e => e.NumeroDocumento).HasMaxLength(30);
            entity.Property(e => e.NumeroTransaccion).HasMaxLength(200);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Id_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
