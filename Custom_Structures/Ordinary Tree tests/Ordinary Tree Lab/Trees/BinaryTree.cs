using System;

public class BinaryTree<T>
{

    public T Value { get; private set; }

    public BinaryTree<T> Left { get; private set; }

    public BinaryTree<T> Right { get; private set; }

    


    public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
    {
        this.Value = value;
        this.Left = leftChild;
        this.Right = rightChild;
    }

    public void PrintIndentedPreOrder( int indent = 0)
    {
        this.PrintIndentedPreOrder(this, indent);
    }

    public void PrintIndentedPreOrder( BinaryTree<T> node, int indent = 0)
    {
        if (node is null)
        {
            return; 
        }

        Console.WriteLine($"{new string(' ',indent)}{node.Value}");
        this.PrintIndentedPreOrder(node.Left, indent+2);
        this.PrintIndentedPreOrder(node.Right, indent+2);
    }

    public void EachInOrder(Action<T> action)
    {
        EachInOrder(this, action);
    }

    public void EachInOrder(BinaryTree<T> node, Action<T> action)
    {
        if (node is null)
        {
            return;
        }

        EachInOrder(node.Left,action);
        action(node.Value);
        EachInOrder(node.Right, action);
    }



    public void EachPostOrder(Action<T> action)
    {
        this.EachPostOrder(this, action);
    }

    public void EachPostOrder(BinaryTree<T> node, Action <T> action)
    {
        if (node is null)
        {
            return;
        }
        EachPostOrder(node.Left, action);
        EachPostOrder(node.Right, action);
        action(node.Value);

    }
}
