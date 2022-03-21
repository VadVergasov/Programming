namespace Task_2.Services {
    public class Task_2 {
        public static double calc(long z) {
            double x = Math.Abs(z);
            if (z < -1) {
                Console.WriteLine("z < -1");
                x = -z / 3.0;
            } else {
                Console.WriteLine("z >= -1");
            }
            return Math.Log(x + 0.5) + (Math.Exp(x) - Math.Exp(-x));
        }
    }
}
