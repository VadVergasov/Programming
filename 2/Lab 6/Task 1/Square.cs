namespace Task_1 {
    public class Square : Shape {
        private long side;

        public long Side {
            get => side;
            set {
                if (side < 0) {
                    side = 0;
                } else {
                    side = value;
                }
            }
        }

        public Square(long side) : base(4) {
            Console.WriteLine("Creating Square!");
            Side = side;
        }

        public override double Area() {
            return Side * Side;
        }

        public override double Perimeter() {
            return Side * 4;
        }

        public virtual double Diagonal() {
            return Math.Sqrt(Side * Side + Side * Side);
        }
    }
}
