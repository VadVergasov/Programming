namespace Task_1 {
    public class ATS {
        private static ATS? intance;

        public string Address { get; set; }
        public long UserCount { get; set; }
        public Rate Rate { get; set; }

        private ATS(string Address, long UserCount, Rate Rate) {
            this.Address = Address;
            this.UserCount = UserCount;
            this.Rate = Rate;
        }

        private ATS(string Address, int UserCount, Rate Rate) {
            this.Address = Address;
            this.UserCount = UserCount;
            this.Rate = Rate;
        }

        public static ATS GetInstance(string Address = "", long UserCount = 0, Rate? Rate = null) {
            Rate = Rate ?? new Rate(0);
            if (intance == null) {
                intance = new ATS(Address, UserCount, Rate);
            }
            return intance;
        }

        public static ATS GetInstance(string Address, int UserCount, Rate Rate) {
            if (intance == null) {
                intance = new ATS(Address, UserCount, Rate);
            }
            return intance;
        }

        public long GetSum() {
            return UserCount * Rate.Cost;
        }
    }
}
