namespace Task_1 {
    public class Task1 {
        public static long min(long a, long b) {
            return a < b ? a : b;
        }

        public static long calc(long x, long y) {
            return min(x, 2 * y + x) - min(7 * x + 2 * y, y);
        }
    }
}
