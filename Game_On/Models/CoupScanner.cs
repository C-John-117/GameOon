using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_On.Models
{
    internal class CoupScanner
    {
        private int[][] grille;
        public CoupScanner(string grillestr)
        {
            if (grillestr == null) throw new ArgumentNullException(nameof(grillestr));
            if (grillestr.Length != 81) throw new ArgumentException("Expected 81 characters", nameof(grillestr));

            grille = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                grille[i] = new int[9]; // allocate inner arrays
                for (int j = 0; j < 9; j++)
                {
                    char c = grillestr[i * 9 + j];
                    if (c < '0' || c > '9') throw new FormatException($"Invalid character at pos {i * 9 + j}: '{c}'");
                    grille[i][j] = c - '0';
                }
            }
        }
        public bool isCoupValide(int X, int Y, int coup)
        {
            bool value = false;
            value = checkcase(X, Y, coup);
            if (value == false)
            {
                return false;
            }
            value = checklinehorizontal(X, Y, coup);
            if (value == false)
            {
                return false;
            }
            value = checklinevertical(X, Y, coup);

            if (value == true) 
            { 
                grille[X][Y] = coup;
            }
            return value;
        }

        private bool checklinehorizontal(int x, int y, int coup)
        {
            for (int i = 0; i < 9; i++)
            {
                if (grille[x][i] == coup)
                {
                    return false;
                }
            }
            return true;
        }

        private bool checklinevertical(int x, int y, int coup)
        {
            for (int i = 0; i < 9; i++)
            {
                if (grille[i][y] == coup)
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkcase(int x, int y, int coup)
        {
            if (checkCaseFrom((x / 3) * 3, (y / 3) * 3, coup))
            {
                return true;
            }
            return false;
        }

        private bool checkCaseFrom(int v1, int v2, int coup)
        {
            for (int i = v1; i <= v1 + 2; i++)
            {
                for (int j = v2; j <= v2 + 2; j++)
                {
                    if (grille[i][j] == coup)
                    {
                        return false;
                    }

                }
            }
            return true;
        }
    }
}
