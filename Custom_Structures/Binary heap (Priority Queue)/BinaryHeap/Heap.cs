using System;
using System.Collections.Generic;
using System.Linq;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        ConstructHeap(arr);
        HeapSort(arr);
    }

    private static void HeapSort(T[] arr)
    {

        for (int i = arr.Length - 1; i >= 0; i--)
        {
            //we swap the values
            SwapNodeValues(arr, 0, i);
            //then we heapify them down 
            HeapifyDown(arr, 0, i);
        }


    }

    private static void ConstructHeap(T[] arr)
    {
        for (int i = arr.Length / 2; i >= 0; i--)
        {
            //here we give the length or the i? 
            HeapifyDown(arr, 0, arr.Length);
        }


    }


    //transfer the methods below from the other class
    private static int GetGreaterIndex(T[] heap, int leftIndex, int rightIndex)
    {
        T leftValue = heap[leftIndex];
        T rightValue = heap[rightIndex];

        int compare = leftValue.CompareTo(rightValue);

        return compare < 0 ? rightIndex : leftIndex;

    }

    private static void SwapNodeValues(T[] heap, int parentIndex, int childIndex)
    {
        //here we shift them
        var parentValue = heap[parentIndex];
        heap[parentIndex] = heap[childIndex];
        heap[childIndex] = parentValue;
    }


    private static void HeapifyDown(T[] heap, int index, int length)
    {

        int parentIndex = index;

        //here we`ll be decreasing the length?
        while (parentIndex < length / 2)
        {
            int childIndex = 2 * parentIndex + 1;

            //here we`ll be decreasing the length?
            if (childIndex + 1 < length)
            {
                childIndex = GetGreaterIndex(heap, childIndex, childIndex + 1);
            }


            if (heap[parentIndex].CompareTo(heap[childIndex]) < 0)
            {
                SwapNodeValues(heap, parentIndex, childIndex);
            }


            parentIndex = childIndex;
        }

    }


    //won`t be necessary? 
    //public static void HeapifyUp( List<T> heap, T item, int index)
    //{
    //    var childIndex = index;

    //    var parentIndex = (index - 1) / 2;

    //    var parent = heap[parentIndex];

    //    int compare = parent.CompareTo(item);

    //    if (compare < 0)
    //    {
    //        SwapNodeValues(heap,parentIndex, childIndex);
    //        //we should take the parentIndex because it is next
    //        HeapifyUp(heap,heap[parentIndex], parentIndex);
    //    }

    //}

}
