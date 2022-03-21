using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_3;

namespace Task_3.Tests {
    [TestClass()]
    public class DateServiceTests {
        [TestMethod()]
        public void GetDayTest() {
            string[] input = { "21.03.2022", "01.01.0001", "12.01.1234", "29.02.2004", "31.03.2020" };
            string[] results = { "Monday", "Monday", "Thursday", "Sunday", "Tuesday" };

            for (int i = 0; i < input.Length; i++) {
                Assert.AreEqual(results[i], DateService.GetDay(input[i]));
            }
        }

        [TestMethod()]
        public void GetDaysSpanTest() {
            int[][] input = { new int[] { 12, 06, 2003 }, new int[] { 1, 1, 1}, new int[] { 1, 1, 2030} };
            long[] results = { 6857, 738234, -2843 };

            for (int i = 0; i < input.Length; i++) {
                Assert.AreEqual(results[i], DateService.GetDaysSpan(input[i][0], input[i][1], input[i][2]));
            }
        }
    }
}
