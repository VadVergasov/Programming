using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_2.Services;

namespace Task_2.Services.Tests {
    [TestClass()]
    public class Task_2Tests {
        [TestMethod()]
        public void calcTest() {
            double[] results = { 5.87880433586595, 22028.81712466395, 2.7558674953957674 };
            long[] input = { -5, 10, -1 };

            for (int i = 0; i < input.Length; i++) {
                Assert.AreEqual(results[i], Task_2.calc(input[i]));
            }
        }
    }
}