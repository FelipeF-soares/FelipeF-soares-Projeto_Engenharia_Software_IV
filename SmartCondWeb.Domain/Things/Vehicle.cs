using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.Domain.Things;

public class Vehicle
{
    [Key]
    public int Id { get; set; }
    [Required]
    [DisplayName("Marca")]
    public string Make { get; set; }
    [Required]
    [DisplayName("Modelo")]
    public string Model { get; set; }
    [Required]
    [DisplayName("Placa")]
    public string LicensePlate { get; set; }
    [Required]
    [DisplayName("Tipo")]
    public string Type { get; set; }
    [Required]
    [DisplayName("Numero da TAG")]
    public string TagNumber { get; set; }
    [Required]
    [DisplayName("Vaga Alugada?")]
    public bool Rented { get; set; }
    [DisplayName("Bloco do Locador")]
    public string? BuildingRentedBuider { get; set; }
    [DisplayName("Apartamento do Locador")]
    public string? RentedUnitNumber { get; set; }
    [DisplayName("Nome Completo do Locador")]
    public string? RentedFullName { get; set; }
    public int? UnitId { get; set; }
    public virtual Unit Unit { get; set; }
}
