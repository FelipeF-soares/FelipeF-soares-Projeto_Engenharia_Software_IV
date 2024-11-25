using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SmartCondWeb.Domain.People;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.Domain.Events;

public class VisitorAccessControl
{
    [Key]
    public int Id { get; set; }
    public DateTime? Arrival { get; set; }
    public DateTime? Leave { get; set; }
    [Required(ErrorMessage ="Escolha uma opção")]
    [DisplayName("Número do Apartamento")]
    public string UnitNumber { get; set; }
    [Required(ErrorMessage = "Escolha uma opção")]
    [DisplayName("Bloco")]
    public string Building { get; set; }
    public int VisitantId { get; set; }
    [ValidateNever]
    public virtual Visitant Visitant  { get; set; }
}
