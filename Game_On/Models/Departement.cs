using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_On.Models
{
   
    [PrimaryKey("Id")]
    public class Departement
    {
        public int Id { get; set; }
        public string NomDepartement {  get; set; }
        public int EntrepriseId { get; set; }

    }
}
