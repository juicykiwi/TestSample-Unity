using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum PrioritySortType
{
    Ascending,
    Descending,
}
    
public class PriorityQueue<T> : ICollection<T> where T : IComparable<T>
{
    PrioritySortType _prioritySortType = PrioritySortType.Ascending;
    
    protected List<T> _list = new List<T>();

    public int Count { get { return _list.Count; } }
    public int EndIndex { get { return _list.Count - 1; } }

    public T this[int index]
    {
        get { return _list[index]; }
        set { _list[index] = value; }
    }

    public bool IsReadOnly { get { return false; } }

    public PriorityQueue()
    {
    }

    public PriorityQueue(PrioritySortType sortType)
    {
        _prioritySortType = sortType;
    }

    public void Add(T item)
    {
        _list.Add(item);
        HeapInsertSort(EndIndex);
    }

    public void Clear()
    {
        _list.Clear();
    }

    public bool Contains(T item)
    {
        return _list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException("The array cannot be null.");
        
        if (arrayIndex < 0)
            throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
        
        if (Count > array.Length - arrayIndex + 1)
            throw new ArgumentException("The destination array has fewer elements than the collection.");

        for (int i = 0; i < _list.Count; i++)
        {
            array[i + arrayIndex] = _list[i];
        }
    }

    public bool Remove(T item)
    {
        bool result = false;

        for (int index = 0; index < _list.Count; index++)
        {
            T eachItem = _list[index];

            if (eachItem.Equals(item) == true)
            {
                Swap(index, EndIndex);
                _list.RemoveAt(EndIndex);
                HeapRemoveSort(index);

                result = true;
                break;
            }
        }

        return result;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new PriorityQueueEnumerator<T>(this);
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return new PriorityQueueEnumerator<T>(this);
    }

    public T Pop()
    {
        if (_list.Count <= 0)
            return default(T);

        T data = _list[0];

        Swap(0, EndIndex);
        _list.RemoveAt(EndIndex);
        HeapRemoveSort(0);

        return data;
    }

    int ParentDataIndex(int curIndex)
    {
        return ((curIndex - 1) / 2);
    }

    int LeftChildIndex(int curIndex)
    {
        return ((2 * curIndex) + 1);
    }

    int RightChildIndex(int curIndex)
    {
        return ((2 * curIndex) + 2);
    }

    void HeapInsertSort(int index)
    {
        if (index > EndIndex)
            return;

        int currentIndex = index;

        while (currentIndex > 0)
        {
            int parentIndex = ParentDataIndex(currentIndex);
            if (CompareBySortType(currentIndex, parentIndex) >= 0)
                break;

            Swap(currentIndex, parentIndex);
            currentIndex = parentIndex;
        }
    }

    void HeapRemoveSort(int index)
    {
        int currentIndex = index;
        int compareChildIndex = 0;

        while (currentIndex < EndIndex)
        {
            compareChildIndex = ChildIndexForCompare(currentIndex);
            if (compareChildIndex == -1)
                break;

            if (CompareBySortType(currentIndex, compareChildIndex) <= 0)
                break;

            Swap(currentIndex, compareChildIndex);
            currentIndex = compareChildIndex;
        }
    }

    int ChildIndexForCompare(int index)
    {
        int leftChildIndex = LeftChildIndex(index);
        int rightChildIndex = RightChildIndex(index);

        if (leftChildIndex > EndIndex)
            return -1;

        if (rightChildIndex > EndIndex)
            return leftChildIndex;

        if (CompareBySortType(leftChildIndex, rightChildIndex) <= 0)
            return leftChildIndex;

        return rightChildIndex;
    }

    int CompareBySortType(int leftIndex, int rightIndex)
    {
        int compareValue = _list[leftIndex].CompareTo(_list[rightIndex]);

        if (_prioritySortType == PrioritySortType.Ascending)
            return compareValue;

        return compareValue * -1;
    }

    void Swap(int leftIndex, int rightIndex)
    {
        T temp = _list[leftIndex];
        _list[leftIndex] = _list[rightIndex];
        _list[rightIndex] = temp;
    }
}

public class PriorityQueueEnumerator<T> : IEnumerator<T> where T : IComparable<T>
{
    PriorityQueue<T> _collection = null;
    int _curIndex = -1;
    T _curItem = default(T);

    public PriorityQueueEnumerator(PriorityQueue<T> collection)
    {
        _collection = collection;
    }

    public bool MoveNext()
    {
        if (++_curIndex >= _collection.Count)
            return false;
            
        _curItem = _collection[_curIndex];
        return true;
    }

    public void Reset()
    {
        _curIndex = -1; 
    }

    void IDisposable.Dispose()
    {
    }

    public T Current { get { return _curItem; } }

    object IEnumerator.Current { get { return Current; } }
}
