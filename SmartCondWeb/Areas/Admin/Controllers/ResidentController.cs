using Microsoft.AspNetCore.Mvc;
using SmartCondWeb.DataAcess.Persist;
using SmartCondWeb.Domain.People;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using X.PagedList.Extensions;

namespace SmartCondWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class ResidentController : Controller
{
    private readonly IResidentPersist residentPersist;
    private readonly IUnitPersist unitPersist;
    private readonly IWebHostEnvironment webHostEnvironment;

    public ResidentController(IResidentPersist residentPersist, IUnitPersist unitPersist, IWebHostEnvironment webHostEnvironment)
    {
        this.residentPersist = residentPersist;
        this.unitPersist = unitPersist;
        this.webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index(int? page, string? name)
    {
        var resisdents = residentPersist.GetAllResidentsInBuilder();
        if (name != null)
        {
            resisdents = residentPersist.GetResisdentsForName(name);
        }
        int pageSize = 5;
        int pageNumber = page ?? 1;
        

        int totalItems = resisdents.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var paginatedResidents = resisdents.Skip((pageNumber -1) * pageSize).Take(pageSize).ToList();

        ViewBag.CurrentPage = pageNumber;
        ViewBag.TotalPages = totalPages;

        return View(paginatedResidents);
    }

    [HttpGet]
    public IActionResult Add(int id)
    {
        Resident resident = new Resident();
        var unit = unitPersist.GetUnitForId(id);
        resident.Unit = unit;
        resident.UnitId = unit.Id;
        resident.UnitCodeResident = resident.GeneratorUnitCode(unit.UnitCode, unit.Residents.ToList());
        
        return View(resident);
    }
    [HttpPost]
    public IActionResult Add(Resident resident,IFormFile? file)
    {
        string wwwRootPath = webHostEnvironment.WebRootPath;
        resident.Id = 0;
        residentPersist.Add(resident);
        residentPersist.SaveChange();
        if (file != null)
        {
            string fileName = $"{resident.UnitCodeResident}_{resident.Name.Replace(" ","_")}{Path.GetExtension(file.FileName)}";
            string productPath = Path.Combine(wwwRootPath, @"images\residents");
            using(var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            resident.ImageURL = @"\images\residents\" + fileName;
            residentPersist.Update(resident);
            residentPersist.SaveChange();
        }
        return RedirectToAction("UnitResidents", "Resident", new { id = resident.UnitId});
    }
    [HttpGet]
    public IActionResult UnitResidents(int id)
    {
        var unit = unitPersist.GetUnitForId(id);
        return View(unit);
    }
}
