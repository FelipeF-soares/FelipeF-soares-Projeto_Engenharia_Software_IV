using Microsoft.AspNetCore.Mvc.Rendering;
using SmartCondWeb.Domain.Things;
using SmartCondWeb.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist.Interfaces;

public interface IUnitPersist : IGenericPersist<Unit>
{
    Unit GetUnitForId(int id);
    List<Unit> GetAllUnits();
    HomeownerVM GetAllUnitsForAddOwner();
}
