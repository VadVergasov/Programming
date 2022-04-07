using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task_1.Tests {
    [TestClass()]
    public class ATSTests {
        [TestMethod()]
        public void AddRateTest() {
            Rate[] input = { new Rate(new Client("Вергасов", "Вадим"), 10),
                new Rate(new Client("Власенко", "Тимофей"), 100) };
            ATS ats = new ATS("Гикало 9");
            for (int i = 0; i < input.Length; i++) {
                ats.AddRate(input[i]);
                Assert.AreEqual(input[i].To.Name, ats.Rates[i].To.Name);
                Assert.AreEqual(input[i].To.Surname, ats.Rates[i].To.Surname);
                Assert.AreEqual(input[i].Cost, ats.Rates[i].Cost);
            }
            ATS another = new ATS();
            another.Rates = input;
            for (int i = 0; i < input.Length; i++) {
                Assert.AreEqual(input[i].To.Name, ats.Rates[i].To.Name);
                Assert.AreEqual(input[i].To.Surname, ats.Rates[i].To.Surname);
                Assert.AreEqual(input[i].Cost, ats.Rates[i].Cost);
            }
        }

        [TestMethod()]
        public void AddClientTest() {
            Client[] input = { new Client("Вергасов", "Вадим"),
                new Client("Власенко", "Тимофей") };
            ATS ats = new ATS("Гикало 9");
            for (int i = 0; i < input.Length; i++) {
                ats.AddClient(input[i]);
                Assert.AreEqual(input[i].Name, ats.Clients[i].Name);
                Assert.AreEqual(input[i].Surname, ats.Clients[i].Surname);
            }
            ATS another = new ATS();
            another.Clients = input;
            for (int i = 0; i < input.Length; i++) {
                Assert.AreEqual(input[i].Name, ats.Clients[i].Name);
                Assert.AreEqual(input[i].Surname, ats.Clients[i].Surname);
            }
        }

        [TestMethod()]
        public void AddCallTest() {
            Call[] input = { new Call(new Client("Вергасов", "Вадим"), new Client("Власенко", "Тимофей"), 10, Call.CALLTYPE.SUCCESSFUL),
                new Call(new Client("Скворцов", "Александр"), new Client("Щиров", "Павел"), 10, Call.CALLTYPE.MISSED)};
            ATS ats = new ATS("Гикало 9");
            for (int i = 0; i < input.Length; i++) {
                ats.AddCall(input[i]);
                Assert.AreEqual(input[i].From.Name, ats.Calls[i].From.Name);
                Assert.AreEqual(input[i].From.Surname, ats.Calls[i].From.Surname);
                Assert.AreEqual(input[i].To.Name, ats.Calls[i].To.Name);
                Assert.AreEqual(input[i].To.Surname, ats.Calls[i].To.Surname);
                Assert.AreEqual(input[i].Duration, ats.Calls[i].Duration);
                Assert.AreEqual(input[i].CallType, ats.Calls[i].CallType);
            }
            ATS another = new ATS();
            another.Calls = input;
            for (int i = 0; i < input.Length; i++) {
                Assert.AreEqual(input[i].From.Name, ats.Calls[i].From.Name);
                Assert.AreEqual(input[i].From.Surname, ats.Calls[i].From.Surname);
                Assert.AreEqual(input[i].To.Name, ats.Calls[i].To.Name);
                Assert.AreEqual(input[i].To.Surname, ats.Calls[i].To.Surname);
                Assert.AreEqual(input[i].Duration, ats.Calls[i].Duration);
                Assert.AreEqual(input[i].CallType, ats.Calls[i].CallType);
            }
        }

        [TestMethod()]
        public void GetTotalCostTest() {
            Rate[] input_rates = { new Rate(new Client("Вергасов", "Вадим"), 10),
                new Rate(new Client("Власенко", "Тимофей"), 100),
                new Rate(new Client("Скворцов", "Александр"), 10),
                new Rate(new Client("Щиров", "Павел"), 10)};
            Call[] input_calls = { new Call(new Client("Вергасов", "Вадим"), new Client("Власенко", "Тимофей"), 10, Call.CALLTYPE.SUCCESSFUL),
                new Call(new Client("Скворцов", "Александр"), new Client("Щиров", "Павел"), 10, Call.CALLTYPE.MISSED)};
            ATS ats = new ATS("Гикало 9");
            ats.Rates = input_rates;
            ats.Calls = input_calls;
            Assert.AreEqual(100, ats.GetTotalCost("Вергасов"));
            Assert.AreEqual(100, ats.GetTotalCost());
            ats.AddCall(new Call(new Client("Скворцов", "Александр"), new Client("Щиров", "Павел"), 10, Call.CALLTYPE.SUCCESSFUL));
            Assert.AreEqual(110, ats.GetTotalCost());
        }
    }
}