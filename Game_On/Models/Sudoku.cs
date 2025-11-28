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
    public class Sudoku
    {
        public int Id { get; set; }
        public string Puzzle { get; set; }
        public string Solution { get; set; }

        public DateTime Date { get; set; }
        public string Difficulte { get; set; }

        [AllowNull]
        public bool IsTraining { get; set; }

        //public Sudoku() { }
        //public Sudoku(string[,] puzzle, string[,] solution, DateTime date, string difficulte)
        //{
        //    Puzzle = puzzle;
        //    Solution = solution;
        //    Date = date;
        //    Difficulte = difficulte;
        //}
    }
}
