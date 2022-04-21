namespace Task_1 {
    public class Triangle : Shape {
        private readonly double[] side = new double[3];

        protected double HalfPerimeter() {
            return Perimeter() / 2;
        }

        public override double Perimeter() {
            return side[0] + side[1] + side[2];
        }

        public override double Area() {
            return Math.Sqrt(HalfPerimeter() * (HalfPerimeter() - side[0]) * (HalfPerimeter() - side[1]) * (HalfPerimeter() - side[2]));
        }

        public Triangle(double a, double b, double c) : base(3) {
            Console.WriteLine("Creating Triangle!");
            side[0] = a;
            side[1] = b;
            side[2] = c;
        }
    }
}
