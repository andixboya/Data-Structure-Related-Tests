namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>

    {
        private Node root;
        private Dictionary<T, Node> refToNodes;



        public Hierarchy(T root)
        {
            this.root = new Node(root);
            this.refToNodes = new Dictionary<T, Node>()
            {
                { root,this.root }
            };
        }

        public int Count
        {
            get
            {
                return this.refToNodes.Count;
            }
        }

        public void Add(T element, T child)
        {

            if (!refToNodes.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            if (refToNodes.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            var parent = refToNodes[element];
            var currentChild = new Node(child, parent);


            refToNodes[child] = currentChild;
            parent.Children.Add(currentChild);

        }

        public void Remove(T element)
        {
            if (!refToNodes.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            var current = refToNodes[element];

            if (current.Parent is null)
            {
                throw new InvalidOperationException();
            }



            if (current.Children.Any())
            {
                foreach (var child in current.Children)
                {
                    current.Parent.Children.Add(child);
                    child.Parent = current.Parent;
                }
                
            }

            //we remove the reference here! 
            current.Parent.Children.Remove(current);
            refToNodes.Remove(current.Value);
        }

        public IEnumerable<T> GetChildren(T item)
        {

            if (!refToNodes.ContainsKey(item))
            {
                throw new ArgumentException();
            }
            Node parent = refToNodes[item];

            foreach (var child in parent.Children)
            {
                yield return child.Value;
            }

        }

        public T GetParent(T item)
        {

            if (!refToNodes.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            var result = refToNodes[item]?.Parent;

            if (result is null)
            {
                return default(T);
            }
            return result.Value;
        }

        public bool Contains(T value)
        {
            return refToNodes.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            var currentHierarchy = new HashSet<T>(this.refToNodes.Keys);
            currentHierarchy.IntersectWith(other.refToNodes.Keys);

            return currentHierarchy;
        }

        public IEnumerator<T> GetEnumerator()
        {

            Queue<Node> queue = new Queue<Node>();

            Node current = this.root;
            queue.Enqueue(current);

            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                yield return current.Value;
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }



        public class Node
        {
            public Node(T value, Node parent = null)
            {
                this.Value = value;
                this.Children = new List<Node>();
                this.Parent = parent;
            }

            public List<Node> Children;
            public Node Parent;
            public T Value;

        }
    }

}