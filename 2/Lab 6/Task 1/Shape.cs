namespace Task_1 {
    public abstract class Shape {
        private int verticles_count;

        public abstract double Area();
        public abstract double Perimeter();

        public int Verticles_count {
            get {
                return verticles_count;
            }
            set {
                if (value < 3) {
                    throw new ArgumentException("Количество вершин должно быть больше 2");
                }
                verticles_count = value;
            }
        }

        public Shape(int verticles_count) {
            Console.WriteLine("Creating Shape!\n");
            Verticles_count = verticles_count;
        }
    }
}
