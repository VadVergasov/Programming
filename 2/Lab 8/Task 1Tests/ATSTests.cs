using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task_1.Tests {
    [TestClass()]
    public class ATSTests {
        [TestMethod()]
        public void ATSTest() {
            ATS ats = new ATS();
            Assert.AreEqual(0, ats.Rates.Length);
            ats.AddDiscountRate(100, 10);
            Assert.AreEqual(1, ats.Rates.Length);
            ats.AddRegularRate(100);
            Assert.AreEqual(2, ats.Rates.Length);
        }

        [TestMethod()]
        public void AveragePriceTest() {
            ATS ats = new ATS();
            ats.AddDiscountRate(100, 10);
            ats.AddRegularRate(100);
            Assert.AreEqual(95, ats.AveragePrice());
            ats.AddDiscountRate(100, 100);
            Assert.AreEqual(190.0 / 3.0, ats.AveragePrice());
            ats.AddDiscountRate(new RegularRate(-100), 99);
            Assert.AreEqual(190.0 / 4.0, ats.AveragePrice());
        }
    }
}