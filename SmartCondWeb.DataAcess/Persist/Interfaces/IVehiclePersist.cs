using SmartCondWeb.Domain.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist.Interfaces;

public interface IVehiclePersist : IGenericPersist<Vehicle>
{
    Vehicle GetVehicleForId(int id);
    List<Vehicle> GetAllVehicles();
}
