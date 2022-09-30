namespace Lab4.Entities {
    internal class MyCustomComparer : IComparer<Passanger> {
        public int Compare(Passanger? x, Passanger? y) {
            return string.Compare(x.Name, y.Name);
        }
    }
}
