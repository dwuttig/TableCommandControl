using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TableCommandControl.Collections {
    public class ObservableQueue<T> :Collection<T>, INotifyPropertyChanged, INotifyCollectionChanged {
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
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<T>() { obj }));
            T overflow;
            while (_queue.Count > Size && _queue.TryDequeue(out overflow)) {
                OnPropertyChanged("Count");
                OnPropertyChanged("Queue");
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new List<T>() { overflow }));
            }
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            CollectionChanged?.Invoke(this, e);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}