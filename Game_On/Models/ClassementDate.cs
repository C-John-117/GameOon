using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_On.Models
{
    [PrimaryKey("Id")]
    public partial class ClassementDate : ObservableObject
    {

        public int Id { get; set; }

        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }

        public int? EntrepriseId { get; set; }
        public Entreprise? Entreprise { get; set; }

        public int? DepartementId { get; set; }
        public Departement? Departement { get; set; }

        public int Score { get; set; }

        public DateTime DateClassement { get; set; }
    }
}
