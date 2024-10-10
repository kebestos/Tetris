using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class Tetromino : Grid
    {
    private List<Piece> pieceListe;
    private int a;
    

        public Tetromino(params String[] stringlistp)
        {           
            this.a = 0;
            pieceListe = new List<Piece>();
            for (int i = 0; i < stringlistp.Count(); i++)
            {
                
                Piece b = new Piece(stringlistp[i]);
                pieceListe.Add(b);
            }
        }

        public Tetromino(List<Piece> piecelistp, int a)
        {
            this.a = a;
            this.pieceListe = piecelistp;
        }

        public override String ToString()
        {
            return pieceListe[a].ToString();
        }

        public Tetromino RotateRight()
        {
            int c = a;
            if (c == pieceListe.Count()-1)
            {
                c = 0;
            }
            else c += 1; 
            return new Tetromino(pieceListe,c);            
        }

        public Tetromino RotateLeft()
        {
            int q = a;
            if (q == 0)
            {
                q = pieceListe.Count()-1;
            }
            else q -= 1;
            return new Tetromino(pieceListe,q);
        }

        int Grid.Rows()
        {
            string currentstring = pieceListe[a].ToString();
            int rows = StringToMatrix.countRow(currentstring);
            return rows;
        }

        int Grid.Columns()
        {
            string currentstring = pieceListe[a].ToString();
            int cols = StringToMatrix.countcolumns(currentstring);
            return cols;
        }

        char Grid.CellAt(int Row, int Col)
        {
            string currentstring = pieceListe[a].ToString();
            char[,] matrix = new StringToMatrix(currentstring).blocks;
            return matrix[Row, Col];
        }

        public static int StartingRowOffset (Grid shape)
        {
            for(int i =0; i < shape.Rows(); i++)
            {
                for(int j = 0; j < shape.Columns(); j++)
                {
                    if (shape.CellAt(i, j) != '.') return -i;
                }
            }
            return 0;
        }  


        public static Tetromino T_SHAPE = new Tetromino(
                "....\n" +
                "TTT.\n" +
                ".T..\n"
            ,
                ".T..\n" +
                "TT..\n" +
                ".T..\n"
            ,
                "....\n" +
                ".T..\n" +
                "TTT.\n"
            ,
                ".T..\n" +
                ".TT.\n" +
                ".T..\n"
            );

        public static Tetromino I_SHAPE = new Tetromino(
                "....\n" +
                "IIII\n" +
                "....\n" +
                "....\n"
            ,
                "..I.\n" +
                "..I.\n" +
                "..I.\n" +
                "..I.\n"
            );

        public static Tetromino L_SHAPE = new Tetromino(
                "....\n" +
                "LLL.\n" +
                "L...\n" 
            ,
                "LL..\n" +
                ".L..\n" +
                ".L..\n"
            ,
                "....\n" +
                "..L.\n" +
                "LLL.\n"
            ,
                "..L.\n" +
                "..L.\n" +
                "..LL\n"
            );

        public static Tetromino J_SHAPE = new Tetromino(
                "....\n" +
                "JJJ.\n" +
                "..J.\n"
            ,
                ".J..\n" +
                ".J..\n" +
                "JJ..\n"
            ,
                "....\n" +
                "J...\n" +
                "JJJ.\n"
            ,
                "..JJ\n" +
                "..J.\n" +
                "..J.\n"
            );

        public static Tetromino S_SHAPE = new Tetromino(
                "...\n" +
                ".SS\n" +
                "SS.\n" 
            ,
                "S..\n" +
                "SS.\n" +
                ".S.\n"
            );

        public static Tetromino Z_SHAPE = new Tetromino(
                "...\n" +
                "ZZ.\n" +
                ".ZZ\n"
            ,
                "..Z\n" +
                ".ZZ\n" +
                ".Z.\n"
            );

        public static Tetromino O_SHAPE = new Tetromino(
                "OO\n" +
                "OO\n"
            );
    }
}
