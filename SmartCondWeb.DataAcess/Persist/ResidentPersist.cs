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

public class ResidentPersist : IResidentPersist
{
    private readonly SmartCondContext context;

    public ResidentPersist(SmartCondContext context)
    {
        this.context = context;
    }
    public void Add(Resident entity)
    {
        context.Add(entity);
    }
    public void Update(Resident entity)
    {
        context.Update(entity);
    }

    public void Delete(Resident entity)
    {
        context.Remove(entity);
    }

    public List<Resident> GetAllResident()
    {
        List<Resident> residents = context.Residents.Include(unit => unit.Unit)
                                                    .OrderBy(unit => unit.Unit.UnitNumber)
                                                    .AsNoTracking()
                                                    .ToList();
        return residents;
    }

    public Resident GetResidentForId(int id)
    {
        Resident resident = context.Residents.Include(unit => unit.Unit)
                                             .AsNoTracking()
                                             .FirstOrDefault(reside => reside.Id == id);
        return resident;
    }

    public bool SaveChange()
    {
        return (context.SaveChanges()) > 0;
    }

    public IQueryable<Resident> GetAllResidentsInBuilder()
    {
       var query = context.Residents.OrderBy(unit => unit.Unit.UnitNumber)
                           .Include(unit=>unit.Unit);
        return query;
    }

    public IQueryable<Resident> GetResisdentsForName(string name)
    {
        var resisdents = context.Residents.OrderBy(unit => unit.Unit.UnitNumber)
                                           .Include(unit => unit.Unit)
                                           .Where(re => re.Name == name);
        return resisdents;
    }
}
