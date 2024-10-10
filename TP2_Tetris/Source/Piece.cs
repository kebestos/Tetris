using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public interface Grid
    {
        int Rows();
        int Columns();
        char CellAt(int row, int col);
    }

    public class Piece : Grid
    {
        char[,] matrix;
        int rows;
        int cols;
        public Piece(string s)
        {
            this.rows = StringToMatrix.countRow(s);
            this.cols = StringToMatrix.countcolumns(s);
            this.matrix = new StringToMatrix(s).blocks;
        }

        public override String ToString()
        {
            return StringToMatrix.Inverse(matrix,rows,cols);
        }
        int  Grid.Rows()
        {
            return rows;
        }

        int Grid.Columns()
        {
            return cols;
        }

        char Grid.CellAt(int Row, int Col)
        {
            char c = matrix[Row, Col];
            return c;
        }
    }
}
