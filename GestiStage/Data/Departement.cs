using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestiStage.Data
{
    public class Departement
    {

        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

    }
}