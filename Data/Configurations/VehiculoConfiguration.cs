using HeroesMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeroesMvc.Data.Configurations;

public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        // TODO 1: tabla y esquema
        builder.ToTable("Vehiculos", "dbo");

        // TODO 2: clave primaria
        builder.HasKey(v => v.Id);

        // TODO 3: Placa -> obligatoria, 10, columna Nro_Placa
        builder.Property(v => v.Placa)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("Nro_Placa");

        // TODO 4: índice único sobre Placa (UQ_Vehiculos_Placa)
        builder.HasIndex(v => v.Placa)
            .IsUnique()
            .HasDatabaseName("UQ_Vehiculos_Placa");

        // TODO 5: Modelo -> obligatorio, 60
        builder.Property(v => v.Modelo)
            .IsRequired()
            .HasMaxLength(60);

        // TODO 6: CostoMantenimiento -> precisión 12,2
        builder.Property(v => v.CostoMantenimiento)
            .HasPrecision(12, 2);

        // TODO 7: FechaAlta -> valor por defecto GETDATE()
        builder.Property(v => v.FechaAlta)
            .HasDefaultValueSql("GETDATE()");

        // TODO 8: relación con Cuartel, FK opcional, borrar = dejar en null
        builder.HasOne(v => v.Cuartel)
            .WithMany(c => c.Vehiculos)
            .HasForeignKey(v => v.CuartelId)
            .IsRequired(false)
            .HasConstraintName("FK_Vehiculos_Cuarteles")
            .OnDelete(DeleteBehavior.SetNull);
    }
}
