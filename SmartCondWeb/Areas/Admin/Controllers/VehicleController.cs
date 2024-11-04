using Microsoft.AspNetCore.Mvc;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Domain.Things;
using SmartCondWeb.Domain.Utils;

namespace SmartCondWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class VehicleController : Controller
{
    private readonly IUnitPersist unitPersist;
    private readonly IVehiclePersist vehiclePersist;
    private readonly IWebHostEnvironment webHostEnvironment;

    public VehicleController(IUnitPersist unitPersist, IVehiclePersist vehiclePersist, IWebHostEnvironment webHostEnvironment)
    {
        this.unitPersist = unitPersist;
        this.vehiclePersist = vehiclePersist;
        this.webHostEnvironment = webHostEnvironment;
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
    public IActionResult ReportVehicle()
    {
        var vehicles = vehiclePersist.GetAllVehicles();
        vehicles = vehicles.OrderBy(v => v.Unit.UnitCode).ToList();
        string wwwRootPath = webHostEnvironment.WebRootPath;
        CreateReportVehicles createReport = new CreateReportVehicles(wwwRootPath);
        var path = createReport.ReportPDF(vehicles);
        if (!System.IO.File.Exists(path))
        {
            return NotFound("O arquivo não foi encontrado!");
        }
        byte[] fileByte = System.IO.File.ReadAllBytes(path);
        return File(fileByte, "application/pdf", "Relatorio_De_Veiculos.pdf");
    }
}
