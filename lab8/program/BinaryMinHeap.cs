using System;
namespace program
{
    public class BinaryMinHeap
    {
        private string[] heap;
        private int heapSize;
        public BinaryMinHeap()
        {
            this.heap = new string[7];
            this.heapSize = 0;
        }
        public void Insert(string s)
        {
            if(heap.Length == heapSize)
            {
                Resize();
            }
            int position = heapSize;
            heap[heapSize] = s;
            heapSize++;
            HeapifyBottomToTop(position);
        }
        public string ExtractMin()
        {
            if(heapSize == 0)
            {
                throw new Exception("Cannot extract, heap is empty.");
            }
            string min = heap[0];
            heap[0] = heap[heapSize - 1];
            heap[heapSize - 1] = min;
            heapSize--;
            HeapifyTopToBottom(0);
            return min;
        }
        private void Resize()
        {
            Array.Resize(ref heap, heap.Length*2);
        }
        private void HeapifyBottomToTop(int i)
        {
            int parentIndex = (i-1)/2;
            if(parentIndex >= 0 && CompareStrings(heap[parentIndex], heap[i]) == 2)
            {
                string buffer = heap[parentIndex];
                heap[parentIndex] = heap[i];
                heap[i] = buffer;
                HeapifyBottomToTop(parentIndex);
            }
        }
        private void HeapifyTopToBottom(int i)
        {
            int leftChild = 2*i + 1;
            int rightChild = 2*i + 2;
            int min = 0;
            if(leftChild < heapSize)
            {
                if(rightChild < heapSize && CompareStrings(heap[leftChild], heap[rightChild]) == 2)
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
            if(CompareStrings(heap[min], heap[i]) == 1)
            {
                string buffer = heap[min];
                heap[min] = heap[i];
                heap[i] = buffer;
                HeapifyTopToBottom(min);
            }
        }
        public string[] PyramidSort(string[] input, bool control)
        {
            if(heap[0] != null)
            {
                ClearHeap();
            }
            for(int i = 0; i<input.Length; i++)
            {
                Insert(input[i]);
                if(control)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Insert {input[i]}...");
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
                    Console.WriteLine($"Extract {extracted}...");
                    Console.ResetColor();
                    PrintHeap();
                }
            }
            string[] sorted = new string[input.Length];
            Array.Copy(heap, sorted, input.Length);
            return sorted;
        }
        private void ClearHeap()
        {
            this.heap = new string[7];
            this.heapSize = 0; 
        }

        public void PrintHeap()
        {
            if(heapSize == 0)
            {
                Console.WriteLine("Heap is empty.");
                return;
            }
            Console.WriteLine($"Root: {heap[0]}");
            string whiteSpace = "   ";
            PrintChildren(0, whiteSpace);
        }
        private void PrintChildren(int i, string whiteSpace)
        {
            int leftChild = 2*i + 1;
            int rightChild = 2*i + 2;
            if(leftChild < heapSize)
            {
                Console.WriteLine(whiteSpace + $"`----L: {heap[leftChild]}");
                string newWhiteSpace = whiteSpace + "|    ";
                PrintChildren(leftChild, newWhiteSpace);
            }
            if(rightChild < heapSize)
            {
                Console.WriteLine(whiteSpace + $"`----R: {heap[rightChild]}");
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
            if(minLength == s1.Length && minLength == s2.Length)
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