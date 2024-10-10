using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Source;

namespace Test
{
    [TestClass]
    public class Step5_RotateFallingTetrominoesTest
    {
        Board board;

        [TestInitialize]
        public void SetUp()
        {
            board = new Board(6, 8);
        }

        #region when_there_is_enough_space

        [TestMethod]
        public void piece_cannot_rotate_on_the_first_row()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);

            // act
            board.RotateRight();

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

        [TestMethod]
        public void piece_can_rotate_right()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);
            board.MoveDown();

            // act
            board.RotateRight();

            // assert
            Assert.AreEqual(board.ToString(),
                "...T....\n" +
                "..TT....\n" +
                "...T....\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        [TestMethod]
        public void piece_can_rotate_left()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);
            board.MoveDown();

            // act
            board.RotateLeft();

            // assert
            Assert.AreEqual(board.ToString(),
                "...T....\n" +
                "...TT...\n" +
                "...T....\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        #endregion

        #region when_there_is_no_room_to_rotate

        [TestMethod]
        public void next_to_stationary_blocks()
        {
            //arrange
            board.FromString(
                "........\n" +
                "........\n" +
                "........\n" +
                "..Z..Z..\n" +
                "..Z..Z..\n" +
                "..Z..Z..\n"
            );
            board.Drop(Tetromino.T_SHAPE);
            board.MoveDown();
            board.RotateLeft();
            board.MoveDown();
            board.MoveDown();

            // act
            board.RotateRight();

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "........\n" +
                "...T....\n" +
                "..ZTTZ..\n" +
                "..ZT.Z..\n" +
                "..Z..Z..\n"
            );
            Assert.IsTrue(board.IsFallingBlock());
        }

        [TestMethod]
        public void next_to_left_wall()
        {
            // arrange
            board.FromString(
                "........\n" +
                "........\n" +
                "........\n" +
                "..Z..Z..\n" +
                "..Z..Z..\n" +
                "..Z..Z..\n"
            );
            board.Drop(Tetromino.T_SHAPE);
            board.MoveDown();
            board.RotateLeft();
            board.MoveLeft();
            board.MoveLeft();
            board.MoveLeft();
            board.MoveDown();
            board.MoveDown();

            // act
            board.RotateLeft();

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "........\n" +
                "T.......\n" +
                "TTZ..Z..\n" +
                "T.Z..Z..\n" +
                "..Z..Z..\n"
            );
        }

        [TestMethod]
        public void next_to_right_wall()
        {
            // arrange
            board.FromString(
               "........\n" +
               "........\n" +
               "........\n" +
               "..Z..Z..\n" +
               "..Z..Z..\n" +
               "..Z..Z..\n"
           );
            board.Drop(Tetromino.T_SHAPE);
            board.MoveDown();
            board.RotateRight();
            board.MoveRight();
            board.MoveRight();
            board.MoveRight();
            board.MoveRight();
            board.MoveDown();
            board.MoveDown();

            // act
            board.RotateRight();

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "........\n" +
                ".......T\n" +
                "..Z..ZTT\n" +
                "..Z..Z.T\n" +
                "..Z..Z..\n"
            );
        }

        #endregion

        #region when_wallkick_can_be_used_to_rotate

        [TestMethod]
        public void one_step_to_right()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);
            board.MoveDown();
            board.RotateLeft();
            board.MoveLeft();
            board.MoveLeft();
            board.MoveLeft();

            // act
            board.RotateLeft();

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                ".T......\n" +
                "TTT.....\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        [TestMethod]
        public void two_steps_to_right()
        {
            // arrange
            board.Drop(Tetromino.I_SHAPE);
            board.MoveDown();
            board.RotateLeft();
            board.MoveLeft();
            board.MoveLeft();
            board.MoveLeft();
            board.MoveLeft();

            // act
            board.RotateLeft();

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "IIII....\n" +
                "........\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        [TestMethod]
        public void one_step_to_left()
        {
            // arrange
            board.Drop(Tetromino.T_SHAPE);
            board.MoveDown();
            board.RotateRight();
            board.MoveRight();
            board.MoveRight();
            board.MoveRight();
            board.MoveRight();

            // act
            board.RotateRight();

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "......T.\n" +
                ".....TTT\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        [TestMethod]
        public void two_steps_to_left()
        {
            // arrange
            board.Drop(Tetromino.I_SHAPE);
            board.MoveDown();
            board.RotateRight();
            board.MoveRight();
            board.MoveRight();
            board.MoveRight();

            // act
            board.RotateRight();

            // assert
            Assert.AreEqual(board.ToString(),
                "........\n" +
                "....IIII\n" +
                "........\n" +
                "........\n" +
                "........\n" +
                "........\n"
            );
        }

        #endregion
    }
}
