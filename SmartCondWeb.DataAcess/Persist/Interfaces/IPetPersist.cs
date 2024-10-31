using SmartCondWeb.Domain.Animal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist.Interfaces;

public interface IPetPersist:IGenericPersist<Pet>
{
    Pet GetPetForId(int id);
    List<Pet> GetAllPets();
}
