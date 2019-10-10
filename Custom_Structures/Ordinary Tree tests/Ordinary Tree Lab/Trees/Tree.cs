using System;
using System.Collections.Generic;
using System.Linq;

public class Tree<T>
{

    public T Value { get; private set; }
    public List<Tree<T>> Children { get; private set; }


    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = children.ToList();
    }

    public void Print(int indent = 0)
    {

        this.Print(this, indent);
    }


    //note: check this again! 
    private void Print(Tree<T> node, int indent)
    {
        //this is the same as dfs 
        Console.WriteLine($"{new string (' ',indent)}{node.Value}");
       
        foreach (var nodeChild in this.Children)
        {
            nodeChild.Print(indent+2);
        }

    }

    public void Each(Action<T> action)
    {
        //TODO:

        Each(this, action);
        
    }

    public void Each (Tree<T> tree, Action<T> action)
    {

        action(tree.Value);

        foreach (var child in tree.Children)
        {
            Each(child, action);
        }

    }

    public IEnumerable<T> OrderDFS()
    {
        var result = new List<T>();
        this.DFS(this, result);
        //StackDFS(result);
        //this.Test(this, result);

        return result;

    }
    public IEnumerable<T> Test(Tree<T> tree,List<T> result)
    {
        //foreach (var child in tree.Children)
        //{
        //    tree.Test(child, result);
        //}

        //result.Add(tree.Value);

        return result;
    }

    private void StackDFS(List<T> result)
    {
        Stack<Tree<T>> stack = new Stack<Tree<T>>();

        stack.Push(this);

        while (stack.Count > 0)
        {
            var current = stack.Pop();

            foreach (var child in current.Children)
            {
                stack.Push(child);
            }

            result.Add(current.Value);
        }

        result.Reverse();
    }

    //via recursion 
    private void DFS(Tree<T> node, List<T> result)
    {
        foreach (var child in node.Children)
        {
            this.DFS(child, result);
        }

        result.Add(node.Value);
    }

    public IEnumerable<T> OrderBFS()
    {
        var result = new List<T>();

        Queue<Tree<T>> queue = new Queue<Tree<T>>();

        queue.Enqueue(this);


        while (queue.Count>0)
        {
            var current = queue.Dequeue();
            result.Add(current.Value);

            foreach (var child in current.Children)
            {
                queue.Enqueue(child);
            }
        }

        return result;
    }
}
