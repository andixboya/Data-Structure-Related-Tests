using System;
using System.Collections;
using System.Collections.Generic;

//linkedList IS JUST A NAME, IT DOES NOT HAVE ANY FUNCTIONS A NORMAL LIST DOES (you just use the T)
public class LinkedList<T> : IEnumerable<T>
{
    public int Count { get; private set; }

    public Node<T> Head { get; private set; }

    public Node<T> Tail { get; private set; }




    public void AddFirst(T item)
    {
        var oldHead = this.Head;

        this.Head = new Node<T>(item);
        this.Head.Next = oldHead;

        if (this.Count==0)
        {
            this.Tail = this.Head;
        }

        this.Count++;
        

    }

    public void AddLast(T item)
    {

        Node<T> oldTail = this.Tail;

        this.Tail = new Node<T>(item);
        

        if (this.Count==0)
        {
            this.Head = this.Tail;
        }
        else
        {
            oldTail.Next = this.Tail;
        }
        

        this.Count++;

    }

    public T RemoveFirst()
    {
        // TODO: Throw exception if the list is empty

        if (this.IsEmpty())
        {
            throw new InvalidOperationException();
        }

        Node<T> oldHead = this.Head;

        this.Head = this.Head.Next;

        this.Count--;
        if (this.IsEmpty())
        {
            this.Tail = null;
        }

        return oldHead.Value;


    }

    public T RemoveLast()
    {
        if (this.Count==0)
        {
            throw new InvalidOperationException();
        }

        Node<T> oldTail = this.Tail;
        if (this.Count==1)
        {
            this.Head = null;
            this.Tail = null;
        }

        else
        {

            Node<T> newTail = this.GetSecondLast();
            newTail.Next = null;
            this.Tail = newTail;
        }

        this.Count--;
        return oldTail.Value;


    }

    public IEnumerator<T> GetEnumerator()
    {
        var node = this.Head;

        while (node.Next!= null)
        {
            yield return node.Value;
            
            node = node.Next;
        }


        // TODO
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public class Node<Т>
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; private set; }

        public Node<T> Next { get; set; }

    }
    private bool IsEmpty()
    {
        return this.Count == 0;
    }
    private Node<T> GetSecondLast()
    {
        var node = this.Head;

        while (node.Next!= this.Tail)
        {

            node = node.Next;
        }

        return node;
    }
}
