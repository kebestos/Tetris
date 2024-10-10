using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class Board
    {
        readonly int rows;
        readonly int columns;
        List<MovableGrid> blockliste;
        MovableGrid CurrentBlock = null;        

        public Board(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            this.blockliste = new List<MovableGrid>();
        }

        public override String ToString()
        {
            char[,] matrix = new char[rows, columns];
            for(int a = 0; a < rows; a++)
            {
                for(int b = 0; b < columns; b++)
                {
                    matrix[a, b] = '.';
                }
            }
            foreach(MovableGrid m in blockliste)
            {
                int posX = m.Row;
                int posY = m.Col;
                for (int i = 0; i < m.n; i++)
                {
                    for (int j = 0; j < m.m; j++)
                    {
                        if(posX + i >=0 && posY + j <= columns - 1 && posY + j >= 0)
                        {
                            if(m.matrix[i, j] != '.')
                            {
                                matrix[posX + i, posY + j] = m.matrix[i, j];
                            }                            
                        }
                    }
                }                
            }         
           
            
            string l = StringToMatrix.Inverse(matrix,rows,columns);
            return l;
        }

        public bool IsFallingBlock()
        {
            if (CurrentBlock == null)
            {
                return false;
            }
            else
            {
                return CurrentBlock.IsFalling;
            }            
        }

        public void Drop(Tetromino shape)
        {
            Grid Gshape = shape;
            int n = Gshape.Rows();
            int m = Gshape.Columns();
            int x = Tetromino.StartingRowOffset(Gshape);
            int y = columns / 2 - m / 2;
            MovableGrid block = new MovableGrid(shape,x,y,n,m);           
            if (CurrentBlock == null)
            {
                blockliste.Add(block);
                CurrentBlock = block;
                CurrentBlock.IsFalling = true;
            }
            else
            {
                throw new System.ArgumentException("A block is already falling.");
            }
            
        }

        public void Tick()
        {            
            if (CurrentBlock != null)
            {                
                bool ko = KoTick();                                   
                if(ko == false)
                {
                    int NbRowVide = CountRowDownOffset(CurrentBlock);
                    if (CurrentBlock.Row + CurrentBlock.n - NbRowVide != rows)
                    {
                        CurrentBlock.Row += 1;
                    }
                    else if (CurrentBlock.Row + CurrentBlock.n - NbRowVide == rows)
                    {
                        CurrentBlock = null;
                        IsFallingBlock();
                        TryToRemoveRow();
                    }
                }   
            }            
        }
        
        public void MoveDown()
        {
            Tick();
        }

        public void MoveLeft()
        {
            if (CurrentBlock != null)
            {
                bool ko = KoLeft();
                if (ko == false)
                {
                    int NbColLeftVide = StartingColLeftOffset(CurrentBlock);
                    
                    if (CurrentBlock.Col + NbColLeftVide - 1 >= 0)
                    {
                        CurrentBlock.Col -= 1;
                    }
                }
            }  
        }

        public void MoveRight()
        {
            if(CurrentBlock != null)
            {
                bool ko = KoRight();
                if (ko == false)
                {
                    int NbColvide = StartingColOffset(CurrentBlock);
                    if (CurrentBlock.Col + CurrentBlock.m - NbColvide <= columns - 1)
                    {
                        CurrentBlock.Col += 1;
                    }
                }
            }                      
        }

        public int StartingColOffset(MovableGrid mvb)
        {

            Grid shape = mvb;            
            int temp = 0;
            int NbColVide = 0;
            for (int i = mvb.m-1; i != 0; i--)
            {
                for (int j = 0; j< mvb.n; j++)
                {
                    if (shape.CellAt(j, i) != '.')
                    {                        
                        return NbColVide;
                    }
                    temp += 1;
                }
                if(temp == mvb.n)
                {
                    NbColVide += 1;
                }
                temp = 0;
            }
            return NbColVide;
        }

        public void FromString(string BoardString)
        {
            char[,] matrix = new char[rows, columns];
            int Nbligne = 0;
            int NbColonne = 0;
            foreach (char c in BoardString)
            {
                if (c == '\n')
                {
                    Nbligne++;
                    NbColonne = 0;
                }
                else
                {
                    matrix[Nbligne, NbColonne] = c;
                    NbColonne++;
                }
                
            }
            for(int i = 0; i<rows; i++)
            {
                for(int j = 0; j<columns; j++)
                {
                    if (matrix[i, j] != '.')
                    {
                        string bab = matrix[i, j].ToString();
                        Tetromino Z = new Tetromino(bab+"\n");
                        MovableGrid Ze = new MovableGrid(Z,i,j,1,1);
                        blockliste.Add(Ze);
                    }
                }
            }            
        }

        public bool KoTick()
        {
            MovableGrid CurrentMovableGrid = CurrentBlock;
            bool ko = false;
            if (blockliste.Count() > 1)
            {
                Grid CurrentGrid = CurrentBlock;
                for (int row = 0; row < CurrentMovableGrid.n; row++)
                {
                    for (int col = 0; col < CurrentMovableGrid.m; col++)
                    {
                        foreach (MovableGrid b in blockliste)
                        {

                            if (CurrentMovableGrid != b)
                            {
                                if (CurrentMovableGrid.Col + col >= b.Col &&
                                CurrentMovableGrid.Col + col <= b.Col + b.m - 1 &&
                                CurrentMovableGrid.Row + row + 1 >= b.Row &&
                                CurrentMovableGrid.Row + row + 1 <= b.Row + b.n - 1 &&
                                CurrentGrid.CellAt(row, col) != '.')
                                {

                                    Grid ForGrid = b;

                                    for (int i = 0; i < b.n; i++)
                                    {
                                        for (int j = 0; j < b.m; j++)
                                        {
                                            if (CurrentMovableGrid.Row + row + 1 == b.Row + i &&
                                                CurrentMovableGrid.Col + col == b.Col + j &&
                                                ForGrid.CellAt(i, j) != '.')
                                            {
                                                CurrentBlock = null;
                                                IsFallingBlock();
                                                ko = true;
                                                TryToRemoveRow();
                                            }
                                            if (ko == true)
                                            {
                                                break;
                                            }
                                        }
                                        if (ko == true)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            if (ko == true)
                            {
                                break;
                            }
                        }
                        if (ko == true)
                        {
                            break;
                        }
                    }
                    if (ko == true)
                    {
                        break;
                    }
                }
            }
            return ko;
        }

        public bool KoLeft()
        {
            MovableGrid CurrentMovableGrid = CurrentBlock;
            bool ko = false;
            if (blockliste.Count() > 1)
            {
                Grid CurrentGrid = CurrentBlock;
                for (int row = 0; row < CurrentMovableGrid.n; row++)
                {
                    for (int col = 0; col < CurrentMovableGrid.m; col++)
                    {
                        foreach (MovableGrid b in blockliste)
                        {
                            if (CurrentMovableGrid != b)
                            {
                                if (CurrentMovableGrid.Col -1 + col >= b.Col &&
                                CurrentMovableGrid.Col + col -1 <= b.Col + b.m - 1 &&
                                CurrentMovableGrid.Row + row >= b.Row &&
                                CurrentMovableGrid.Row + row <= b.Row + b.n - 1 &&
                                CurrentGrid.CellAt(row, col) != '.')
                                {
                                    Grid ForGrid = b;
                                    for (int i = 0; i < b.n; i++)
                                    {
                                        for (int j = 0; j < b.m; j++)
                                        {
                                            if (CurrentMovableGrid.Row + row == b.Row + i &&
                                                CurrentMovableGrid.Col + col - 1 == b.Col + j &&
                                                ForGrid.CellAt(i, j) != '.')
                                            {
                                                ko = true;
                                            }
                                            if (ko == true)
                                            {
                                                break;
                                            }
                                        }
                                        if (ko == true)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            if (ko == true)
                            {
                                break;
                            }
                        }
                        if (ko == true)
                        {
                            break;
                        }
                    }
                    if (ko == true)
                    {
                        break;
                    }
                }
            }
            return ko;
        }

        public bool KoRight()
        {
            MovableGrid CurrentMovableGrid = CurrentBlock;
            bool ko = false;
            if (blockliste.Count() > 1)
            {
                Grid CurrentGrid = CurrentBlock;
                for (int row = 0; row < CurrentMovableGrid.n; row++)
                {
                    for (int col = 0; col < CurrentMovableGrid.m; col++)
                    {
                        foreach (MovableGrid b in blockliste)
                        {
                            if (CurrentMovableGrid != b)
                            {
                                if (CurrentMovableGrid.Col + 1 + col >= b.Col &&
                                CurrentMovableGrid.Col + col + 1 <= b.Col + b.m - 1 &&
                                CurrentMovableGrid.Row + row >= b.Row &&
                                CurrentMovableGrid.Row + row <= b.Row + b.n - 1 &&
                                CurrentGrid.CellAt(row, col) != '.')
                                {
                                    Grid ForGrid = b;
                                    for (int i = 0; i < b.n; i++)
                                    {
                                        for (int j = 0; j < b.m; j++)
                                        {
                                            if (CurrentMovableGrid.Row + row == b.Row + i &&
                                                CurrentMovableGrid.Col + col + 1 == b.Col + j &&
                                                ForGrid.CellAt(i, j) != '.')
                                            {
                                                ko = true;
                                            }
                                            if (ko == true)
                                            {
                                                break;
                                            }
                                        }
                                        if (ko == true)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            if (ko == true)
                            {
                                break;
                            }
                        }
                        if (ko == true)
                        {
                            break;
                        }
                    }
                    if (ko == true)
                    {
                        break;
                    }
                }
            }
            return ko;
        }

        public void RotateRight()
        {
            if(CurrentBlock != null)
            {
                if (CurrentBlock.Row != -1)
                {
                    bool ko = KoRotateRight();                  
                    if (ko == false)
                    {
                        MovableGrid test = CurrentBlock.GridRotateRight();
                        TryRotateLeft(test);
                    }
                    
                }
            }
        }

        public void RotateLeft()
        {
            if (CurrentBlock != null)
            {
                if (CurrentBlock.Row != -1)
                {
                    bool ko = KoRotateLeft();
                    if(ko == false)
                    {                        
                        MovableGrid Test = CurrentBlock.GridRotateLeft();
                        TryRotateLeft(Test);
                    }                  
                }
            }
        }

        public bool KoRotateRight()
        {
            Tetromino T = CurrentBlock.Tetro;
            T = T.RotateRight();
            MovableGrid CurrentMovableGrid = new MovableGrid(T,CurrentBlock.Row,CurrentBlock.Col,CurrentBlock.n,CurrentBlock.m);
            bool ko = false;
            if (blockliste.Count() > 1)
            {
                Grid CurrentGrid = CurrentBlock;
                for (int row = 0; row < CurrentMovableGrid.n; row++)
                {
                    for (int col = 0; col < CurrentMovableGrid.m; col++)
                    {
                        foreach (MovableGrid b in blockliste)
                        {
                            if (CurrentBlock != b)
                            {
                                
                                if (CurrentMovableGrid.Col + col >= b.Col &&
                                CurrentMovableGrid.Col + col <= b.Col + b.m - 1 &&
                                CurrentMovableGrid.Row + row >= b.Row &&
                                CurrentMovableGrid.Row + row <= b.Row + b.n - 1 &&
                                CurrentGrid.CellAt(row, col) != '.')
                                {
                                    Grid ForGrid = b;
                                    for (int i = 0; i < b.n; i++)
                                    {
                                        for (int j = 0; j < b.m; j++)
                                        {
                                            if (CurrentMovableGrid.Row + row == b.Row + i &&
                                                CurrentMovableGrid.Col + col == b.Col + j &&
                                                ForGrid.CellAt(i, j) != '.')
                                            {
                                                ko = true;
                                            }
                                            if (ko == true)
                                            {
                                                break;
                                            }
                                        }
                                        if (ko == true)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            if (ko == true)
                            {
                                break;
                            }
                        }
                        if (ko == true)
                        {
                            break;
                        }
                    }
                    if (ko == true)
                    {
                        break;
                    }
                }
            }
            return ko;
        }

        public int StartingColLeftOffset(MovableGrid mvb)
        {
            int temp = 0;
            int NbColVide = 0;
            Grid shape = mvb;
            for (int i = 0; i<mvb.m; i++)
            {
                for (int j = 0; j < mvb.n; j++)
                {
                    if (shape.CellAt(j, i) != '.')
                    {
                        return NbColVide;
                    }
                    temp += 1;
                }
                if (temp == mvb.n)
                {
                    NbColVide += 1;
                }
                temp = 0;
            }
            return NbColVide;
        }

        public bool KoRotateLeft()
        {
            Tetromino T = CurrentBlock.Tetro;
            T = T.RotateLeft();
            MovableGrid CurrentMovableGrid = new MovableGrid(T, CurrentBlock.Row, CurrentBlock.Col, CurrentBlock.n, CurrentBlock.m);
            bool ko = false;
            if (blockliste.Count() > 1)
            {
                Grid CurrentGrid = CurrentBlock;
                for (int row = 0; row < CurrentMovableGrid.n; row++)
                {
                    for (int col = 0; col < CurrentMovableGrid.m; col++)
                    {
                        foreach (MovableGrid b in blockliste)
                        {
                            if (CurrentBlock != b)
                            {
                                if (CurrentMovableGrid.Col + col >= b.Col &&
                                CurrentMovableGrid.Col + col <= b.Col + b.m - 1 &&
                                CurrentMovableGrid.Row + row >= b.Row &&
                                CurrentMovableGrid.Row + row <= b.Row + b.n - 1 &&
                                CurrentGrid.CellAt(row, col) != '.')
                                {
                                    Grid ForGrid = b;
                                    for (int i = 0; i < b.n; i++)
                                    {
                                        for (int j = 0; j < b.m; j++)
                                        {
                                            if (CurrentMovableGrid.Row + row == b.Row + i &&
                                                CurrentMovableGrid.Col + col == b.Col + j &&
                                                ForGrid.CellAt(i, j) != '.')
                                            {
                                                ko = true;
                                            }
                                            if (ko == true)
                                            {
                                                break;
                                            }
                                        }
                                        if (ko == true)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            if (ko == true)
                            {
                                break;
                            }
                        }
                        if (ko == true)
                        {
                            break;
                        }
                    }
                    if (ko == true)
                    {
                        break;
                    }
                }
            }
            return ko;
        }

        void TryRotateLeft(MovableGrid rotated)
        {
            MovableGrid[] moves = {
            rotated ,
            rotated.MoveLeft(), // wallkick moves
            rotated.MoveRight(),
            rotated.MoveLeft().MoveLeft () ,
            rotated.MoveRight().MoveRight () ,
            };
            int temp = 0;
            foreach (MovableGrid test in moves)
            {
                temp += 1;
                if (ConflictsWithBoard(test) == false)
                {
                    bool ko = KoTryRotateLeft(test);
                    if(ko == false)
                    {
                        blockliste.Remove(CurrentBlock);
                        CurrentBlock = test;
                        CurrentBlock.IsFalling = true;
                        blockliste.Add(CurrentBlock);
                        return;
                    }   
                }
            }            
        }

        public bool ConflictsWithBoard(MovableGrid test)
        {
            Grid Test = test;
            bool ko = false;
            int NbColvide = StartingColOffset(test);
            for(int col = 0; col < test.m; col++)
            {
                for(int row = 0;row< test.n; row++)
                {
                    if(Test.CellAt(row,col)!='.' && test.Col < 0)
                    {
                        ko = true;
                    }
                    if (Test.CellAt(row, col) != '.' && test.Col + test.m -NbColvide -1 >columns-1)
                    {
                        ko = true;
                    }
                    if (ko == true)
                    {
                        break;
                    }
                }
                if (ko == true)
                {
                    break;
                }
            }
            return ko;
        }

        public bool KoTryRotateLeft(MovableGrid test)
        {
            Grid Test = test;
            bool ko = false;
            for (int col = 0; col < test.m; col++)
            {
                for (int row = 0; row < test.n; row++)
                {
                    foreach (MovableGrid b in blockliste)
                    {
                        if (CurrentBlock != b)
                        {
                            if (test.Col + col >= b.Col &&
                            test.Col + col <= b.Col + b.m - 1 &&
                            test.Row + row >= b.Row &&
                            test.Row + row <= b.Row + b.n - 1 &&
                            Test.CellAt(row, col) != '.')
                            {
                                Grid ForGrid = b;
                                for (int i = 0; i < b.n; i++)
                                {
                                    for (int j = 0; j < b.m; j++)
                                    {
                                        if (test.Row + row == b.Row + i &&
                                            test.Col + col == b.Col + j &&
                                            ForGrid.CellAt(i, j) != '.')
                                        {
                                            ko = true;
                                        }
                                        if (ko == true)
                                        {
                                            break;
                                        }
                                    }
                                    if (ko == true)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if (ko == true)
                        {
                            break;
                        }
                    }
                }
                if (ko == true)
                {
                    break;
                }
            }
            return ko;
        }
        
        public int CountRowDownOffset(MovableGrid mvb)
        {
            Grid shape = mvb;
            int temp = 0;
            int NbRowVide = 0;
            for(int i = mvb.n - 1; i != 0; i--)
            {
                for(int j = 0; j < mvb.m; j++)
                {
                    if (shape.CellAt(i, j) != '.')
                    {
                        return NbRowVide;
                    }
                    temp += 1;
                }
                if(temp == mvb.m)
                {
                    NbRowVide += 1;
                }
                temp = 0;
            }
            return NbRowVide;
        }

        public int CountRowUpOffset(MovableGrid mvb)
        {
            Grid shape = mvb;
            int temp = 0;
            int NbRowVide = 0;
            for (int i = 0; i < mvb.n; i++)
            {
                for (int j = 0; j < mvb.m; j++)
                {
                    if (shape.CellAt(i, j) != '.')
                    {
                        return NbRowVide;
                    }
                    temp += 1;
                }
                if (temp == mvb.m)
                {
                    NbRowVide += 1;
                }
                temp = 0;
            }
            return NbRowVide;
        }

        public void TryToRemoveRow()
        {
            char[,] matrix = new char[rows, columns];
            for (int a = 0; a < rows; a++)
            {
                for (int b = 0; b < columns; b++)
                {
                    matrix[a, b] = '.';
                }
            }
            foreach (MovableGrid m in blockliste)
            {
                int posX = m.Row;
                int posY = m.Col;
                for (int i = 0; i < m.n; i++)
                {
                    for (int j = 0; j < m.m; j++)
                    {
                        if (posX + i >= 0 && posY + j <= columns - 1 && posY + j >= 0)
                        {
                            if (m.matrix[i, j] != '.')
                            {
                                matrix[posX + i, posY + j] = m.matrix[i, j];
                            }
                        }
                    }
                }
            }

            int temp = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (matrix[i, j] != '.')
                    {
                        temp += 1;
                    }
                }
                if (temp == columns)
                {
                    bool ak = false;
                    foreach (MovableGrid m in blockliste)
                    {
                        int NbRowUpVide = CountRowUpOffset(m);
                        for (int h = 0; h < m.n; h++)
                        {
                            for (int f = 0; f < m.m; f++)
                            {
                                if(m.n != 1)
                                {
                                    if (m.Row + h == i)
                                    {
                                        ak = true;
                                        if (h != 0)
                                        {
                                            m.matrix[h, f] = m.matrix[h - 1, f] ;                                            
                                        }
                                    }
                                }
                                else if(m.n == 1 && m.Row == i)
                                {
                                    m.matrix[h, f] = '.';
                                }
                                
                            }
                        }
                        if (ak == true)
                        {
                            
                            Console.Write(NbRowUpVide);
                            for (int t = 0; t < m.m; t++)
                            {
                                if (NbRowUpVide < m.n)
                                {
                                    m.matrix[0 + NbRowUpVide, t] = '.';
                                }

                            }
                        }
                        for (int ans = 0; ans < blockliste.Count(); ans++)
                        {
                            foreach (MovableGrid tre in blockliste)
                            {
                                int NbRowDOwnVide = CountRowDownOffset(tre);
                                if (tre.Row + tre.n - NbRowDOwnVide - 1 < i)
                                {
                                    bool ko = KoRemoveTick(tre);
                                    if (ko == false)
                                    {
                                        tre.Row += 1;
                                        ko = KoRemoveTick(tre);
                                    }
                                }
                            }
                        }
                    }
                }
                temp = 0;
            }
        }

        public bool KoRemoveTick(MovableGrid test)
        {
            
            bool ko = false;
            if (blockliste.Count() > 1)
            {
                Grid CurrentGrid = test;
                for (int row = 0; row < test.n; row++)
                {
                    for (int col = 0; col < test.m; col++)
                    {
                        foreach (MovableGrid b in blockliste)
                        {

                            if (test != b)
                            {
                                if (test.Col + col >= b.Col &&
                                test.Col + col <= b.Col + b.m - 1 &&
                                test.Row + row + 1 >= b.Row &&
                                test.Row + row + 1 <= b.Row + b.n - 1 &&
                                CurrentGrid.CellAt(row, col) != '.')
                                {

                                    Grid ForGrid = b;

                                    for (int i = 0; i < b.n; i++)
                                    {
                                        for (int j = 0; j < b.m; j++)
                                        {
                                            if (test.Row + row + 1 == b.Row + i &&
                                                test.Col + col == b.Col + j &&
                                                ForGrid.CellAt(i, j) != '.')
                                            {                                               
                                                ko = true;
                                            }
                                            if (ko == true)
                                            {
                                                break;
                                            }
                                        }
                                        if (ko == true)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            if (ko == true)
                            {
                                break;
                            }
                        }
                        if (ko == true)
                        {
                            break;
                        }
                    }
                    if (ko == true)
                    {
                        break;
                    }
                }
            }
            return ko;
        }        
    }
}
