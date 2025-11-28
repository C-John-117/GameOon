using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_On.Models
{
    [PrimaryKey("Id")]
    public class Partie
    {
        public int Id { get; set; }
        public DateTime DateDebut { get; set; }

        [AllowNull]
        public DateTime DateFin { get; set; }
        public int UtilisateurId { get; set; }

        [AllowNull]
        public string Save { get; set; } = null;
        public int SudokuId { get; set; }

        //public Partie() { }
        //public Partie(int id, DateTime dateDebut, DateTime dateFin)
        //{
        //    Id = id;
        //    DateDebut = dateDebut;
        //    DateFin = dateFin;
        //}

    }
}
