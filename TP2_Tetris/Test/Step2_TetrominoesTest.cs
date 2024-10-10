using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Source;

namespace Test
{
    [TestClass]
    public class Step2_TetrominoesTest
    {

        #region T_SHAPE

        Tetromino CreateTshape()
        {
            return new Tetromino(
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
        }

        [TestMethod]
        public void T_shape_first_orientation()
        {
            // act
            Tetromino T_SHAPE = CreateTshape();

            // assert
            Assert.AreEqual(T_SHAPE.ToString(),
                "....\n" +
                "TTT.\n" +
                ".T..\n"
            );
        }

        [TestMethod]
        public void T_shape_rotated_right()
        {
            // arrange
            Tetromino T_SHAPE = CreateTshape();

            // act
            Tetromino shape = T_SHAPE.RotateRight();

            // assert
            Assert.AreEqual(shape.ToString(),
                ".T..\n" +
                "TT..\n" +
                ".T..\n"
            );
        }

        [TestMethod]
        public void T_shape_rotated_left()
        {
            // arrange
            Tetromino T_SHAPE = CreateTshape();

            // act
            Tetromino shape = T_SHAPE.RotateLeft();

            // assert
            Assert.AreEqual(shape.ToString(),
                ".T..\n" +
                ".TT.\n" +
                ".T..\n"
            );
        }

        [TestMethod]
        public void T_shape_looped_around_right()
        {
            // arrange
            Tetromino T_SHAPE = CreateTshape();

            // act
            Tetromino shape = T_SHAPE;
            shape = shape.RotateRight();
            shape = shape.RotateRight();
            shape = shape.RotateRight();
            shape = shape.RotateRight();
            // assert
            Assert.AreEqual(shape.ToString(), T_SHAPE.ToString());
        }

        [TestMethod]
        public void T_shape_looped_around_left()
        {
            // arrange
            Tetromino T_SHAPE = CreateTshape();

            // act
            Tetromino shape = T_SHAPE;
            shape = shape.RotateLeft();
            shape = shape.RotateLeft();
            shape = shape.RotateLeft();
            shape = shape.RotateLeft();

            // assert
            Assert.AreEqual(shape.ToString(), T_SHAPE.ToString());
        }

        #endregion

        #region immutable

        [TestMethod]
        public void tetrominoes_are_immutable()
        {
            // arrange
            Tetromino shape = CreateTshape();
            String original = shape.ToString();

            // act
            shape.RotateRight();
            // assert
            Assert.AreEqual(original, shape.ToString());

            // act
            shape.RotateLeft();

            // assert
            Assert.AreEqual(original, shape.ToString());
        }

        #endregion
    }
}
