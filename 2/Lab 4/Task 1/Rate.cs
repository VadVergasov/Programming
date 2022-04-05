namespace Task_1 {
    public class Rate {
        private long cost;

        public long Cost {
            get { return cost; }
            set {
                if (value < 0) {
                    cost = 0;
                } else {
                    cost = value;
                }
            }
        }

        public Rate(long cost = 0) {
            Cost = cost;
        }
    }
}
