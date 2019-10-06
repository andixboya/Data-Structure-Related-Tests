using System;

namespace L02_Linked_Stack
{
    public class Launcher
    {
        static void Main(string[] args)
        {
            LinkedStack<int> numbers = new LinkedStack<int>();

            numbers.Push(5);
            numbers.Push(6);
            numbers.Push(7);
            var array = numbers.ToArray();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(numbers.Peek());
                Console.WriteLine(numbers.Pop());
                Console.WriteLine(array[i]);
            }

        }
    }
}
