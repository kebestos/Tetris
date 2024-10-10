using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class MovableGrid : Grid
    {
        public Tetromino Tetro;
        public int n;
        public int m;
        string currentstring;
        public char[,] matrix;


        public int Row
        {
            get;set;
        }
        
        public int Col
        {
            get;set;
        }

        public MovableGrid(Tetromino Tete, int row, int col, int n, int m)
        {
            this.n = n;
            this.m = m;
            this.Tetro = Tete;
            this.Row = row;
            this.Col = col;
            this.IsFalling = false;
            this.currentstring = Tetro.ToString();
            this.matrix = new StringToMatrix(currentstring).blocks;
        }

        public override String ToString()
        {
            return Tetro.ToString();
        }

        public bool IsFalling
        {
            get; set;
        }

        int Grid.Rows()
        {
            int rows = StringToMatrix.countRow(currentstring);
            return rows;
        }

        int Grid.Columns()
        {
            int cols = StringToMatrix.countcolumns(currentstring);
            return cols;
        }

        char Grid.CellAt(int Row, int Col)
        {
            return matrix[Row, Col];
        }

        public MovableGrid GridRotateRight()
        {
            Tetromino R = Tetro;
            R = R.RotateRight();
            return new MovableGrid(R, Row, Col, n, m);
        }

        public MovableGrid GridRotateLeft()
        {
            Tetromino R = Tetro;
            R = R.RotateLeft();
            return new MovableGrid(R, Row, Col, n, m);
        }

        public MovableGrid MoveLeft()
        {
            int a = Col - 1;
            return new MovableGrid(Tetro,Row,a,n,m);
        }

        public MovableGrid MoveRight()
        {
            int a = Col + 1;
            return new MovableGrid(Tetro, Row, a, n, m);
        }
    }
}
