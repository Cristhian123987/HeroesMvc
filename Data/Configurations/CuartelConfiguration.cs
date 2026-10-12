using HeroesMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeroesMvc.Data.Configurations;

public class CuartelConfiguration : IEntityTypeConfiguration<Cuartel>
{
    public void Configure(EntityTypeBuilder<Cuartel> builder)
    {
        // Tabla, esquema y CHECK de capacidad (la columna está en Cuarteles)
        builder.ToTable("Cuarteles", "dbo", t =>
            t.HasCheckConstraint(
                "CK_Cuarteles_Capacidad",
                "[Capacidad] > 0"));

        builder.HasKey(c => c.Id);

        // Nombre obligatorio, máximo 80, único
        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(80);

        builder.HasIndex(c => c.Nombre)
            .IsUnique()
            .HasDatabaseName("UQ_Cuarteles_Nombre");

        // Dirección obligatoria, máximo 150
        builder.Property(c => c.Direccion)
            .IsRequired()
            .HasMaxLength(150);
    }
}
