namespace Task_1 {
    public class Rectangle : Square {
        private double ratio;

        public double Ratio {
            get => ratio;
            set {
                if (ratio < 0) {
                    ratio = 1;
                } else { ratio = value; }
            }
        }

        public Rectangle(long side, double ratio) : base(side) {
            Console.WriteLine("Creating Rectangle!");
            Ratio = ratio;
        }

        public override double Area() {
            return Side * Side * Ratio;
        }

        public override double Perimeter() {
            return 2 * (Side * Ratio + Side);
        }

        public override double Diagonal() {
            return Math.Sqrt(Side * Side + Side * Side * Ratio * Ratio);
        }
    }
}
