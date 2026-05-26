using System;
using System.Collections;
using System.Collections.Generic;

namespace Colections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SmartStack<int> stack1 = new SmartStack<int>();
            Console.WriteLine($"Stack1: Capacity={stack1.Capacity}, Count={stack1.Count}");

            for (int i = 1; i <= 6; i++)
                stack1.Push(i * 10);

            Console.WriteLine($"После Push: Count={stack1.Count}, Capacity={stack1.Capacity}, Peek={stack1.Peek()}");

            while (stack1.Count > 0)
                Console.WriteLine($"Pop: {stack1.Pop()}, i: {stack1.Count}");

            SmartStack<string> stack2 = new SmartStack<string>(10);
            string[] fruits = { "яблоко", "банан", "вишня", "финик" };
            stack2.PushRange(fruits);
            Console.WriteLine($"PushRange: Count={stack2.Count}, Peek={stack2.Peek()}");

            Console.WriteLine($"Contains 'банан': {stack2.Contains("банан")}");
            Console.WriteLine($"Contains 'груша': {stack2.Contains("груша")}");

            for (int i = 0; i < stack2.Count; i++)
                Console.WriteLine($"Глубина {i}: {stack2[i]}");

            int[] numbers = { 100, 200, 300, 400 };
            SmartStack<int> stack3 = new SmartStack<int>(numbers);
            Console.WriteLine($"Из коллекции: Count={stack3.Count}, Peek={stack3.Peek()}");

            SmartStack<int> stack4 = new SmartStack<int>(3);
            stack4.Push(1);
            stack4.Push(2);
            int[] newNumbers = { 10, 20, 30, 40, 50 };
            stack4.PushRange(newNumbers);
            Console.WriteLine($"Расширение: Count={stack4.Count}, Capacity={stack4.Capacity}, Peek={stack4.Peek()}");

            SmartStack<double> emptyStack = new SmartStack<double>();
            try { emptyStack.Pop(); }
            catch (InvalidOperationException ex) { Console.WriteLine($"Ошибка Pop: {ex.Message}"); }

            try { emptyStack.Peek(); }
            catch (InvalidOperationException ex) { Console.WriteLine($"Ошибка Peek: {ex.Message}"); }

            SmartStack<int> stack6 = new SmartStack<int>(2);
            stack6.Push(1);
            stack6.Push(2);
            stack6.Push(3);
            Console.WriteLine($"До Pop: Count={stack6.Count}, Capacity={stack6.Capacity}");
            stack6.Pop();
            Console.WriteLine($"После Pop: Count={stack6.Count}, Capacity={stack6.Capacity}");
        }
    }
}