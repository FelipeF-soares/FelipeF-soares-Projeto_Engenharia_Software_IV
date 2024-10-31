using Microsoft.EntityFrameworkCore;
using SmartCondWeb.DataAcess.ContextPersist;
using SmartCondWeb.DataAcess.Persist.Interfaces;
using SmartCondWeb.Domain.Animal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.Persist;

public class PetPersist : IPetPersist
{
    private readonly SmartCondContext context;

    public PetPersist(SmartCondContext context)
    {
        this.context = context;
    }
    public void Add(Pet entity)
    {
        context.Add(entity);
    }

    public void Update(Pet entity)
    {
        context.Update(entity);
    }

    public void Delete(Pet entity)
    {
        context.Remove(entity);
    }

    public List<Pet> GetAllPets()
    {
        List<Pet> pets = context.Pets.Include(resident => resident.Resident)
                                     .AsNoTracking()
                                     .ToList();
        return pets;
    }

    public Pet GetPetForId(int id)
    {
        Pet pet = context.Pets.Include(resident => resident.Resident)
                              .AsNoTracking()
                              .FirstOrDefault(pet => pet.Id == id);
        return pet;
    }

    public bool SaveChange()
    {
        return (context.SaveChanges()) > 0;
    }
}
