using SmartCondWeb.Domain.People;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SmartCondWeb.Domain.Things;

public class Unit
{
    [Key]
    public int Id { get; set; }
    [Required]
    [DisplayName("Código da Unidade")]
    public string UnitCode { get; set; }
    [Required]
    [DisplayName("Numero do Apartamento")]
    public string UnitNumber { get; set; }
    [Required]
    [DisplayName("Bloco")]
    public string Building { get; set; }
    public int? HomeownerId { get; set; }
    [ValidateNever]
    public virtual Homeowner Homeowner { get; set; }
    [ValidateNever]
    public virtual Vehicle? Vehicles { get; set; }
    [ValidateNever]
    public virtual ICollection<Resident>? Residents { get; set; }
    
}
