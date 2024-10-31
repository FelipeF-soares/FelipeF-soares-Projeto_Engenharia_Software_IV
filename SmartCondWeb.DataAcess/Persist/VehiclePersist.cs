using Microsoft.EntityFrameworkCore;
using SmartCondWeb.DataAcess.ContextPersist;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Domain.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist;

public class VehiclePersist : IVehiclePersist
{
    private readonly SmartCondContext context;

    public VehiclePersist(SmartCondContext context)
    {
        this.context = context;
    }
    public void Add(Vehicle entity)
    {
        context.Add(entity);
    }

    public void Update(Vehicle entity)
    {
        context.Update(entity);
    }

    public void Delete(Vehicle entity)
    {
        context.Remove(entity);
    }

    public List<Vehicle> GetAllVehicles()
    {
        List<Vehicle> vehicles = context.Vehicles
                                        .Include(unit => unit.Unit)
                                        .AsNoTracking()
                                        .ToList();
        return vehicles;
    }

    public Vehicle GetVehicleForId(int id)
    {
        Vehicle vehicle = context.Vehicles.Include(unit => unit.Unit)
                                            .AsNoTracking()
                                            .FirstOrDefault(vehicle => vehicle.Id == id);
        return vehicle;
    }

    public bool SaveChange()
    {
        return context.SaveChanges() > 0;
    }
}
