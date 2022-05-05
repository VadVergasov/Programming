namespace Task_1 {
    public class ATS {
        private IRate[] rates;

        public IRate this[int index] {
            get => rates[index];
            set => rates[index] = value ?? throw new ArgumentNullException("Null value passed");
        }

        public IRate[] Rates { get; set; }

        public ATS() {
            Rates = Array.Empty<IRate>();
        }

        public void AddRegularRate(double price) {
            IRate rate = new RegularRate(price);
            Rates = Rates.Append(rate).ToArray();
        }

        public void AddDiscountRate(double price, double percent) {
            IRate rate = new DiscountRate(price, percent);
            Rates = Rates.Append(rate).ToArray();
        }

        public void AddDiscountRate(IRate rate, double percent) {
            IRate newRate = new DiscountRate(rate, percent);
            Rates = Rates.Append(newRate).ToArray();
        }

        public double AveragePrice() {
            double sum = 0;
            foreach (IRate rate in Rates) {
                sum += rate.GetPrice();
            }
            return sum / Rates.Length;
        }
    }
}
