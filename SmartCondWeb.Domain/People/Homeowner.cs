using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCondWeb.Domain.Things;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SmartCondWeb.Domain.People;

public class Homeowner : Person
{
    private string email;
    [Required(ErrorMessage = "E-mail é um Campo Obrigatório!")]
    [DisplayName("E-mail")]
    [EmailAddress(ErrorMessage ="Formato inválido de e-mail")]
    public string Email {
        get => email;
        set => email = value.ToLower();
    }
    [ValidateNever]
    public virtual ICollection<Unit> Units { get; set; }
}
