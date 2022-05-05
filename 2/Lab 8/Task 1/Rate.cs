namespace Task_1 {
    public interface IRate {
        public double Price { get; set; }

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
        private double price;
        private double discount;

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
            Price = 0;
            Discount = 0;
        }

        public DiscountRate(double price, double discount = 0) {
            Price = price;
            Discount = discount;
        }

        public double GetPrice() {
            return price - price * discount / 100;
        }
    }
}
