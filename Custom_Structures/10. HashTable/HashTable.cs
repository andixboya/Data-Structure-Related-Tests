using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{

    private const int DefaultCapacity = 16;

    private const float LoadFactor = 0.7f;

    private LinkedList<KeyValue<TKey, TValue>>[] elements;


    public int Count { get; private set; } //no need to refactor this !


    public int Capacity
    {
        get
        {
            return this.elements.Length;
        }
    }



    public HashTable(int capacity = DefaultCapacity)
    {
        this.elements = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        this.Count = 0;

    }

    public void Add(TKey key, TValue value)
    {

        GrowIfNecessary();

        int index = Math.Abs(key.GetHashCode() % this.Capacity);

        var currentList = this.elements[index];

        if (currentList is null)
        {
            this.elements[index] = new LinkedList<KeyValue<TKey, TValue>>();
            currentList = this.elements[index];
        }

        foreach (var kvp in currentList)
        {
            if (kvp.Key.Equals(key))
            {
                throw new ArgumentException();
            }

        }

        KeyValue<TKey, TValue> newKvp = new KeyValue<TKey, TValue>(key, value);
        this.elements[index].AddLast(newKvp);

        this.Count++;

    }

    private void GrowIfNecessary()
    {
        //i think it should be +1 because if it is on the boundary it should grow first, so we check ++
        //note: here convert it into float, otherwise it will round it up/down to an int!
        float currentRate = ((float)this.Count + 1) / this.Capacity;

        if (currentRate >= LoadFactor)
        {
            Grow();
        }

    }

    private void Grow()
    {
        //interesting, here we add a table and we`ll use its methods to fill in the array!
        //and we`ll transfer its newArray into the current one.
        HashTable<TKey, TValue> newTable = new HashTable<TKey, TValue>(this.Capacity * 2);

        foreach (var linkedList in this.elements.Where(e => e != null))
        {
            foreach (var kvp in linkedList)
            {
                newTable.Add(kvp.Key, kvp.Value);
            }

        }

        this.elements = newTable.elements;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        this.GrowIfNecessary();

        int index = Math.Abs(key.GetHashCode() % this.Capacity);

        var currentList = this.elements[index];

        if (currentList is null)
        {
            this.elements[index] = new LinkedList<KeyValue<TKey, TValue>>();
            currentList = this.elements[index];
        }

        foreach (var kvp in currentList)
        {
            if (kvp.Key.Equals(key))
            {
                kvp.Value = value;

                return true;
            }

        }




        KeyValue<TKey, TValue> newKvp = new KeyValue<TKey, TValue>(key, value);
        this.elements[index].AddLast(newKvp);

        //here we`ll increase by 1 , because 
        this.Count++;

        return false;
    }

    public TValue Get(TKey key)
    {
        var kvp = this.Find(key);

        if (kvp is null)
        {
            throw new KeyNotFoundException();
        }

        return kvp.Value;

    }

    public TValue this[TKey key]
    {
        get
        {
            var kvp = this.Find(key);

            if (kvp == null)
            {
                throw new KeyNotFoundException();
            }

            return kvp.Value;
        }
        set
        {
            this.AddOrReplace(key, value);
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var current = this.Find(key);

        if (current is null)
        {
            value = default(TValue);
            return false;
        }

        value = current.Value;
        return true;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        KeyValue<TKey, TValue> kvp = null;

        int hash = Math.Abs(key.GetHashCode()) % this.Capacity;

        var linkedList = this.elements[hash];

        if (linkedList is null)
        {
            return kvp;
        }


        foreach (var pair in linkedList)
        {
            if (pair.Key.Equals(key))
            {
                kvp = new KeyValue<TKey, TValue>(pair.Key, pair.Value);
                break;
            }

        }

        return kvp;
    }

    public bool ContainsKey(TKey key)
    {
        var kvp = this.Find(key);

        return kvp is null ? false : true;
    }

    public bool Remove(TKey key)
    {
        int index = Math.Abs(key.GetHashCode())%this.Capacity;

        var list = this.elements[index];

        if (list is null)
        {
            return false;
        }

        var kvpToDelete = default(KeyValue<TKey, TValue>);

        foreach (var kvp in list)
        {
            if (kvp.Key.Equals(key))
            {
                kvpToDelete = kvp;
                break;
            }

        }

        if (kvpToDelete is null)
        {
            return false;
        }

        list.Remove(kvpToDelete);
        this.Count--;

        return true;
    }

    public void Clear()
    {
        this.elements = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            foreach (var list in this.elements.Where(l=> l != null))
            {
                foreach (var kvp in list)
                {
                    yield return kvp.Key;
                }

            }

            //this is not working, apparently? 
            //return this.elements.SelectMany(l => l).Select(k => k.Key);
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {

            foreach (var list in this.elements.Where(l => l != null))
            {
                foreach (var kvp in list)
                {
                    yield return kvp.Value;
                }

            }

            //return this.elements.SelectMany(l => l).Select(k => k.Value);
        }
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {

        foreach (var list in this.elements.Where(l => l != null))
        {
            foreach (var kvp in list)
            {
                yield return kvp;
            }

        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
