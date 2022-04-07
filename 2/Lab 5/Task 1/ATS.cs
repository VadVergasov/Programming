namespace Task_1 {
    public class ATS {
        private string? address;

        private Rate[] rates;
        private Client[] clients;
        private Call[] calls;

        public Rate[] Rates {
            get => rates;
            set => rates = value;
        }

        public Client[] Clients {
            get => clients;
            set => clients = value;
        }

        public Call[] Calls {
            get => calls;
            set => calls = value;
        }

        public string? Address {
            get => address;
            set => address = value;
        }

        public ATS() {
            Rates = Array.Empty<Rate>();
            Clients = Array.Empty<Client>();
            Calls = Array.Empty<Call>();
            Address = null;
        }

        public ATS(string address) {
            Rates = Array.Empty<Rate>();
            Clients = Array.Empty<Client>();
            Calls = Array.Empty<Call>();
            Address = address;
        }

        public void AddRate(Rate rate) {
            Array.Resize(ref rates, rates.Length + 1);
            rates[^1] = rate;
        }

        public void AddClient(Client client) {
            Array.Resize(ref clients, clients!.Length + 1);
            clients[^1] = client;
        }

        public void AddCall(Call call) {
            Array.Resize(ref calls, calls!.Length + 1);
            calls[^1] = call;
        }

        public void AddRate(Rate[] rates) {
            Rates = rates;
        }

        public void AddClient(Client[] clients) {
            Clients = clients;
        }

        public void AddCall(Call[] calls) {
            Calls = calls;
        }

        public long? GetTotalCost(string surname) {
            long? totalCost = 0;
            foreach (var call in calls) {
                if (call.From.Surname == surname && call.CallType == Call.CALLTYPE.SUCCESSFUL) {
                    foreach (var rate in rates) {
                        if (call.To.Surname == rate.To.Surname && call.To.Name == rate.To.Name) {
                            totalCost += rate.Cost;
                        }
                    }
                }
            }
            return totalCost;
        }

        public long? GetTotalCost() {
            long? totalCost = 0;
            foreach (var call in calls) {
                if (call.CallType == Call.CALLTYPE.SUCCESSFUL) {
                    foreach (var rate in rates) {
                        if (call.To.Surname == rate.To.Surname && call.To.Name == rate.To.Name) {
                            totalCost += rate.Cost;
                        }
                    }
                }
            }
            return totalCost;
        }
    }
}
