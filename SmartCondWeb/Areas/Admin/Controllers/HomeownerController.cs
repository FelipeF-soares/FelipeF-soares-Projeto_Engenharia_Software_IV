using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Domain.People;
using SmartCondWeb.Domain.ViewModel;
using System.Drawing.Text;


namespace SmartCondWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class HomeownerController : Controller
{
    private readonly IHomeownerPersist ownerpersist;
    private readonly IUnitPersist unitPersist;

    public HomeownerController(IHomeownerPersist ownerpersist,IUnitPersist unitPersist)
    {
        this.ownerpersist = ownerpersist;
        this.unitPersist = unitPersist;
    }
    public IActionResult Index()
    {
        try
        {
            var homeowners = ownerpersist.GetAllHomeowners();
            return View(homeowners);
        }
        catch (Exception ex) 
        {
            return RedirectToAction("Error", "Shared");
        }
    }
    public IActionResult Add()
    {
        try
        {
            HomeownerVM homeownerVM = unitPersist.GetAllUnitsForAddOwner();
            
            return View(homeownerVM);
        }
        catch (Exception ex)
        {
            return RedirectToAction("Error", "Shared");
        }
    }
    [HttpPost]
    public IActionResult Add(HomeownerVM homeownerVM)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var homeowner = homeownerVM.Homeowner;
                ownerpersist.Add(homeowner);
                ownerpersist.SaveChange();
                var unit = unitPersist.GetUnitForId(homeownerVM.VmId);
                unit.HomeownerId = homeowner.Id;
                unitPersist.Update(unit);
                unitPersist.SaveChange();
                return RedirectToAction("Index", "Homeowner");
            }
            catch (DbUpdateException) 
            {
                ModelState.AddModelError("Homeowner.IdentificationDocument", "Documento já cadastrado verifique!");
            }
            
        }        
                homeownerVM = unitPersist.GetAllUnitsForAddOwner();
                return View(homeownerVM);
    }
    public IActionResult Update(int id)
    {
            var homeowner = ownerpersist.GetHomeownerForId(id);
            HomeownerVM homeownerVM = unitPersist.GetAllUnitsForAddOwner();
            homeownerVM.Homeowner = homeowner;
            return View(homeownerVM);
    }
    [HttpPost]
    public IActionResult Update(HomeownerVM homeownerVM)
    {
        int id = homeownerVM.Homeowner.Id;
        if (ModelState.IsValid)
        {
            try
            {
                var homeowner = homeownerVM.Homeowner;
                ownerpersist.Update(homeowner);
                ownerpersist.SaveChange();
                if (homeownerVM.VmId != 0)
                {
                    var unit = unitPersist.GetUnitForId(homeownerVM.VmId);
                    unit.HomeownerId = homeowner.Id;
                    unitPersist.Update(unit);
                    unitPersist.SaveChange();
                }

                return RedirectToAction("Index", "Homeowner");
            }
            catch (DbUpdateException) 
            {
                ModelState.AddModelError("Homeowner.IdentificationDocument", "Documento já cadastrado verifique!");
            }
            var homeownerErro = ownerpersist.GetHomeownerForId(id);
            homeownerVM = unitPersist.GetAllUnitsForAddOwner();
            homeownerVM.Homeowner = homeownerErro;
            return View(homeownerVM);
        }
        return View();
    }
    public IActionResult Delete(int id)
    {
        try
        {
            var homeowner =ownerpersist.GetHomeownerForId(id);
            ownerpersist.Delete(homeowner);
            ownerpersist.SaveChange();
            return RedirectToAction("Index", "Homeowner");
        }
        catch (Exception ex)
        {
            return RedirectToAction("Error", "Shared");
        }
    }

}
