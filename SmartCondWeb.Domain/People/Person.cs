using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.Domain.People;

public abstract class Person
{
    private string name;
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage ="Nome Completo é um Campo Obrigatório!")]
    [DisplayName("Nome Completo")]
    public string Name {
        get => name;

        set => name = value.ToUpper(); 
      }
    [Required(ErrorMessage = "CPF/CNPJ é um Campo Obrigatório!")]
    [DisplayName("CPF/CNPJ")]
    public string IdentificationDocument { get; set; }
    [Required(ErrorMessage = "Celular é um Campo Obrigatório!")]
    [DisplayName("Celular")]
    public string CellPhone { get; set; }
}
