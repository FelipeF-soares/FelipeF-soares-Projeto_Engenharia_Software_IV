using SmartCondWeb.Domain.People;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.Domain.Animal;

public class Pet
{
    [Key]
    public int Id { get; set; }
    [Required]
    [DisplayName("Nome do PET")]
    public string Name { get; set; }
    [Required]
    [DisplayName("Tamanho")]
    public string Size { get; set; }
    [Required]
    [DisplayName("Raça")]
    public string Breeds { get; set; }
    public int ResidentId { get; set; }
    public virtual Resident Resident { get; set; }

}
