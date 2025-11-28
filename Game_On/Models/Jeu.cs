using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_On.Models
{
    [PrimaryKey("Id")]
    public class Jeu
    {
        public int Id { get; set; }

        public string NomJeu { get; set; }  

        public int GameCategoryId { get; set; }

        public bool EstDisponible { get; set; }

        //public Jeu() { }

        //public Jeu(int id, string nomJeu)
        //{
        //    Id = id;
        //    NomJeu = nomJeu;
        //}
    }
}
