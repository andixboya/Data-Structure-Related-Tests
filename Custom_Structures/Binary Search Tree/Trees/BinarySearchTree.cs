using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node root;

    public BinarySearchTree()
    {
        this.root = null;
    }
    public BinarySearchTree(Node node)
    {
        this.Copy(node);
    }

    public void Copy(Node node)
    {
        if (node is null)
        {
            return; 
        }

        //note IMPORTANT => debug here, to understand how it makes
        //another new instance and it does not keep the old reference! 
        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);

    }

    public void Insert(T value)
    {
        this.root= this.Insert(this.root, value);

        //#region interactive
        ////note 0:  first => in which it is empty! 
        //if (this.root is null)
        //{
        //    this.root = new Node(value);
        //    return;
        //}
        ////note: 1 in case we got a root, we search depending on our value within it 
        ////we compare it with the value given

        ////we`ll need the parent as well, to remember wher it has to be connected!
        ////we start with null, since the current is ROOT (parent);
        //Node parent = null;
        //Node current = this.root;


        ////note: 2 next we distribute it to the right place (left or right, depending on
        ////the bottom result) 


        ////when do we break the loop? 
        //while (current != null) // != , not "==" !!!!
        //{
        //    int valueToCompare = current.Value.CompareTo(value);

        //    //first case when current value is lower than root value
        //    if (valueToCompare > 0)
        //    {
        //        parent = current;
        //        current = current.Left;

        //    }
        //    //second case when current value is bigger than root value
        //    else if (valueToCompare < 0)
        //    {
        //        parent = current;
        //        current = current.Right;
        //    }
        //    //in case of duplicate we don`t add it!
        //    else
        //    {
        //        return;
        //    }
        //}

        ////here we have to decide, in which place it will be (left/right)
        ////there is no equal case, because we `ve made sure that there will be no balance!
        //current = new Node(value);
        //var sideDesider = current.Value.CompareTo(parent.Value);

        //if (sideDesider < 0)
        //{
        //    parent.Left = current;
        //}
        //else
        //{
        //    parent.Right = current;
        //}

        //#endregion





    }

    private Node Insert(Node node, T value)
    {
        // >0 ==> value is bigger
        // =0 => equal
        // <0 => value is lower
        

        if (node is null)
        {
            return new Node(value);
        }

        var comparisonValue = node.Value.CompareTo(value);

        if (comparisonValue>0)
        {
             node.Left= this.Insert(node.Left, value);
        }
        
        else if (comparisonValue<0)
        {
            node.Right= this.Insert(node.Right, value);
        }

       
        return node;

    }


    public bool Contains(T value)
    {
        Node current = this.root;

        if (current is null)
        {
            return false;
        }

        while (current != null)
        {
            var currentValue = current.Value;

            //if (currentValue.Equals(value))
            //{
            //    return true;
            //}

            var comparisonValue = currentValue.CompareTo(value);

            if (comparisonValue>0)
            {
                current = current.Left;
            }
            else if (comparisonValue<0)
            {
                current = current.Right;
            }
            
            else
            {
                return true;
            }
            
        }


        return false;
    }

    public void DeleteMin()
    {
        Node parent = null;
        Node current = this.root;

        if (current is null)
        {
            return;
        }
        if (current.Left is null && current.Right is null)
        {

            var x = ReferenceEquals(current, this.root); //this is so weird!?!?!
            //current = null; //the structure is reference, yet when i set it to null it does not set
            //the instance in the object to null?
            this.root = null;
            
            
            return;
        }


        while (current.Left!= null)
        {
            parent = current;
            current = current.Left;
        }

        if (current.Right != null)
        {
            parent.Left = current.Right;
        }

        else
        {
            parent.Left = null;
        }

    }

    public BinarySearchTree<T> Search(T item)
    {

        Node current = this.root;

        while (current != null)
        {

            int compare = current.Value.CompareTo(item);


            if (compare>0)
            {
                current = current.Left;
            }
            else if (compare<0)
            {
                current = current.Right;
            }
            else if (compare==0)
            {
                //here we add it and use the ctor + the help
                //of a method to create it?
                return new BinarySearchTree<T>(current);
            }

        }


        return null;

    }



    public IEnumerable<T> Range(T startRange, T endRange)
    {
        List<T> result = new List<T>();


        Range(this.root, result, startRange, endRange);

        return result;
    }

    private void Range(Node currentRoot, List<T> result, T start, T end)
    {
        if (currentRoot is null)
        {
            return;
        }

        int lowerBoundary = currentRoot.Value.CompareTo(start);
        int higherBoundary = currentRoot.Value.CompareTo(end);

        //these all are -1/0/1 (not the actual values) 
        if (lowerBoundary>0 )
        {
            Range(currentRoot.Left, result, start, end);
        }
        //if we add 0 here, we add the equal cases
        if (lowerBoundary>=0 && higherBoundary<=0)
        {
            result.Add(currentRoot.Value);
        }
        if (higherBoundary<0)
        {
            Range(currentRoot.Right, result, start, end);
        }

    }

    public void EachInOrder(Action<T> action)
    {
         this.EachInOrder(this.root, action);
    }

    public void EachInOrder(Node node, Action<T> action)
    {

        if (node is null)
        {
            return;
        }

        if (node.Left != null)
        {
            EachInOrder(node.Left, action);
        }
        action(node.Value);
        
        if (node.Right!= null)
        {
            EachInOrder(node.Right, action);
        }
    }


    public class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }


        public T Value { get;   set; }

        public Node Left { get;   set; }

        public Node Right { get;  set; }

    }

}






public class Launcher
{
    public static void Main(string[] args)
    {
        
    }
}
