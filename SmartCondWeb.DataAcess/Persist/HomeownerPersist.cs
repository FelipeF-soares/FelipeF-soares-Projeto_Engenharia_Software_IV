using Microsoft.EntityFrameworkCore;
using SmartCondWeb.DataAcess.ContextPersist;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist;

public class HomeownerPersist : IHomeownerPersist
{
    private readonly SmartCondContext context;

    public HomeownerPersist(SmartCondContext context)
    {
        this.context = context;
    }
    public void Add(Homeowner entity)
    {
        context.Add(entity);
    }
    public void Update(Homeowner entity)
    {
        context.Update(entity);
    }

    public void Delete(Homeowner entity)
    {
       context.Remove(entity);
    }

    public List<Homeowner> GetAllHomeowners()
    {
        List<Homeowner> homeowners = context.Homeowners
                                  .Include(owner => owner.Units)
                                  .AsNoTracking()
                                  .ToList();
        return homeowners;
        
    }

    public Homeowner GetHomeownerForId(int id)
    {
        Homeowner homeowner = context.Homeowners.Include(owner => owner.Units)
                                                .AsNoTracking()
                                                .FirstOrDefault(owner => owner.Id == id);
                                                
        return homeowner;
    }

    public bool SaveChange()
    {
       return (context.SaveChanges())>0;
    }
}
