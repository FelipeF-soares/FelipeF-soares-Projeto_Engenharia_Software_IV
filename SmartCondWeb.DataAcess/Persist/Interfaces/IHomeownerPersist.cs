using SmartCondWeb.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist.Interfaces;

public interface IHomeownerPersist : IGenericPersist<Homeowner>
{
    Homeowner GetHomeownerForId(int id);
    List<Homeowner> GetAllHomeowners();
}
