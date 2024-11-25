using SmartCondWeb.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist.Interfaces;

public interface IVisitorAccessControlPersist : IGenericPersist<VisitorAccessControl>
{
    List<VisitorAccessControl> GetVisitorAccessControlList();
}
