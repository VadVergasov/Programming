using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task_1.Tests {
    [TestClass()]
    public class Tests {
        [TestMethod()]
        public void PerimeterTest() {
            double[] result = { 12, 40, 35, 40 };

            Shape[] shapes = new Shape[4];
            shapes[0] = new Triangle(3, 4, 5);
            shapes[1] = new Rombus(10, System.Math.PI / 4);
            shapes[2] = new Rectangle(10, 3.0 / 4.0);
            shapes[3] = new Square(10);

            for (int i = 0; i < shapes.Length; i++) {
                Assert.AreEqual(result[i], shapes[i].Perimeter());
            }
        }

        [TestMethod()]
        public void AreaTest() {
            double[] result = { 6, 70.71067811865476, 75, 100 };

            Shape[] shapes = new Shape[4];
            shapes[0] = new Triangle(3, 4, 5);
            shapes[1] = new Rombus(10, System.Math.PI / 4);
            shapes[2] = new Rectangle(10, 3.0 / 4.0);
            shapes[3] = new Square(10);

            for (int i = 0; i < shapes.Length; i++) {
                Assert.AreEqual(result[i], shapes[i].Area());
            }
        }

        [TestMethod()]
        public void DiagonalTest() {
            double[] result = { 12.5, 10 * System.Math.Sqrt(2) };

            Square[] shapes = new Square[2];
            shapes[0] = new Rectangle(10, 3.0 / 4.0);
            shapes[1] = new Square(10);

            for (int i = 0; i < shapes.Length; i++) {
                Assert.AreEqual(result[i], shapes[i].Diagonal());
            }
        }
    }
}