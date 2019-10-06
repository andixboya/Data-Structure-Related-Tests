

namespace L02_Linked_Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public  class LinkedStack<T> :IEnumerable<T>
    {
        public int Count { get; set; }

        private StackNode Top { get; set; }

        public void Push(T value)
        {
            this.Top = new StackNode(value, this.Top);
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count==0)
            {
                throw new InvalidOperationException("There are no elements to pop!");
            }

            var oldTop = this.Top;
            this.Top = oldTop.Next;
            this.Count--;

            return oldTop.Value;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("No element at the Top!");
            }

            return this.Top.Value;
        }

        public T[] ToArray()
        {
            T[] result = new T[this.Count];

            var currentTop = this.Top;

            int counter = 0;
            while (currentTop!= null)
            {
                result[counter] = currentTop.Value;
                currentTop = currentTop.Next;
                counter++;
            }

            return result;
        }


        public IEnumerator<T> GetEnumerator()
        {
            var currentTop = this.Top;

            while (currentTop!=null)
            {
                yield return currentTop.Value;
                currentTop = currentTop.Next;
            }
            
        }

        

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        

        private class StackNode
        {
            public StackNode(T value, StackNode next)
            {
                this.Value = value;
                this.Next = next;
            }
            public T Value { get; set; }

            public StackNode Next { get; set; }

        }
    }
}
