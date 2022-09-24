using _153503_Verhasau_Lab1_2.Interfaces;
using System.Collections;

namespace _153503_Verhasau_Lab1_2.Collections {
    class ItemNotFoundException : Exception {
        public ItemNotFoundException() { }

        public ItemNotFoundException(string message) : base(message) { }

        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    class Node<T> {
        public T? Value { get; set; }
        public Node<T>? Next { get; set; }
        public Node<T>? Prev { get; set; }

        public Node() {
            Next = null;
            Prev = null;
        }
    }

    class MyCustomCollection<T> : ICustomCollection<T>, IEnumerable<T> {
        private Node<T>? _head, _current;
        private int _size;

        public MyCustomCollection() {
            _head = new Node<T>();
            _current = _head;
            _size = 0;
        }

        public T this[int index] {
            get {
                if (index < 0 || index >= _size) {
                    throw new IndexOutOfRangeException();
                }
                Node<T> node = _head!;
                for (int cur = 0; cur < index; cur++) {
                    node = node.Next!;
                }
                return node.Value!;
            }
            set {
                Node<T> node = _head!;
                for (int cur = 0; cur < index; cur++) {
                    node = node.Next!;
                }
                node.Value = value;
            }
        }

        public void Reset() {
            _current = _head;
            _size = 0;
        }

        public void Next() {
            _current = _current!.Next;
        }

        public T Current() {
            return _current!.Value!;
        }

        public int Count() { return _size; }

        public void Add(T item) {
            if (_current!.Value == null) {
                _current!.Value = item;
            } else {
                _current!.Next = new Node<T>();
                _current.Next.Prev = _current;
                _current = _current.Next;
                _current.Value = item;
            }
            _size++;
        }

        public void Remove(T item) {
            Node<T> current = _head!;
            while (current != _current!.Next && current.Value!.Equals(item) == false) {
                current = current.Next!;
            }
            if (current == _current.Next) {
                throw new ItemNotFoundException($"Элемент ({item}), который удаляется не сущесвтует");
            }
            _size--;
        }

        public T RemoveCurrent() {
            Node<T> node = _current!;
            _current!.Prev!.Next = _current!.Next;
            node.Next!.Prev = node.Prev;
            _size--;
            return node.Value!;
        }

        public IEnumerator<T>? GetEnumerator() {
            Node<T> current = _head;
            while (current != null) {
                yield return current!.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
