namespace Task_1 {
    public sealed class Rombus : Square {
        private double angle;

        public double Angle {
            get => angle;
            set => angle = value;
        }

        public Rombus(long side, double angle) : base(side) {
            Console.WriteLine("Creating Rhombus!");
            Angle = angle;
        }

        public override double Area() {
            return Side * Side * Math.Sin(Angle);
        }
    }
}
