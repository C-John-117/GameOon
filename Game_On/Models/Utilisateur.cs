using Game_On.Data.Context;
using Game_On.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_On.Models
{
  
    [PrimaryKey("Id")]
    public partial class Utilisateur
    {


        public int Id { get; set; }
        public string NomUtilisateur { get; set; }
        public string PrenomUtilisateur { get; set; }
        public int EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }
        public int DepartementId { get; set; }
        public Departement Departement { get; set; }
        public DateTime LoginTime { get; set; }
        public int Score { get; set; }
        public DateTime LogoutTime { get; set; }

        public string Email { get; set; }

        public string Pseudo {  get; set; }

        public string MotDePasse { get; set; }

        public TimeSpan TempsDeJeuCumule { get; set; }




    }
}
