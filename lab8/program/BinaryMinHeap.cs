using System;
namespace program
{
    public class BinaryMinHeap
    {
        private string[] _heap;
        private int _heapSize;
        public BinaryMinHeap()
        {
            this._heap = new string[7];
            this._heapSize = 0;
        }
        public void Insert(string s)
        {
            if(_heap.Length == _heapSize)
            {
                Resize();
            }
            int position = _heapSize;
            _heap[_heapSize] = s;
            _heapSize++;
            HeapifyBottomToTop(position);
        }
        public string ExtractMin()
        {
            if(_heapSize == 0)
            {
                throw new Exception("Cannot extract, heap is empty.");
            }
            string min = _heap[0];
            _heap[0] = _heap[_heapSize - 1];
            _heap[_heapSize - 1] = min;
            _heapSize--;
            HeapifyTopToBottom(0);
            return min;
        }
        private void Resize()
        {
            Array.Resize(ref _heap, _heap.Length*2);
        }
        private void HeapifyBottomToTop(int i)
        {
            int parentIndex = (i-1)/2;
            if(parentIndex >= 0 && CompareStrings(_heap[parentIndex], _heap[i]) == 2)
            {
                string buffer = _heap[parentIndex];
                _heap[parentIndex] = _heap[i];
                _heap[i] = buffer;
                HeapifyBottomToTop(parentIndex);
            }
        }
        private void HeapifyTopToBottom(int i)
        {
            int leftChild = 2*i + 1;
            int rightChild = 2*i + 2;
            int min = 0;
            if(leftChild < _heapSize)
            {
                if(rightChild < _heapSize && CompareStrings(_heap[leftChild], _heap[rightChild]) == 2)
                {
                    min = rightChild;
                }
                else
                {
                    min = leftChild;
                }
            }
            else
            {
                return;
            }
            if(CompareStrings(_heap[min], _heap[i]) == 1)
            {
                string buffer = _heap[min];
                _heap[min] = _heap[i];
                _heap[i] = buffer;
                HeapifyTopToBottom(min);
            }
        }
        public string[] PyramidSort(string[] input, bool control)
        {
            if(_heap[0] != null)
            {
                ClearHeap();
            }
            for(int i = 0; i<input.Length; i++)
            {
                Insert(input[i]);
                if(control)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Insert '{input[i]}'...");
                    Console.ResetColor();
                    PrintHeap();
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Formed binary min heap:");
            Console.ResetColor();
            PrintHeap();
            for(int i = 0; i<input.Length; i++)
            {
                string extracted = ExtractMin();
                if(control)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Extract '{extracted}'...");
                    Console.ResetColor();
                    PrintHeap();
                }
            }
            string[] sorted = new string[input.Length];
            Array.Copy(_heap, sorted, input.Length);
            return sorted;
        }
        private void ClearHeap()
        {
            this._heap = new string[7];
            this._heapSize = 0; 
        }
        public void PrintHeap()
        {
            if(_heapSize == 0)
            {
                Console.WriteLine("Heap is empty.");
                return;
            }
            Console.WriteLine($"Root: {_heap[0]}");
            string whiteSpace = "   ";
            PrintChildren(0, whiteSpace);
        }
        private void PrintChildren(int i, string whiteSpace)
        {
            int leftChild = 2*i + 1;
            int rightChild = 2*i + 2;
            if(leftChild < _heapSize)
            {
                Console.WriteLine(whiteSpace + $"`----L: {_heap[leftChild]}");
                string newWhiteSpace = whiteSpace + "|    ";
                PrintChildren(leftChild, newWhiteSpace);
            }
            if(rightChild < _heapSize)
            {
                Console.WriteLine(whiteSpace + $"`----R: {_heap[rightChild]}");
                string newWhiteSpace = whiteSpace + "     ";
                PrintChildren(rightChild, newWhiteSpace);
            }
        }
        public int CompareStrings(string s1, string s2) //returns number of min string
        {
            int minLength = Math.Min(s1.Length, s2.Length);
            for(int i = 0; i<minLength; i++)
            {
                if((int)s1[i] < (int)s2[i])
                {
                    return 1;
                }
                else if((int)s1[i] > (int)s2[i])
                {
                    return 2;
                }
            }
            if(s1.Length == s2.Length)
            {
                return 0;
            }
            else if(minLength == s1.Length)
            {
                return 1;
            }
            return 2;
        }
    }
}