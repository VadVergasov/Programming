using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task_1.Tests {
    [TestClass()]
    public class ATSTests {
        [TestMethod()]
        public void GetInstanceTest() {
            ATS instance = ATS.GetInstance("Гикало 9", 100, new Rate(150));
            Assert.AreEqual(ATS.GetInstance(), instance);
        }

        [TestMethod()]
        public void GetSumTest() {
            ATS instance = ATS.GetInstance("Гикало 9", 100, new Rate(150));
            Assert.AreEqual(100 * 150, instance.GetSum());
            instance.Rate = new Rate(200);
            Assert.AreEqual(100 * 200, instance.GetSum());
            instance.Rate = new Rate(-100);
            Assert.AreEqual(0, instance.GetSum());
        }
    }
}
