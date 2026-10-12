using HeroesMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeroesMvc.Data.Configurations;

public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {
        // Tabla y esquema
        builder.ToTable("Ciudades", "dbo");

        // Clave primaria
        builder.HasKey(c => c.Id);

        // Obligatorio y longitud
        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Pais)
            .IsRequired()
            .HasMaxLength(60);

        // No pueden existir dos ciudades con el mismo nombre
        builder.HasIndex(c => c.Nombre)
            .IsUnique()
            .HasDatabaseName("UQ_Ciudades_Nombre");

        // La relación con Villano NO se configura aquí: se configura una sola
        // vez desde el dependiente (VillanoConfiguration).
    }
}
