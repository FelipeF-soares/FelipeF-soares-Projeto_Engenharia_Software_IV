using SmartCondWeb.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist.Interfaces;

public interface IVisitantPersist:IGenericPersist<Visitant>
{
    Visitant GetVisitantByIdentificationDocument(string document);
    List<Visitant> GetAllVisitants();
}
