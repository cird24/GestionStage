using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestiStage.Data;
using System.Threading.Tasks;

namespace GestiStage
{
    public class GestiStageDbInit
    {
         public async Task SeedDB(GestiStageDbContext context)
        {
            var departements = new List<Departement>
            {
                new Departement{Nom="Informatique"},
                new Departement{Nom="Administration"},
                new Departement{Nom="Soins Infirmiers"},
            };

            departements.ForEach(s => context.Departements.Add(s));
            await context.SaveChangesAsync();
        }
        
    }
}