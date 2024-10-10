using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Source;

namespace Test
{
    [TestClass]
    public class Step3_StringToMatrixTest
    {
        [TestMethod]
        public void string_to_matrix()
        {
            string grid = "...\n...\n...\n";

            StringToMatrix converter = new StringToMatrix(grid);
            
            CollectionAssert.AreEqual(converter.blocks, new char[,] {
                {'.','.','.'},
                {'.','.','.'},
                {'.','.','.'}
            });
        }

        [TestMethod]
        public void matrix_to_string()
        {
            char[,] matrix = new char[,] {
                {'.','.','.'},
                {'.','.','.'},
                {'.','.','.'}
            };

            string grid = StringToMatrix.Inverse(matrix, 3, 3);

            Assert.AreEqual(grid, "...\n...\n...\n");
        }
    }

    [TestClass]
    public class Step3_FallingTetrominosTest
    {
        Board board;

        [TestInitialize]
        public void SetUp()
        {
            board = new Board(6, 8);
        }

        void RepeatTick(int times)
        {
            for (int i = 0; i < times; i++)
                board.Tick();
        }

        #region when_a_piece_is_dropped

        [TestMethod]
        public void tetromino_starts_from_top_middle()
        {
            // act
            board.Drop(Tetromino.T_SHAPE);
            // assert
            Assert.AreEqual(board.ToString(),
                "..TTT...\n" +
                "...T....\n" +
                "........\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        #endregion

        #region when_a_piece_reaches_the_bottom

        [TestMethod]
        public void piece_is_still_falling_on_the_last_row()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);

            // act
            RepeatTick(4);

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "........\n" +
                "........\n" +
                "........\n" +
                "..TTT...\n" +
                "...T....\n"
            );
            Assert.IsTrue(board.IsFallingBlock());
        }

        [TestMethod]
        public void piece_stops_when_it_hits_the_bottom()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);

            // act
            RepeatTick(5);

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "........\n" +
                "........\n" +
                "........\n" +
                "..TTT...\n" +
                "...T....\n"
            );
            Assert.IsFalse(board.IsFallingBlock());
        }

        #endregion

        #region when_a_piece_lands_on_another_piece

        [TestMethod]
        public void piece_is_still_falling_right_above_the_other_piece()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);
            RepeatTick(5);

            // act
            board.Drop(Tetromino.T_SHAPE);
            RepeatTick(2);

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "........\n" +
                "..TTT...\n" +
                "...T....\n" +
                "..TTT...\n" +
                "...T....\n"
            );
            Assert.IsTrue(board.IsFallingBlock());
        }

        [TestMethod]
        public void piece_stops_when_it_hits_the_other_piece()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);
            RepeatTick(5);

            // act
            board.Drop(Tetromino.T_SHAPE);
            RepeatTick(3);

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "........\n" +
                "..TTT...\n" +
                "...T....\n" +
                "..TTT...\n" +
                "...T....\n"
            );
            Assert.IsFalse(board.IsFallingBlock());
        }

        #endregion

    }
}
