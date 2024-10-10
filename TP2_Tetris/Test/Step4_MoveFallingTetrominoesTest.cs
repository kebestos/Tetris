using Microsoft.VisualStudio.TestTools.UnitTesting;
using Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class Step4_MoveFallingTetrominoesTest
    {
        Board board;

        [TestInitialize]
        public void SetUp()
        {
            board = new Board(6, 8);
        }

        #region a_falling_piece

        [TestMethod]
        public void piece_can_be_moved_down()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);

            // act
            board.MoveDown();

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "..TTT...\n" +
                "...T....\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        [TestMethod]
        public void piece_can_be_moved_left()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);

            // act
            board.MoveLeft();

            // assert
            Assert.AreEqual(board.ToString(),
                ".TTT....\n" +
                "..T.....\n" +
                "........\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        [TestMethod]
        public void piece_can_be_moved_right()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);

            // act
            board.MoveRight();

            // assert
            Assert.AreEqual(board.ToString(),
                "...TTT..\n" +
                "....T...\n" +
                "........\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        [TestMethod]
        public void piece_cannot_be_moved_left_outside_the_board()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);

            // act
            for (int i = 0; i < 10; i++)
                board.MoveLeft();

            // assert
            Assert.AreEqual(board.ToString(),
                "TTT.....\n" +
                ".T......\n" +
                "........\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        [TestMethod]
        public void piece_cannot_be_moved_right_outside_the_board()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);

            // act
            for (int i = 0; i < 10; i++)
                board.MoveRight();

            // assert
            Assert.AreEqual(board.ToString(),
                ".....TTT\n" +
                "......T.\n" +
                "........\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        [TestMethod]
        public void piece_cannot_be_moved_down_outside_the_board()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);

            // act
            for (int i = 0; i < 10; i++)
                board.MoveDown();

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "........\n" +
                "........\n" +
                "........\n" +
                "..TTT...\n" +
                "...T....\n"
            );
        }

        #endregion

        #region when_there_are_blocks_in_the_way

        [TestMethod]
        public void piece_cannot_be_moved_left_over_other_blocks()
        {
            //arrange
            board.FromString(
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z.ZZZZ.Z\n"
            );
            board.Drop(Tetromino.T_SHAPE);

            // act
            for (int i = 0; i < 10; i++)
                board.MoveLeft();

            // assert
            Assert.AreEqual(board.ToString(),
                "ZTTT...Z\n" +
                "Z.T....Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z.ZZZZ.Z\n"
            );
        }

        [TestMethod]
        public void piece_cannot_be_moved_right_over_other_blocks()
        {
            //arrange
            board.FromString(
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z.ZZZZ.Z\n"
            );
            board.Drop(Tetromino.T_SHAPE);

            // act
            for (int i = 0; i < 10; i++)
                board.MoveRight();

            // assert
            Assert.AreEqual(board.ToString(),
                "Z...TTTZ\n" +
                "Z....T.Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z.ZZZZ.Z\n"
            );
        }

        [TestMethod]
        public void piece_cannot_be_moved_down_over_other_blocks()
        {
            //arrange
            board.FromString(
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z.ZZZZ.Z\n"
            );
            board.Drop(Tetromino.T_SHAPE);

            // act
            for (int i = 0; i < 10; i++)
                board.MoveDown();

            // assert
            Assert.AreEqual(board.ToString(),
                "Z......Z\n" +
                "Z......Z\n" +
                "Z......Z\n" +
                "Z.TTT..Z\n" +
                "Z..T...Z\n" +
                "Z.ZZZZ.Z\n"
            );
        }

        #endregion
    }
}
