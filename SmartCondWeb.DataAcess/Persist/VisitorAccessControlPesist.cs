using SmartCondWeb.DataAcess.ContextPersist;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist;

public class VisitorAccessControlPesist : IVisitorAccessControlPersist
{
    private readonly SmartCondContext context;

    public VisitorAccessControlPesist(SmartCondContext context)
    {
        this.context = context;
    }
    
    public void Add(VisitorAccessControl entity)
    {
        context.Add(entity);
    }
    public void Update(VisitorAccessControl entity)
    {
        context.Update(entity);
    }

    public void Delete(VisitorAccessControl entity)
    {
        context.Remove(entity);
    }

    public List<VisitorAccessControl> GetVisitorAccessControlList()
    {
        var visitorList = context.VisitorAccessControls.ToList();
        return visitorList;
    }

    public bool SaveChange()
    {
        return context.SaveChanges() > 0;
    }
}
