using System;
using System.Collections;
using System.Collections.Generic;

namespace Colections
{
    internal class Program
    {
        public class SmartStack<T> : IEnumerable<T>
        {
            private T[] _array;
            private int _count;

            public SmartStack()
            {
                _array = new T[4];
                _count = 0;
            }

            public SmartStack(int capacity)
            {
                if (capacity < 0)
                    throw new ArgumentException("Ёмкость не может быть отрицательной", nameof(capacity));

                _array = new T[capacity];
                _count = 0;
            }

            public SmartStack(IEnumerable<T> collection)
            {
                if (collection == null)
                    throw new ArgumentNullException(nameof(collection));

                int tempCount = 0;
                IEnumerator<T> enumerator = collection.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    tempCount++;
                }

                _array = new T[tempCount];
                _count = tempCount;

                enumerator = collection.GetEnumerator();
                int index = tempCount - 1;
                while (enumerator.MoveNext())
                {
                    _array[index] = enumerator.Current;
                    index--;
                }
            }

            public int Count => _count;

            public int Capacity => _array.Length;

            public void Push(T item)
            {
                if (_count == _array.Length)
                {
                    T[] newArray = new T[_array.Length * 2];
                    for (int i = 0; i < _count; i++)
                    {
                        newArray[i] = _array[i];
                    }
                    _array = newArray;
                }
                _array[_count] = item;
                _count++;
            }

            public void PushRange(IEnumerable<T> collection)
            {
                if (collection == null)
                    throw new ArgumentNullException(nameof(collection));

                int addCount = 0;
                IEnumerator<T> enumerator = collection.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    addCount++;
                }

                if (addCount == 0)
                    return;

                if (_count + addCount > _array.Length)
                {
                    int newSize = _array.Length;
                    while (newSize < _count + addCount)
                    {
                        newSize *= 2;
                    }

                    T[] newArray = new T[newSize];
                    for (int i = 0; i < _count; i++)
                    {
                        newArray[i] = _array[i];
                    }
                    _array = newArray;
                }

                enumerator = collection.GetEnumerator();
                T[] tempArray = new T[addCount];
                int tempIndex = 0;
                while (enumerator.MoveNext())
                {
                    tempArray[tempIndex] = enumerator.Current;
                    tempIndex++;
                }

                for (int i = 0; i < addCount; i++)
                {
                    _array[_count + i] = tempArray[addCount - 1 - i];
                }
                _count += addCount;
            }

            public T Pop()
            {
                if (_count == 0)
                    throw new InvalidOperationException("Стек пуст");

                _count--;
                T result = _array[_count];
                _array[_count] = default(T);
                return result;
            }

            public T Peek()
            {
                if (_count == 0)
                    throw new InvalidOperationException("Стек пуст");

                return _array[_count - 1];
            }

            public bool Contains(T item)
            {
                for (int i = 0; i < _count; i++)
                {
                    if (Equals(_array[i], item))
                        return true;
                }
                return false;
            }

            public T this[int depth]
            {
                get
                {
                    if (depth < 0 || depth >= _count)
                        throw new ArgumentOutOfRangeException(nameof(depth), "Индекс выходит за границы стека");
                    return _array[depth];
                }
                set
                {
                    if (depth < 0 || depth >= _count)
                        throw new ArgumentOutOfRangeException(nameof(depth), "Индекс выходит за границы стека");
                    _array[depth] = value;
                }
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (int i = 0; i < _count; i++)
                {
                    yield return _array[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

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