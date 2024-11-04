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
        var units = persist.GetAllUnits();
        string wwwRootPath = webHostEnvironment.WebRootPath;
        CreateReportUnits createReport = new CreateReportUnits(wwwRootPath);
        var path = createReport.ReportPDF(units);
        if (!System.IO.File.Exists(path)) 
        { 
            return NotFound("O arquivo não foi encontrado!");
        }
        byte[] fileByte = System.IO.File.ReadAllBytes(path);
        return File(fileByte, "application/pdf", "Relatório_Das_Unidades.pdf");
    }
}
