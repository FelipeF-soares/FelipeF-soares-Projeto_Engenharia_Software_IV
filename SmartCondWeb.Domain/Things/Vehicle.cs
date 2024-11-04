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
    private string make;
    private string model;
    private string licensePlate;
    private string buildingRentedBuider;
    private string rentedUnitNumber;
    private string rentedFullName;
    [Key]
    public int Id { get; set; }
    [Required]
    [DisplayName("Marca")]
    public string Make 
    {
        get => make;
        set => make = value.ToUpper(); 
    }
    [Required]
    [DisplayName("Modelo")]
    public string Model 
    {
        get => model; 
        set => model = value.ToUpper();
    }
    [Required]
    [DisplayName("Placa")]
    public string LicensePlate 
    {
        get => licensePlate; 
        set => licensePlate = value.ToUpper();
    }
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
    public string? BuildingRentedBuider 
    {
        get => buildingRentedBuider;
        set => buildingRentedBuider = value.ToUpper();
    }
    [DisplayName("Apartamento do Locador")]
    public string? RentedUnitNumber 
    { 
        get => rentedUnitNumber;
        set => rentedUnitNumber = value.ToUpper(); 
    }
    [DisplayName("Nome Completo do Locador")]
    public string? RentedFullName 
    {
        get => rentedFullName;
        set => rentedFullName = value.ToUpper();
    }
    public int? UnitId { get; set; }
    public virtual Unit Unit { get; set; }
}
