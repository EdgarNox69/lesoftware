using System;
using System.Collections.Generic;
using lesoftware.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace lesoftware.Server;

public partial class TiendaContext : DbContext
{
    public TiendaContext()
    {
    }

    public TiendaContext(DbContextOptions<TiendaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<ArticuloTiendum> ArticuloTienda { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClienteArticulo> ClienteArticulos { get; set; }

    public virtual DbSet<Tiendum> Tienda { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.Codigo, "AK_Table_1_codigo").IsUnique();

            entity.HasIndex(e => e.Id, "AK_Table_1_id").IsUnique();

            entity.HasIndex(e => e.Codigo, "UQ_Articulos_codigo").IsUnique();

            entity.HasIndex(e => e.Id, "UQ_Articulos_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Imagen)
                .HasColumnType("image")
                .HasColumnName("imagen");
            entity.Property(e => e.Precio)
                .HasColumnType("money")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<ArticuloTiendum>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("articulo_tienda");

            entity.HasIndex(e => e.Id, "AK_Table_1_id").IsUnique();

            entity.HasIndex(e => e.Id, "UQ_articulo_tienda_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticuloId).HasColumnName("articulo_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.TiendaId).HasColumnName("tienda_id");

            entity.HasOne(d => d.Articulo).WithMany(p => p.ArticuloTienda)
                .HasForeignKey(d => d.ArticuloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("articulo_tienda_Articulos_FK");

            entity.HasOne(d => d.Tienda).WithMany(p => p.ArticuloTienda)
                .HasForeignKey(d => d.TiendaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("articulo_tienda_Tienda_FK");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.Id, "AK_Table_1_id").IsUnique();

            entity.HasIndex(e => e.Id, "UQ_Clientes_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<ClienteArticulo>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("cliente_articulo");

            entity.HasIndex(e => e.Id, "AK_Table_1_id").IsUnique();

            entity.HasIndex(e => e.Id, "UQ_cliente_articulo_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArticuloId).HasColumnName("articulo_id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");

            entity.HasOne(d => d.Articulo).WithMany(p => p.ClienteArticulos)
                .HasForeignKey(d => d.ArticuloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cliente_articulo_Articulos_FK");

            entity.HasOne(d => d.Cliente).WithMany(p => p.ClienteArticulos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cliente_articulo_Clientes_FK");
        });

        modelBuilder.Entity<Tiendum>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => e.Id, "AK_Table_1_id").IsUnique();

            entity.HasIndex(e => e.Id, "UQ_Tienda_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Sucursal)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Id, "AK_Table_1_id").IsUnique();

            entity.HasIndex(e => e.Id, "UQ_Usuario_id").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
