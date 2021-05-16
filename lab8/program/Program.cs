using System;

namespace program
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(@"Available options:
                1. Control example
                2. Enter data to sort
                3. Exit");
                Console.ResetColor();
                string command = Console.ReadLine();
                if(command == "1")
                {
                    string[] control = new string[]{"base", "airport", "queue", "city", "yellow", "anyone", "done", "exercise",
                                                    "zebra", "asd", "done", "queue"};
                    ProcessData(control, true);
                }
                else if(command == "2")
                {
                    Console.WriteLine("Enter number of strings:");
                    int n;
                    while(!int.TryParse(Console.ReadLine(),out n) || n <= 0)
                    {
                        Console.WriteLine("Error: Number of strings must be positive integer. Try again.");
                    }
                    string[] input = new string[n];
                    for(int i = 0; i<n; i++)
                    {
                        string s = "";
                        while(true)
                        {
                            Console.Write($"Enter {i+1} string: ");
                            s = Console.ReadLine();
                            s = s.ToLower();
                            if(!CheckInput(s))
                            {
                                Console.WriteLine("Error: Incorrect input format. Try again.");
                                continue;
                            }
                            break;
                        }
                        input[i] = s;
                    }
                    ProcessData(input, false);
                }
                else if(command == "3")
                {
                    Console.WriteLine("End.");
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Unavailable command. Try again.");
                    continue;
                }
            }
        }
        static void ProcessData(string[] input, bool isControl)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Input:");
            Console.ResetColor();
            PrintArray(input);
            BinaryMinHeap heap = new BinaryMinHeap();
            string[] sorted = heap.PyramidSort(input, isControl);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sorted:");
            Console.ResetColor();
            PrintArray(sorted);
        }
        static void PrintArray(string[] array)
        {
            for(int i = 0; i<array.Length; i++)
            {
                Console.WriteLine("   " + array[i]);
            }
        }
        static bool CheckInput(string s)
        {
            if(s.Length == 0)
            {
                return false;
            }
            for(int i = 0; i<s.Length; i++)
            {
                int charCode = (int)s[i];
                if(charCode < 97 || charCode > 122)
                {
                    return false;
                }
            }
            return true;
        }
    }
}