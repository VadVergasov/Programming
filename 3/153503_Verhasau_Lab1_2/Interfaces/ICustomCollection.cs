namespace _153503_Verhasau_Lab1_2.Interfaces {
    interface ICustomCollection<T> {
        public T this [int index] { get; set; }
        
        public void Reset();
        
        public void Next();
        
        public T Current();

        public int Count();

        public void Add(T item);
        
        public void Remove(T item);
        
        public T RemoveCurrent();
    }
}
