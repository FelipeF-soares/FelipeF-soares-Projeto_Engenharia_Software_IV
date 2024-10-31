using Microsoft.AspNetCore.Mvc;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Models;
using SmartCondWeb.Domain.Utils;
namespace SmartCondWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class UnitController : Controller
{
    private readonly IUnitPersist persist;
    private readonly IWebHostEnvironment webHostEnvironment;


    public UnitController(IUnitPersist persist, IWebHostEnvironment webHostEnvironment)
    {
        this.persist = persist;
        this.webHostEnvironment = webHostEnvironment;
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
    public IActionResult UnitReport()
    {
        var units = persist.GetAllUnits().ToArray();
        string wwwRootPath = webHostEnvironment.WebRootPath;
        CreateReport createReport = new CreateReport(wwwRootPath);
        createReport.ReportPDF(units);
        return View();
    }
}
