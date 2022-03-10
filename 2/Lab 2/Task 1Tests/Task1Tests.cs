using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {
    [TestClass()]
    public class Task1Tests {
        [TestMethod()]
        public void CanBeDevidedTest() {
            Assert.AreEqual(true, Task1.CanBeDevided(10, 1));
            Assert.AreEqual(false, Task1.CanBeDevided(10, 3));
        }
        [TestMethod()]
        public void Devide() {
            Assert.AreEqual(3, Task1.Devide(9, 3));
            Assert.AreEqual(1, Task1.Devide(1, 1));
            Assert.AreEqual(0, Task1.Devide(0, 11212312321));
        }
    }
}