using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_On.Models
{
    [PrimaryKey("Id")]
    public class GameCategory
    {
        public int Id { get; set; }
        public string NomGameCategory { get; set; }


        //public List<Jeu> Jeux { get; set; }

        //public GameCategory() { }

        //public GameCategory(int id, string nomGameCategory, List<Jeu> jeux)
        //{
        //    Id = id;
        //    NomGameCategory = nomGameCategory;
        //    Jeux = jeux;
        //}

    }
}
