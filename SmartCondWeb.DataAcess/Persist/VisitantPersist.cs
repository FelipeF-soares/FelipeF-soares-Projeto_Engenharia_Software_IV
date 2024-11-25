using SmartCondWeb.DataAcess.ContextPersist;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist;

public class VisitantPersist : IVisitantPersist
{
    private readonly SmartCondContext context;

    public VisitantPersist(SmartCondContext context)
    {
        this.context = context;
    }
    public void Add(Visitant entity)
    {
        context.Add(entity);
    }
    public void Update(Visitant entity)
    {
        context.Update(entity);
    }

    public void Delete(Visitant entity)
    {
        context.Remove(entity);
    }
    public bool SaveChange()
    {
        return context.SaveChanges() > 0;
    }

    public Visitant GetVisitantByIdentificationDocument(string document)
    {
        return context.Visitants.FirstOrDefault(visitant => visitant.IdentificationDocument == document);
    }

    public List<Visitant> GetAllVisitants()
    {
        return context.Visitants.ToList();
    }
}
