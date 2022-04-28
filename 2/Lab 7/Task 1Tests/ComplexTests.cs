using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task_1.Tests {
    [TestClass()]
    public class ComplexTests {
        [TestMethod()]
        public void ComplexTestConstructor() {
            Complex complex = new Complex(1, 2);
            Complex empty = new Complex();
            Assert.AreEqual(0, empty.Real);
            Assert.AreEqual(0, empty.Imaginary);
            Assert.AreEqual(1, complex.Real);
            Assert.AreEqual(2, complex.Imaginary);
        }

        [TestMethod()]
        public void ComplexTestToString() {
            Complex complex = new Complex(1, 2);
            Assert.AreEqual("1 + i * 2", complex.ToString());
        }

        [TestMethod()]
        public void ComplexTestOperations() {
            Complex[] input = { new Complex(1, 2), new Complex(3, 4), new Complex(5, 6) };
            Complex[,] expected = { { new Complex(2, 4), new Complex(), new Complex(-3, 4), new Complex(1, 0) },
                { new Complex(6, 8), new Complex(), new Complex(-7, 24), new Complex(1, 0) },
                { new Complex(10, 12), new Complex(), new Complex(-11, 60), new Complex(1, 0) } };
            double[] expected_abs = { System.Math.Sqrt(1 * 1 + 2 * 2), System.Math.Sqrt(3 * 3 + 4 * 4), System.Math.Sqrt(5 * 5 + 6 * 6) };

            for (int i = 0; i < input.Length; i++) {
                Assert.AreEqual(expected[i, 0], input[i] + input[i]);
                Assert.AreEqual(expected[i, 1], input[i] - input[i]);
                Assert.AreEqual(expected[i, 2], input[i] * input[i]);
                Assert.AreEqual(expected[i, 3], input[i] / input[i]);
            }

            for (int i = 0; i < input.Length; i++) {
                Assert.AreEqual(true, input[i] == input[i]);
                Assert.AreEqual(expected_abs[i], (double)input[i]);
                Assert.AreEqual(new Complex(10, 0), 10.0);
                for (int j = i + 1; j < input.Length; j++) {
                    Assert.AreEqual(true, input[i] != input[j]);
                    Assert.AreEqual(true, input[i] < input[j]);
                    Assert.AreEqual(true, input[i] <= input[j]);
                    Assert.AreEqual(true, input[j] > input[i]);
                    Assert.AreEqual(true, input[j] >= input[i]);
                }
            }

            if (new Complex()) {
                Assert.Fail();
            }
            if (input[0]) {
            } else {
                Assert.Fail();
            }
        }
    }
}