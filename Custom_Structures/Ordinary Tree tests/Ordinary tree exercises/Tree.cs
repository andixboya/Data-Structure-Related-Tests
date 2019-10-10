using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic_Tree_Structure
{
    public class Tree<T>
    {
        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = children.ToList();
        }

        public Tree<T> Parent { get; set; }

        public T Value { get; set; }

        public List<Tree<T>> Children { get; set; }


        public void Print(int indent = 0)
        {

            //initially it is the parent, afterwards it will loop through the children ( you don`t need ifs here!)

            Console.WriteLine($"{new string(' ', indent)}{ this.Value}");
            //Console.WriteLine($"{new string(' ', indent)}{ this.Value}");

            foreach (var child in this.Children)
            {
                //here i was printing from the view of the parent and it was never getting out of the loop!
                child.Print(indent + 2);
            }

        }
    }
}
