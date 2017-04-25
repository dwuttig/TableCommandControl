using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TableCommandControl.Collections {
    public class ObservableQueue<T> : IEnumerable<T>, INotifyPropertyChanged, INotifyCollectionChanged {
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();

        /// <summary>Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.</summary>
        public ObservableQueue(int size) {
            Size = size;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Liefert die Liste der Items
        /// </summary>
        public ConcurrentQueue<T> Queue {
            get { return _queue; }
        }

        /// <summary>
        ///     Liefert oder setzt die maximale Anzahl der Elemente in der Queue
        /// </summary>
        public int Size { get; set; }

        public void Enqueue(T obj) {
            _queue.Enqueue(obj);
            OnPropertyChanged("Count");
            OnPropertyChanged("Queue");
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, obj));
            T overflow;
            while (_queue.Count > Size && _queue.TryDequeue(out overflow)) {
                OnPropertyChanged("Count");
                OnPropertyChanged("Queue");
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, overflow, 0));
            }
        }

        /// <summary>Gibt einen Enumerator zurück, der die Auflistung durchläuft.</summary>
        /// <returns>
        ///     Ein <see cref="T:System.Collections.Generic.IEnumerator`1" />, der zum Durchlaufen der Auflistung verwendet
        ///     werden kann.
        /// </returns>
        public IEnumerator<T> GetEnumerator() {
            return _queue.GetEnumerator();
        }

        /// <summary>Gibt einen Enumerator zurück, der eine Auflistung durchläuft.</summary>
        /// <returns>
        ///     Ein <see cref="T:System.Collections.IEnumerator" />-Objekt, das zum Durchlaufen der Auflistung verwendet
        ///     werden kann.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            CollectionChanged?.Invoke(this, e);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}