using SmartCondWeb.Domain.Things;
using SmartCondWeb.Domain.Animal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SmartCondWeb.Domain.People;

public class Resident:Person
{
    [Required]
    [DisplayName("Código do Morador")]
    public string UnitCodeResident { get; set; }
    [Required]
    [DisplayName("E-mail")]
    [EmailAddress(ErrorMessage = "Tipo de e-mail inválido")]
    public string Email { get; set; }
    [DisplayName("Foto")]
    [ValidateNever]
    public string? ImageURL { get; set; }
    [ValidateNever]
    public IEnumerable<Pet>? Pets { get; set; }
    public int UnitId { get; set; }
    [ValidateNever]
    public Unit Unit { get; set; }

    public string GeneratorUnitCode(string unitCodeResident,  List<Resident> residents)
    {
        if (residents.Any())
        {
            int unitCodeLast = int.Parse(residents.LastOrDefault().UnitCodeResident);
            int newUnitCode = unitCodeLast - 1;
            if(newUnitCode == int.Parse(unitCodeResident))
            {
                newUnitCode = int.Parse(unitCodeResident);
                newUnitCode = newUnitCode + 99;
            }
            return newUnitCode.ToString();
        }
        int unitCode = int.Parse(unitCodeResident);
        unitCode = unitCode + 99;
        return unitCode.ToString();
    }
}
