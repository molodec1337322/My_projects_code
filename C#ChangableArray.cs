using System;

namespace DynamicArray
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class DynamicArray<T>
    {
        private T[] Arr;
        private int Size;
        DynamicArray(int Size)
        {
            this.Size = Size;
            this.Arr = new T[this.Size];
        }

        DynamicArray(int Size, params T[] ArrData)
        {
            this.Size = Size;
            this.Arr = new T[this.Size];
            try
            {
                ArrData.CopyTo(Arr, 0);
            }
            catch(Exception ex)
            {
                for (int i = 0; i < Size; i++) Arr[i] = default(T);
            }
        }

        public void Push(T element)
        {
            Array.Resize(ref Arr, Size);
            Arr[Size] = element;
            Size++;
        }

        public T Get(int pos)
        {
            try
            {
                return Arr[pos];
            }
            catch(IndexOutOfRangeException ex)
            {
                return default(T);
            }
        }
    }
}

