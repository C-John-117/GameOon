using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Game_On.Models
{
   
    [PrimaryKey("Id")]
    public class Entreprise
    {
        public int Id { get; set; }
        public string NomEntreprise { get; set; }
        public string NomDomaine { get; set; }
        public int Score {  get; set; } = 0;

      


        //public Entreprise() 
        //{
        //    Departements = new List<Departement>();
        //}

        //public Entreprise (int id, string nomEntreprise)
        //{
        //    Id = id;
        //    NomEntreprise = nomEntreprise;
        //    Departements = new List<Departement>();
        //}
    }
}
