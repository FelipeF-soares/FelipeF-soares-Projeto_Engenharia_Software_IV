using Microsoft.AspNetCore.Mvc;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Models;

namespace SmartCondWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class UnitController : Controller
{
    private readonly IUnitPersist persist;

    public UnitController(IUnitPersist persist)
    {
        this.persist = persist;
    }
    public IActionResult Index()
    {
        try
        {
            var units = persist.GetAllUnits();
            return View(units);
        }
        catch (Exception ex)
        {
            return RedirectToAction("Error","Shared");
        }
        
    }
}
