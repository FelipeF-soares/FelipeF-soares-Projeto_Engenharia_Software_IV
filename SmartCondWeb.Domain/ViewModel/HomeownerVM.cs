using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartCondWeb.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.Domain.ViewModel;

public class HomeownerVM
{
    [ValidateNever]
    public int VmId { get; set; }
    public Homeowner Homeowner { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem> UnitysEmpty { get; set; }
}
