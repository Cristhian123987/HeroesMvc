namespace HeroesMvc.Models;

public class Vehiculo
{
    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public decimal CostoMantenimiento { get; set; }
    public DateTime FechaAlta { get; set; }

    // int? y Cuartel?: un vehículo puede estar en un cuartel o en ninguno,
    // y al borrar el cuartel la FK queda en NULL (SetNull).
    public int? CuartelId { get; set; }
    public Cuartel? Cuartel { get; set; }
}
