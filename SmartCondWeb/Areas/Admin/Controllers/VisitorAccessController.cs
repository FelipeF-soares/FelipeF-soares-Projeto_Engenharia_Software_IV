using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCondWeb.DataAcess.Persist;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Domain.Events;
using SmartCondWeb.Domain.People;

namespace SmartCondWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class VisitorAccessController : Controller
{
    private readonly IVisitantPersist visitantPersist;

    public VisitorAccessController(IVisitantPersist visitantPersist)
    {
        this.visitantPersist = visitantPersist;
    }
    [HttpGet]
    public IActionResult Index(string? document)
    {
        VisitorAccessControl visitorAccessControl = new VisitorAccessControl();
        if (document == null)
        {
            return View(visitorAccessControl);
        }
        else
        {
            var visit = visitantPersist.GetVisitantByIdentificationDocument(document);
            if(visit != null)
            {
                visitorAccessControl.Visitant = visit;
                visitorAccessControl.VisitantId = visit.Id;
                return View(visitorAccessControl);
            }
            TempData["error"] = "Não Localizado!";
            return View(visitorAccessControl);
        }
    }

    public IActionResult AddVisitant() 
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddVisitant(Visitant visitant)
    {
        if (ModelState.IsValid)
        {
            try
            {
                visitantPersist.Add(visitant);
                visitantPersist.SaveChange();
                TempData["success"] = "Informações Salvas Com Sucesso!";
                return RedirectToAction("Index", "VisitorAccess", new { document = visitant.IdentificationDocument });
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("IdentificationDocument","Documento já cadastrado verifique!");
            }
            
        }
        return View(visitant);
    }

}
