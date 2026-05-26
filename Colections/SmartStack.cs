using System.Collections;

namespace Colections
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
}
