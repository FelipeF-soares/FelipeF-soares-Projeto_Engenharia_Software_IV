using Microsoft.EntityFrameworkCore;
using SmartCondWeb.Domain.Animal;
using SmartCondWeb.Domain.People;
using SmartCondWeb.Domain.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCondWeb.DataAcess.ContextPersist;

public class SmartCondContext: DbContext
{
    public SmartCondContext(DbContextOptions<SmartCondContext> options) : base(options)
    {
        
    }

    public DbSet<Unit> Units { get; set; }
    public DbSet<Homeowner> Homeowners { get; set; }
    public DbSet<Resident> Residents { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Unit>()
                    .HasOne(unit => unit.Homeowner) // Uma unidade tem um proprietário
                    .WithMany(homeowner => homeowner.Units) // Um proprietário tem muitas unidades
                    .HasForeignKey(unit => unit.HomeownerId) // A chave estrangeira é OwnerId
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);

        modelBuilder.Entity<Homeowner>()
                    .HasIndex(person => person.IdentificationDocument)
                    .IsUnique();


        modelBuilder.Entity<Vehicle>()
                    .HasOne(vehicle => vehicle.Unit)
                    .WithOne(unit => unit.Vehicles)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);
        modelBuilder.Entity<Vehicle>()
                    .HasIndex(vehicle => vehicle.LicensePlate)
                    .IsUnique();

        modelBuilder.Entity<Unit>().HasData
             (
                 new Unit { Id = 1, UnitCode = "100100", UnitNumber = "01", Building = "BLOCO 1" },
                 new Unit { Id = 2, UnitCode = "100200", UnitNumber = "02", Building = "BLOCO 1" },
                 new Unit { Id = 3, UnitCode = "100300", UnitNumber = "03", Building = "BLOCO 1" },
                 new Unit { Id = 4, UnitCode = "100400", UnitNumber = "04", Building = "BLOCO 1" },
                 new Unit { Id = 5, UnitCode = "100500", UnitNumber = "05", Building = "BLOCO 1" },

                 new Unit { Id = 6, UnitCode = "101100", UnitNumber = "11", Building = "BLOCO 1" },
                 new Unit { Id = 7, UnitCode = "101200", UnitNumber = "12", Building = "BLOCO 1" },
                 new Unit { Id = 8, UnitCode = "101300", UnitNumber = "13", Building = "BLOCO 1" },
                 new Unit { Id = 9, UnitCode = "101400", UnitNumber = "14", Building = "BLOCO 1" },
                 new Unit { Id = 10, UnitCode = "101500", UnitNumber = "15", Building = "BLOCO 1" },
                 new Unit { Id = 11, UnitCode = "101600", UnitNumber = "16", Building = "BLOCO 1" },

                 new Unit { Id = 12, UnitCode = "102100", UnitNumber = "21", Building = "BLOCO 1" },
                 new Unit { Id = 13, UnitCode = "102200", UnitNumber = "22", Building = "BLOCO 1" },
                 new Unit { Id = 14, UnitCode = "102300", UnitNumber = "23", Building = "BLOCO 1" },
                 new Unit { Id = 15, UnitCode = "102400", UnitNumber = "24", Building = "BLOCO 1" },
                 new Unit { Id = 16, UnitCode = "102500", UnitNumber = "25", Building = "BLOCO 1" },
                 new Unit { Id = 17, UnitCode = "102600", UnitNumber = "26", Building = "BLOCO 1" },

                 new Unit { Id = 18, UnitCode = "103100", UnitNumber = "31", Building = "BLOCO 1" },
                 new Unit { Id = 19, UnitCode = "103200", UnitNumber = "32", Building = "BLOCO 1" },
                 new Unit { Id = 20, UnitCode = "103300", UnitNumber = "33", Building = "BLOCO 1" },
                 new Unit { Id = 21, UnitCode = "103400", UnitNumber = "34", Building = "BLOCO 1" },
                 new Unit { Id = 22, UnitCode = "103500", UnitNumber = "35", Building = "BLOCO 1" },
                 new Unit { Id = 23, UnitCode = "103600", UnitNumber = "36", Building = "BLOCO 1" }
             );

        base.OnModelCreating(modelBuilder);

    }
}
