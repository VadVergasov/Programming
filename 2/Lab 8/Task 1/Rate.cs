namespace Task_1 {
    public interface IRate {
        public double GetPrice();
    }

    public class RegularRate : IRate {
        private double price;

        public double Price {
            set {
                if (value < 0) {
                    price = 0;
                } else {
                    price = value;
                }
            }
            get => price;
        }

        public RegularRate() {
            Price = 0;
        }

        public RegularRate(double price) {
            Price = price;
        }

        public double GetPrice() {
            return price;
        }
    }

    public class DiscountRate : IRate {
        private IRate rate;
        private double discount;

        public IRate Rate {
            set => rate = value;
            get => rate;
        }

        public double Discount {
            set {
                if (value < 0 || value > 100) {
                    discount = 0;
                }else {
                    discount = value;
                }
            }
            get => discount;
        }

        public DiscountRate() {
            Rate = new RegularRate();
            Discount = 0;
        }

        public DiscountRate(double price, double discount = 0) {
            Rate = new RegularRate(price);
            Discount = discount;
        }

        public DiscountRate(IRate rate, double discount = 0) {
            Rate = rate;
            Discount = discount;
        }

        public double GetPrice() {
            return Rate.GetPrice() - Rate.GetPrice() * discount / 100;
        }
    }
}
