using System;

public class KdTree
{
    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(Point2D point)
    {

        int level = 0;

        bool isFound = false;
        this.Contains(this.root, point, level, ref isFound);
        return isFound;


    }

    private void Contains(Node current, Point2D point, int level, ref bool isFound)
    {
        if (current is null)
        {
            isFound = false;
        }

        if (current.Point.Equals(point))
        {
            isFound = true; 
        }

        if (current.Left != null && isFound==false)
        {
            Contains(current.Left, point, level++,ref isFound);
        }

        if (current.Right!= null && isFound == false)
        {
            Contains(current.Right, point, level++,ref isFound);
        }


    }

    public void Insert(Point2D point)
    {

        int level = 0;
        this.root = Insert(this.root, point, level);

    }

    private Node Insert(Node current, Point2D point, int level)
    {
        if (current is null)
        {
            return new Node(point);
        }

        var depthLevel = level % 2;
        int compare = -1;

        if (depthLevel == 0)
        {
            compare = point.X.CompareTo(current.Point.X);

            if (compare < 0)
            {
                current.Left = Insert(current.Left, point, level + 1);
            }
            else
            {
                current.Right = Insert(current.Right, point, level++);
            }


        }

        else
        {
            compare = point.Y.CompareTo(current.Point.Y);

            if (compare < 0)
            {
                current.Left = Insert(current.Left, point, level + 1);
            }

            else
            {
                current.Right = Insert(current.Right, point, level++);
            }

        }


        return current;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }
}
