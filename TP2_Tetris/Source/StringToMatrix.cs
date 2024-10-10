using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class StringToMatrix
    {
        public char[,] blocks;
        private string grid;
     

        public StringToMatrix(string grid)
        {
            int cols = countcolumns(grid);
            int rows = countRow(grid);
            this.grid = grid;
            this.blocks = new char[rows, cols];
            int a = 0;
            int b = 0; 
            foreach (char c in grid)
            {
                if (c == '\n')
                {
                    a++;
                    b = 0;
                }
                else
                {
                    blocks[a,b] = c;
                    b++;                    
                }
            }
            
            
        }        

        public static int countcolumns(string grid)
        {
            int nbcols = 0;
            foreach(char c in grid)
            {
                if(c == '\n')
                {
                    break;
                }
                else
                {
                    nbcols++;
                }
            }
            return nbcols;
        }

        public static int countRow(string grid)
        {
            int nbRows = 0;
            foreach(char c in grid)
            {
                if(c == '\n')
                {
                    nbRows++;
                }
            }
            return nbRows;
        }

        public static string Inverse(char[,] matrix, int rows, int cols)
        {
            string s = "";
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    s += matrix[i,j];
                }
                s += "\n";
            }
            return s;
        }
    }
}
