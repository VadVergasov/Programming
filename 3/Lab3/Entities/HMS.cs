using System.Linq;

namespace Lab3.Entities {
    internal class HMS {
        public event EventHandler<HMSEventArgs>? RatesOrCustomersChanged;
        public event EventHandler<HMSEventArgs>? CustomerRatesChanged;

        public List<Customer> customers;
        public Dictionary<string, Rate> rates;

        public HMS() {
            customers = new List<Customer>();
            rates = new Dictionary<string, Rate>();
        }

        public void AddCustomer(Customer customer) {
            customers.Add(customer);
            RatesOrCustomersChanged?.Invoke(this, new HMSEventArgs($"Жилец \"{customer}\" был добавлен в список жильцов"));
        }

        public void RemoveCustomer(Customer customer) {
            customers.Remove(customer);
            RatesOrCustomersChanged?.Invoke(this, new HMSEventArgs($"Жилец \"{customer}\" был удален из списка жильцов"));
        }

        public void AddRate(Rate rate) {
            rates.Add(rate.Name, rate);
            RatesOrCustomersChanged?.Invoke(this, new HMSEventArgs($"Тариф \"{rate}\" был добавлен в список тарифов"));
        }

        public void RemoveRate(Rate rate) {
            rates.Remove(rate.Name);
            RatesOrCustomersChanged?.Invoke(this, new HMSEventArgs($"Тариф \"{rate}\" был удален из списка тарифов"));
        }

        public void AddOption(Customer customer, Rate rate) {
            if (!customers.Contains(customer)) {
                throw new KeyNotFoundException();
            }
            if (!rates.ContainsKey(rate.Name) || !rates[rate.Name].Equals(rate)) {
                throw new KeyNotFoundException();
            }
            foreach(var current in customers) {
                if(current.Equals(customer)) {
                    current.AddRate(rate);
                    break;
                }
            }
        }

        public IEnumerable<string> GetRatesByCost() {
            return from rate in rates orderby rate.Value.Cost select rate.Key;
        }

        public int GetFullSum() {
            return (from customer in customers select customer.Rates.Sum(rate => rate.Cost)).Sum();
        }

        public int GetCustomerSum(Customer customer) {
            return (from current in customers where current.Equals(customer) select current.Rates.Sum(rate => rate.Cost)).Single();
        }

        public string GetCustomerByCost() {
            return (from customer in customers orderby customer.Rates.Sum(rate => rate.Cost) select customer.Name).Last();
        }

        public int GetCustomersCountWithSumMore(int sum) {
            return customers.Aggregate(0, (result, customer) => result + (customer.Rates.Sum(rate => rate.Cost) > sum ? 1 : 0));
        }

        public List<(string, int)> GetCustomerListSum(Customer customer) {
            return (from current in customers where current.Equals(customer) select current.Rates).Single().GroupBy(rate => rate.Name).Select(group => (group.Key, group.Sum(rate => rate.Cost))).ToList();
        }
    }

    internal class HMSEventArgs : EventArgs {
        public readonly string Message;
        public HMSEventArgs(string message) {
            Message = message;
        }
    }
}
