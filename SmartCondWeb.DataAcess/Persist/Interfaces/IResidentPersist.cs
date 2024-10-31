using SmartCondWeb.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist.Interfaces;

public interface IResidentPersist : IGenericPersist<Resident>
{
    Resident GetResidentForId(int id);
    List<Resident> GetAllResident();
    IQueryable<Resident> GetAllResidentsInBuilder();
    IQueryable<Resident> GetResisdentsForName(string name);
}
