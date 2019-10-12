using System;
using System.Collections.Generic;
using System.Linq;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public void Insert(T item)
    {

        this.heap.Add(item);
        var lastIndex = heap.Count - 1;
        this.HeapifyUp(item, lastIndex);

    }

    public void HeapifyUp(T item, int index)
    {
        var childIndex = index;

        var parentIndex = (index - 1) / 2;

        var parent = this.heap[parentIndex];

        int compare = parent.CompareTo(item);

        if (compare < 0)
        {
            SwapNodeValues(parentIndex, childIndex);
            //we should take the parentIndex because it is next
            this.HeapifyUp(this.heap[parentIndex], parentIndex);
        }

    }

    private void SwapNodeValues(int parentIndex, int childIndex)
    {
        //here we shift them
        var parentValue = this.heap[parentIndex];
        this.heap[parentIndex] = this.heap[childIndex];
        this.heap[childIndex] = parentValue;
    }

    public T Peek()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("No elements within the heap.");
        }

        return this.heap[0];
    }

    public T Pull()
    {
        //returns the last on the line
        if (this.heap.Count == 0)
        {
            throw new InvalidOperationException();
        }


        var element = this.heap[0];
        var lastIndex = this.Count - 1;
        this.SwapNodeValues(0, this.Count - 1);
        this.heap.RemoveAt(lastIndex);
        HeapifyDown(0);
        return element;
    }

    private void HeapifyDown(int index)
    {

        int parentIndex = index;



        // above count*2 are the children of this element
        //this is tricky, until which index there are elements who have children (it is a given fact, use it, don`t think about it)
        while (parentIndex < this.heap.Count / 2)
        {
            int childIndex = 2 * parentIndex + 1;



            //otherwise well go outside of the array`s bound
            if (childIndex + 1 < this.heap.Count)
            {
            //also here, you have to check which node has the bigger value and use it
                childIndex = GetGreaterIndex(childIndex, childIndex + 1);
            }

            //here you forgot to add a case when you should swap , not always!
            if (this.heap[parentIndex].CompareTo(this.heap[childIndex]) < 0)
            {
                this.SwapNodeValues(parentIndex, childIndex);
            }

            //by doing this, it goes to the right until it reaches the bound where the elements have children
            parentIndex = childIndex;
        }

    }

    //you`ll need to correct the index here instead of getting the element because later on the index will be used!
    private int GetGreaterIndex(int leftIndex, int rightIndex)
    {
        T leftValue = this.heap[leftIndex];
        T rightValue = this.heap[rightIndex];

        int compare = leftValue.CompareTo(rightValue);

        return compare < 0 ? rightIndex : leftIndex;

    }
}
