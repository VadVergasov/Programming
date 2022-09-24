using _153503_Verhasau_Lab1_2.Collections;

namespace _153503_Verhasau_Lab1_2.Entities {
    internal class Option {
        public Customer Customer { get; set; }
        public MyCustomCollection<Rate> Rates;

        public Option(Customer customer, MyCustomCollection<Rate> rates) {
            Customer = customer;
            Rates = rates;
        }
    }

    internal class HMS {
        public event EventHandler<HMSEventArgs>? RatesOrCustomersChanged;
        public event EventHandler<HMSEventArgs>? OptionAdded;

        private MyCustomCollection<Customer> customers;
        private MyCustomCollection<Rate> rates;
        private MyCustomCollection<Option> includedOptions;

        public HMS() {
            customers = new MyCustomCollection<Customer>();
            rates = new MyCustomCollection<Rate>();
            includedOptions = new MyCustomCollection<Option>();
        }

        public void AddCustomer(Customer customer) {
            customers.Add(customer);
            RatesOrCustomersChanged?.Invoke(this, new HMSEventArgs($"Жилец \"{customer}\" был добавлен в список жильцов"));
        }

        public void AddRate(Rate rate) {
            rates.Add(rate);
            RatesOrCustomersChanged?.Invoke(this, new HMSEventArgs($"Тариф \"{rate}\" был добавлен в список тарифов"));
        }

        public int getSum(Customer customer) {
            int sum = 0;
            for (int current = 0; current < includedOptions.Count(); current++) {
                if (includedOptions[current].Customer.Equals(customer) == true) {
                    for (int rate = 0; rate < includedOptions[current].Rates.Count(); rate++) {
                        sum += includedOptions[current].Rates[rate].Cost;
                    }
                }
            }
            return sum;
        }

        public void AddOption(Customer customer, Rate rate) {
            bool exists = false;
            foreach (Customer current in customers) {
                if (current.Equals(customer)) {
                    exists = true;
                    break;
                }
            }
            if (!exists) {
                throw new ItemNotFoundException($"Не найден клиент ({customer}), для которого добавляется тариф({rate})");
            }
            exists = false;
            foreach (Rate current in rates) {
                if (current.Equals(rate)) {
                    exists = true;
                    break;
                }
            }
            if (!exists) {
                throw new ItemNotFoundException($"Не найден тариф({rate})");
            }
            exists = false;
            for (int current = 0; current < includedOptions.Count(); current++) {
                if (includedOptions[current].Customer.Equals(customer) == true) {
                    exists = true;
                    includedOptions[current].Rates.Add(rate);
                }
            }
            if (!exists) {
                MyCustomCollection<Rate> options = new MyCustomCollection<Rate>();
                options.Add(rate);
                includedOptions.Add(new Option(customer, options));
            }
            OptionAdded?.Invoke(this, new HMSEventArgs($"Клиент \"{customer}\" добавил тариф \"{rate}\""));
        }

        public int getFullSum() {
            int sum = 0;
            for (int current = 0; current < includedOptions.Count(); current++) {
                for (int rate = 0; rate < includedOptions[current].Rates.Count(); rate++) {
                    sum += includedOptions[current].Rates[rate].Cost;
                }
            }
            return sum;
        }
    }

    internal class HMSEventArgs : EventArgs {
        public readonly string Message;
        public HMSEventArgs(string message) {
            Message = message;
        }
    }
}
