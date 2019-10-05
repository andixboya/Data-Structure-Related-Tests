using System;

public class CircularQueue<T>
{
    private const int DefaultCapacity = 4;

    //holds information about the current BUffer limit (its length)
    private T[] elements;

    //counts the current number of elements within the current Buffer
    public int Count { get; private set; }

    //self-explanatory about the rest 
    private int startIndex;
    private int endIndex;

    public CircularQueue(int capacity = DefaultCapacity)
    {
        this.elements = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (this.Count >= this.elements.Length)
        {
            //first case
            this.Resize();
        }
        //normal case
        this.elements[endIndex] = element;
        endIndex = (endIndex + 1) % (this.elements.Length);
        this.Count++;

    }

    private void Resize()
    {

        //we double the buffer capacity
        T[] newElements = new T[this.elements.Length * 2];

        //copy all the elements of the old ones , so that they are placed with same indicies in the new one!
        this.CopyAllElements(newElements);

        //reset the indicies
        this.startIndex = 0;
        this.endIndex = this.Count; //this.elements.Length - 1 (was mine, i guess its wrong)

        //re-set the new array
        this.elements = newElements;

        // TODO

    }

    private T[] CopyAllElements(T[] newArray)
    {

        int index = 0;
        int currentStart = startIndex;

        while (index < this.Count)
        {
            newArray[index] = this.elements[currentStart];
            currentStart = (currentStart + 1) % this.elements.Length;

            index++;
        }

        return newArray;

    }

    // Should throw InvalidOperationException if the queue is empty
    public T Dequeue()
    {

        if (this.Count == 0)
        {
            throw new InvalidOperationException("Queue has no element left!");
        }

        //we`ll take the element
        T elementOut = this.elements[startIndex];

        //move the start index
        //i think we don`t have to empty the cell, because it will get re-written  by itself?
        startIndex = (startIndex + 1) % this.elements.Length;

        //reduce the count, because it is not there any more 
        this.Count--;

        return elementOut;
    }

    public T[] ToArray()
    {
        // TODO
        T[] newArray = new T[this.Count];

        newArray = this.CopyAllElements(newArray);

        return newArray;
    }
}


public class Example
{
    public static void Main()
    {

        CircularQueue<int> queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
