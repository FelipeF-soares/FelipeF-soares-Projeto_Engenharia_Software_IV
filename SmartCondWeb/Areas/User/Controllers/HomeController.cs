using Microsoft.AspNetCore.Mvc;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Domain.ViewModel;
using SmartCondWeb.Models;
using System.Diagnostics;

namespace SmartCondWeb.Areas.User.Controllers;
[Area("User")]
public class HomeController : Controller
{
    private readonly IHomeownerPersist homeownerPersist;
    private readonly IVehiclePersist vehiclePersist;
    private readonly IUnitPersist unitPersist;
    private readonly IVisitantPersist visitantPersist;
    private readonly IResidentPersist residentPersist;

    public HomeController(IHomeownerPersist homeownerPersist, IVehiclePersist vehiclePersist,
                          IUnitPersist unitPersist, IVisitantPersist visitantPersist, IResidentPersist residentPersist)
    {
        this.homeownerPersist = homeownerPersist;
        this.vehiclePersist = vehiclePersist;
        this.unitPersist = unitPersist;
        this.visitantPersist = visitantPersist;
        this.residentPersist = residentPersist;
    }

    public IActionResult Index()
    {
        DashboardVM dashboardVM = new DashboardVM();
        dashboardVM.TotalResidents = residentPersist.GetAllResident().Count;
        dashboardVM.TotalVehicles = vehiclePersist.GetAllVehicles().Count;
        dashboardVM.TotalUnits = unitPersist.GetAllUnits().Count;
        dashboardVM.TotalVisitant = visitantPersist.GetAllVisitants().Count;
        return View(dashboardVM);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
