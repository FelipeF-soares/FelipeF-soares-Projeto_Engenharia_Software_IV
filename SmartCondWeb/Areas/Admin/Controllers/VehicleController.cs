using Microsoft.AspNetCore.Mvc;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Domain.Things;

namespace SmartCondWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class VehicleController : Controller
{
    private readonly IUnitPersist unitPersist;
    private readonly IVehiclePersist vehiclePersist;

    public VehicleController(IUnitPersist unitPersist, IVehiclePersist vehiclePersist)
    {
        this.unitPersist = unitPersist;
        this.vehiclePersist = vehiclePersist;
    }
    public IActionResult Index()
    {
        var vehicles = vehiclePersist.GetAllVehicles();
        return View(vehicles);
    }
    public IActionResult Add(int id)
    {
        try
        {
            Vehicle vehicle = new Vehicle();
            vehicle.UnitId = id;
            return View(vehicle);
        }
        catch (Exception ex) 
        {
            return RedirectToAction("Error", "Shared");
        }
        
    }
    [HttpPost]
    public IActionResult Add(Vehicle vehicle)
    {
        try
        {
            vehicle.Id = 0;
            vehiclePersist.Add(vehicle);
            vehiclePersist.SaveChange();
            return RedirectToAction("Index", "Unit");
        }
        catch (Exception ex) 
        {
            return RedirectToAction("Error", "Shared");
        }
    }

    public IActionResult Update(int  id) 
    {
        try
        {
            var vehicle = vehiclePersist.GetVehicleForId(id);
            return View(vehicle);
        }
        catch (Exception ex) 
        {
            return RedirectToAction("Error", "Shared");
        }
    }
    [HttpPost]
    public IActionResult Update(Vehicle vehicle) 
    {
        try
        {
            vehiclePersist.Update(vehicle);
            vehiclePersist.SaveChange();
            return RedirectToAction("Index", "Unit");
        }
        catch(Exception ex) 
        {
            return RedirectToAction("Error", "Shared");
        }
    }

    public IActionResult Delete(int id)
    {
        try
        {
            Vehicle vehicle = vehiclePersist.GetVehicleForId(id);
            vehiclePersist.Delete(vehicle);
            vehiclePersist.SaveChange();
            return RedirectToAction("Index", "Unit");
        }
        catch (Exception ex)
        {
            return RedirectToAction("Error", "Shared");
        }
    }
}
