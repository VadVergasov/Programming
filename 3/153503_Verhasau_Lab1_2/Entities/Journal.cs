namespace _153503_Verhasau_Lab1_2.Entities {
    internal class Journal {
        private List<string> _registeredEvents = new();

        public void RecordChangesInOrdersOrClientsList(object? sender, HMSEventArgs e) {
            {
                _registeredEvents.Add($"Событие #{_registeredEvents.Count + 1}:{Environment.NewLine}Сообщение: {e.Message} из {sender?.GetType().Name}\n");
            }
        }

        public void PrintRegisteredEvents() {
            Console.WriteLine(string.Join(Environment.NewLine, _registeredEvents));
        }
    }
}
