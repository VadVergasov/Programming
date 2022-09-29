namespace Lab3.Entities {
    internal class Journal {
        private List<string> _registeredEvents = new();

        public void RecordChangesInRatesOrCustomers(object? sender, HMSEventArgs e) {
            {
                _registeredEvents.Add($"Событие #{_registeredEvents.Count + 1}:{Environment.NewLine}Сообщение: {e.Message} из {sender?.GetType().Name}\n");
            }
        }

        public void RecordChangesInCustomerRates(object? sender, HMSEventArgs e) {
            {
                _registeredEvents.Add($"Сообщение: {e.Message}. От {sender?.GetType().Name}\n");
            }
        }

        public void PrintRegisteredEvents() {
            Console.WriteLine(string.Join(Environment.NewLine, _registeredEvents));
        }
    }
}
