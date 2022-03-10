using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {
    [TestClass()]
    public class Task2Tests {
        [TestMethod()]
        public void LocationTest() {
            Assert.AreEqual("Да", Task2.Location(-1, 5));
            Assert.AreEqual("На границе", Task2.Location(0, 8));
            Assert.AreEqual("Нет", Task2.Location(100, 100));
            Assert.AreEqual("Нет", Task2.Location(1, 5));
        }
    }
}