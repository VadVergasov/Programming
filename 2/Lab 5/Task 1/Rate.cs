namespace Task_1 {
    public class Rate {
        private Client to;
        private long? cost;

        public Rate(Client to, long? cost) {
            this.to = to;
            this.cost = cost;
        }

        public Rate() {
            cost = null;
            to = new Client();
        }

        public Client To {
            get => to;
            set => to = value;
        }
        public long? Cost {
            get => cost;
            set {
                if (cost < 0) {
                    cost = 0;
                } else {
                    cost = value;
                }
            }
        }
    }
}
