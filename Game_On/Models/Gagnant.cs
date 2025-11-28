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
    public class Gagnant
    {
        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public int Score { get; set; }
        //public Gagnant() { }
        //public Gagnant(int id, int utilisateurId, int score)
        //{
        //    Id = id;
        //    UtilisateurId = utilisateurId;
        //    Score = score;
        //}

    }
}
