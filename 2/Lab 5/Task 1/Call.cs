namespace Task_1 {
    public class Call {
        public enum CALLTYPE {
            SUCCESSFUL,
            MISSED
        }

        private Client from, to;
        private long duration;
        private CALLTYPE call_type;

        public Call(Client from, Client to, long duration, CALLTYPE type) {
            From = from;
            To = to;
            Duration = duration;
            CallType = type;
        }

        public Client From {
            get => from;
            set => from = value;
        }

        public Client To {
            get => to;
            set => to = value;
        }

        public long Duration {
            get => duration;
            set => duration = value;
        }

        public CALLTYPE CallType {
            get => call_type;
            set => call_type = value;
        }
    }
}
