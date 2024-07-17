using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace boshphelm.Utility
{
    public interface IObservableArray<T>
    {
        event Action<T[]> AnyValueChanged;

        int Count { get; }
        T this[int index] { get; }

        void Swap(int index1, int index2);
        void Clear();
        bool TryAdd(T item);
        bool TryAddAt(T item, int index);
        bool TryRemove(T item);
        bool TryRemoveAt(int index);
    }

    public class ObservableArray<T> : IObservableArray<T>
    {
        public T[] items;

        public event Action<T[]> AnyValueChanged = delegate { };
        public int Count => items.Count(i => i != null);

        public T this[int index] => items[index];

        public ObservableArray(int size = 20, IList<T> initialList = null)
        {
            items = new T[size];

            if (initialList != null)
            {
                initialList.Take(size).ToArray().CopyTo(items, 0);
                Invoke();
            }
        }

        public void Invoke()
        {
            AnyValueChanged.Invoke(items);
        }

        public int Find(T item)
        {
            if (item == null) return -1;

            for (var i = 0; i < items.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[i], item)) return i;
            }

            return -1;
        }

        public void Swap(int index1, int index2)
        {
            (items[index1], items[index2]) = (items[index2], items[index1]);
            Invoke();
        }

        public void Clear()
        {
            items = new T[items.Length];
            Invoke();
        }

        public bool TryAdd(T item)
        {
            //Debug.Log("INVENTORY CAPACITY : " + items.Length);
            for (var i = 0; i < items.Length; i++)
            {
                //Debug.Log("TRYING TO ADD INDEX : " + i);
                if (TryAddAt(item, i)) return true;
            }

            return false;
        }

        public bool TryAddAt(T item, int index)
        {
            if (!IsIndexInRange(index)) return false;

            if (items[index] != null) return false;

            items[index] = item;
            Invoke();

            return true;
        }

        public bool TryRemove(T item)
        {
            for (var i = 0; i < items.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[i], item) && TryRemoveAt(i)) return true;
            }

            return false;
        }

        public bool TryRemoveAt(int index)
        {
            if (!IsIndexInRange(index)) return false;

            if (items[index] == null) return false;

            items[index] = default;
            Invoke();

            return true;
        }

        public bool IsIndexInRange(int index) => index >= 0 && index < items.Length;
    }
}