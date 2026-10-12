using HeroesMvc.Data.Configurations; // nuevo
using HeroesMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroesMvc.Data;

public class HeroesDbContext : DbContext
{
    public HeroesDbContext(DbContextOptions<HeroesDbContext> options)
        : base(options)
    {
    }

    // ... DbSet de la clase 2 (Equipos, Heroes, Fichas, Superpoderes,
    //     HeroesSuperpoderes) se mantienen sin cambios en tu proyecto ...

    // Clase 02 – Fluent API con archivos de configuración
    public DbSet<Ciudad> Ciudades => Set<Ciudad>();
    public DbSet<Villano> Villanos => Set<Villano>();

    // Ejercicio: Cuarteles y Vehículos
    public DbSet<Cuartel> Cuarteles => Set<Cuartel>();
    public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ... modelBuilder.Entity<Equipo>, <Heroe>, <Ficha>, <Superpoder>,
        //     <HeroeSuperpoder> de la clase 2: NO se borran, se conservan aquí ...

        // Registro explícito de las configuraciones (una por entidad)
        modelBuilder.ApplyConfiguration(new CiudadConfiguration());
        modelBuilder.ApplyConfiguration(new VillanoConfiguration());   // Prueba C: comentar esta línea
        modelBuilder.ApplyConfiguration(new CuartelConfiguration());
        modelBuilder.ApplyConfiguration(new VehiculoConfiguration());  // Prueba del ejercicio: comentar esta línea

        // Alternativa (registra todas las IEntityTypeConfiguration del proyecto):
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeroesDbContext).Assembly);
    }
}
