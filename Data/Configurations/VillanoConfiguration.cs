using HeroesMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeroesMvc.Data.Configurations;

public class VillanoConfiguration : IEntityTypeConfiguration<Villano>
{
    public void Configure(EntityTypeBuilder<Villano> builder)
    {
        // Tabla, esquema y CHECK 1..5 (forma de EF Core 7+)
        builder.ToTable("Villanos", "dbo", t =>
            t.HasCheckConstraint(
                "CK_Villanos_NivelAmenaza",
                "[Nivel_Amenaza] BETWEEN 1 AND 5"));

        // Clave primaria
        builder.HasKey(v => v.Id);

        // Alias obligatorio, máximo 100
        builder.Property(v => v.Alias)
            .IsRequired()
            .HasMaxLength(100);

        // Propiedad NivelAmenaza -> columna Nivel_Amenaza
        builder.Property(v => v.NivelAmenaza)
            .HasColumnName("Nivel_Amenaza");

        // Monto con 2 decimales
        builder.Property(v => v.Recompensa)
            .HasPrecision(10, 2);

        // Si no se indica, la base pone la fecha actual
        builder.Property(v => v.FechaRegistro)
            .HasDefaultValueSql("GETDATE()");

        // Relación 1:N obligatoria; no se puede borrar una ciudad con villanos
        builder.HasOne(v => v.Ciudad)
            .WithMany(c => c.Villanos)
            .HasForeignKey(v => v.CiudadId)
            .IsRequired()
            .HasConstraintName("FK_Villanos_Ciudades")
            .OnDelete(DeleteBehavior.NoAction);
    }
}
