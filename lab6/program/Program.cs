using System;

namespace program
{
    class Queue
    {
        public int[] queue;
        public int size;
        public int head;
        public int tail;
        public Queue()
        {
            this.size = 0;
            this.head = 0;
            this.tail = 0;
            this.queue = new int[4];
        }
        private int GetNextIndex()
        {
            int index = (this.tail + 1)%queue.Length;
            return index;
        }
        public void Enqueue(int data)
        {
            int index = GetNextIndex();
            if(IsQueueEmpty())
            {
                queue[0] = data;
            }
            else if(index == head)
            {
                throw new Exception("The queue is full. Try again.");
            }
            else
            {
                queue[index] = data;
                this.tail = index;
            }
            this.size++;
        }
        public void Dequeue()
        {
            if(IsQueueEmpty())
            {
                throw new Exception("There are no elements to delete in the queue.");
            }
            int prevHead = this.head;
            if(this.head + 1 == queue.Length)
            {
                this.head = 0;
            }
            else
            {
                this.head++;
            }
            queue[prevHead] = 0;
            this.size--;
            if(IsQueueEmpty())
            {
                throw new Exception("The queue is empty. End.");
            }
        }
        public bool IsQueueEmpty()
        {
            if(this.size == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PrintQueue()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Current queue: ");
            Console.ResetColor();
            if(head <= tail)
            {
                for(int i = head; i<= tail; i++)
                {
                    Console.Write($"{queue[i]} ");
                }
            }
            else
            {
                for(int i = head; i<queue.Length; i++)
                {
                    Console.Write($"{queue[i]} ");
                }
                for(int i = 0; i<=tail; i++)
                {
                    Console.Write($"{queue[i]} ");
                }
            }
            Console.WriteLine(", current capacity: {0}", queue.Length);
        }
        public void EnsureCapacity()
        {
            int newCapacity = queue.Length *2;
            int[] oldQueue = queue;
            queue = new int[newCapacity];
            if(tail<head)
            {
                for(int i=0;i<=tail;i++)
                {
                    queue[i] = oldQueue[i];
                }
                this.head = this.head + oldQueue.Length;
                for(int i=head;i<newCapacity;i++)
                {
                    queue[i] = oldQueue[i-oldQueue.Length];
                }
            }
            else
            {
                for(int i=0;i<oldQueue.Length;i++)
                {
                    queue[i] = oldQueue[i];
                }
            }
        }
        public int Head()
        {
            return queue[head];
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(@"Available commands:
                1. Control queue
                2. Enter queue
                3. Exit");
                Console.ResetColor();
                string command = Console.ReadLine();
                if(command == "1")
                {
                    int[] control = new int[]{1,2,3,4,0,5,6,7,8,0,9,0};
                    Queue q = new Queue();
                    for(int i = 0; i<control.Length; i++)
                    {
                        Console.WriteLine($"Process {control[i]}:");
                        ProcessNumber(q, control[i]);
                        if(q.IsQueueEmpty())
                        {
                            Environment.Exit(0);
                        }
                    }
                }
                else if(command == "2")
                {
                    Queue q = new Queue();
                    while(true)
                    {
                        Console.Write("Enter number: ");
                        string input = Console.ReadLine();
                        if(!int.TryParse(input, out int n))
                        {
                            Console.WriteLine("Input number must be integer. Try again.");
                            continue;
                        }
                        if(n < 0)
                        {
                            Console.WriteLine("Input number must be natural number or zero. Try again.");
                            continue;
                        }
                        ProcessNumber(q, n);
                        if(q.IsQueueEmpty())
                        {
                            Environment.Exit(0);
                        }
                    }
                }
                else if(command == "3")
                {
                    Console.WriteLine("End.");
                    break;
                }
                else
                {
                    Console.WriteLine("Unavailable command. Try again.");
                }
            }
            static void ProcessNumber(Queue q, int n)
            {
                if(n == 0)
                {
                    int counter = 0;
                    int size = q.size;
                    while(counter < 3)
                    {
                        try
                        {
                            counter++;
                            q.Dequeue();
                            if(!q.IsQueueEmpty())
                            {
                                q.PrintQueue();
                            }
                            if(counter == 3)
                            {
                                Console.WriteLine("3 elements were deleted from the queue.");
                            }
                        }
                        catch(Exception ex)
                        {
                            if(size == 0)
                            {
                                counter--;
                            }
                            if(counter != 0)
                            {
                                Console.WriteLine($"{counter} elements were deleted from the queue.");
                            }
                            Console.WriteLine(ex.Message);
                            break;
                        }
                    }
                    return;
                }
                else
                {
                    try
                    {
                        q.Enqueue(n);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        q.EnsureCapacity();
                        return;
                    }
                }
                q.PrintQueue();
            }
        }
    }
}
