using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StartUp
{

    public static Dictionary<int, Tree<int>> tree
        = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        int count = int.Parse(Console.ReadLine());

        FillInDictionary(count);
        //we add the parent?  WE DONT ADD IT , WE`LL JUST GET IT , SO IT CAN START PRINTING FROM THE TOP!!!!
        //Tree<int> top = GetRootNode();

        
        int criteria = int.Parse(Console.ReadLine());
        var leaves = tree
            .Where(t => !t.Value.Children.Any())
            .OrderBy(n => n.Value.Value)
            .ToList();

        var result = new List<Tree<int>>();

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Paths of sum {criteria}:");
        foreach (var item in leaves)
        {
            var leaf = item.Value;
            var sum = 0;
            sum += leaf.Value;

            while (leaf.Parent != null)
            {
                leaf = leaf.Parent;
                sum += leaf.Value;
            }

            if (sum== criteria)
            {
                result.Add(item.Value);
            }

        }
        

        foreach (var item in result)
        {
            var finalResult = new List<int>();
            var leaf = item;
            finalResult.Add(leaf.Value);
            while (leaf.Parent != null)
            {
                leaf = leaf.Parent;
                finalResult.Add(leaf.Value);   
            }
            finalResult.Reverse();

            sb.AppendLine(string.Join(" ", finalResult));
        }

        Console.WriteLine(sb.ToString().TrimEnd());

    }

    //1
    private static Tree<int> GetRootNode()
    {
        return tree.FirstOrDefault(t => t.Value.Parent is null).Value;
    }

    private static void FillInDictionary(int count)
    {
        for (int i = 0; i < count - 1; i++)
        {
            //parse the current iteration
            int[] numbers = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            int parent = numbers[0];
            int child = numbers[1];


            //add the parent to the dictionary (if they exist or not)
            if (!tree.ContainsKey(parent))
            {
                tree.Add(parent, new Tree<int>(parent));
            }
            //add the child to the dictionary (if they exist or not)
            if (!tree.ContainsKey(child))
            {
                tree.Add(child, new Tree<int>(child));
            }

            //we get them again from the dictionary and bind them
            Tree<int> childNodde = tree[child];
            Tree<int> parentNode = tree[parent];

            //note: here you add them TO EACH OTHER (not only to the parent, it is two-sided!)
            parentNode.Children.Add(childNodde);
            childNodde.Parent = parentNode;

        }
    }

    //2
    public static List<int> GetAllLeaves(Tree<int> root)
    {
        List<int> result = new List<int>();

        result = GetAllLeaves(root, result);
        return result.OrderBy(i => i).ToList();
    }

    private static List<int> GetAllLeaves(Tree<int> node, List<int> result)
    {

        if (node is null)
        {
            return result;
        }

        if (node.Children.Count == 0)
        {
            result.Add(node.Value);
        }

        foreach (var child in node.Children)
        {
            GetAllLeaves(child, result);
        }


        return result;
    }

    //3 
    public static List<int> GetAllMiddleNodes(Tree<int> root)
    {
        List<int> result = new List<int>();

        result = GetAllMiddleNodes(root, result);
        return result.OrderBy(i => i).ToList();
    }

    private static List<int> GetAllMiddleNodes(Tree<int> node, List<int> result)
    {

        if (node is null)
        {
            return result;
        }

        if (node.Children.Count > 0 && node.Parent != null)
        {
            result.Add(node.Value);
        }

        foreach (var child in node.Children)
        {
            GetAllMiddleNodes(child, result);
        }


        return result;
    }


    //5 //really interesting... with the usage of ref! 
    private static Tree<int> GetDeepestNode()
    {
        int biggestDepth = 0;
        Tree<int> deepestNode = null;
        FindDeepestNode(GetRootNode(), 0, ref biggestDepth, ref deepestNode);
        //чрез референтните стойности ще се пренасят (аз мислех с tuple) 
        return deepestNode;
    }

    private static void FindDeepestNode(Tree<int> currentNode, int currentDepth, ref int biggestDepth, ref Tree<int> deepestNode)
    {
        if (biggestDepth < currentDepth)
        {
            biggestDepth = currentDepth;
            deepestNode = currentNode;
        }

        foreach (var child in currentNode.Children)
        {
            FindDeepestNode(child, currentDepth + 1, ref biggestDepth, ref deepestNode);
        }
    }

    //6 Longest Path
    private static string LongestPath()
    {
        List<int> results = new List<int>();
        Tree<int> deepestNode = GetDeepestNode();

        if (deepestNode is null)
        {
            return "";
        }

        results.Add(deepestNode.Value);
        

        while (deepestNode.Parent != null)
        {
            deepestNode = deepestNode.Parent;
            results.Add(deepestNode.Value);
        }
        results.Reverse();
        return string.Join(" ",results);


    }

    //7 All Paths with a Given Sum



}

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


