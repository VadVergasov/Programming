using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_1;

namespace Task_1.Tests {
    [TestClass()]
    public class Task1Tests {
        [TestMethod()]
        public void minTest() {
            long[] results = {
                10,
                20,
                5,
                -10,
                -20,
                0
            };
            long[][] input = {
                new long[] { 10, 20 },
                new long[] { 20, 30 },
                new long[] { 5, 10 },
                new long[] { 5, -10 },
                new long[] {-10, -20},
                new long[] { 0, 0 },
            };

            for (int i = 0; i < results.Length; i++) {
                Assert.AreEqual(results[i], Task1.min(input[i][0], input[i][1]));
            }
        }

        [TestMethod()]
        public void calcTest() {
            long[] results = {
                -10,
                -10,
                -5,
                -5,
                60,
                0
            };
            long[][] input = {
                new long[] { 10, 20 },
                new long[] { 20, 30 },
                new long[] { 5, 10 },
                new long[] { 5, -10 },
                new long[] {-10, -20},
                new long[] { 0, 0 },
            };

            for (int i = 0; i < results.Length; i++) {
                Assert.AreEqual(results[i], Task1.calc(input[i][0], input[i][1]));
            }
        }
    }
}