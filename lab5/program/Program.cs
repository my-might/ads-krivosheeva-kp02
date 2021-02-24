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
                Console.WriteLine(@"Available commands:
                1. Control strings
                2. Enter strings
                3. Exit");
                Console.ResetColor();
                string command = Console.ReadLine();
                if(command == "1")
                {
                    string[] input = new string[]{"AA12345F","AA12346D","AA12346H","AB34526X","AB34536G",
                                        "AC10000H","AC10000Y","AC10001H","XX00001X","XX00002X","YY00000Y"};
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("  Input:");
                    Console.ResetColor();
                    for(int i = 0; i<input.Length; i++)
                    {
                        Console.WriteLine(input[i]);
                    }
                    string[] sorted = SortArray(input);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" Sorted:");
                    Console.ResetColor();
                    for(int i = 0; i<sorted.Length; i++)
                    {
                        Console.WriteLine(sorted[i]);
                    }
                    continue;
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
                            Console.Write($"Enter {i} string in format: XX00000X ");
                            s = Console.ReadLine();
                            s = s.ToUpper();
                            while(!CheckInput(s))
                            {
                                Console.WriteLine("Error: Incorrect input format. Try again.");
                                Console.Write($"Enter {i} string in format: XX00000X ");
                                s = Console.ReadLine();
                            }
                            if(i != 0)
                            {
                                for(int j = 0; j<i; j++)
                                {
                                    if(input[j] == s)
                                    {
                                        Console.WriteLine("Error: All strings must be unique. Try again.");
                                        continue;
                                    }
                                }
                            }
                            break;
                        }
                        input[i] = s;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("  Input:");
                    Console.ResetColor();
                    for(int i = 0; i<input.Length; i++)
                    {
                        Console.WriteLine(input[i]);
                    }
                    string[] sorted = SortArray(input);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" Sorted:");
                    Console.ResetColor();
                    for(int i = 0; i<sorted.Length; i++)
                    {
                        Console.WriteLine(sorted[i]);
                    }
                    continue;
                }
                else if(command == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error: This command does not exist. Try again.");
                }
            }
            Console.WriteLine("End.");
        }
        static string[] SortArray(string[] s)
        {
            int columns = s[0].Length - 1;
            while(columns >= 0)
            {
                int max = 0;
                for(int i = 0; i<s.Length; i++)
                {
                    string current = s[i];
                    char cur = current[columns];
                    if((int)cur > max)
                    {
                        max = (int)cur;
                    }
                }
                int[] count = new int[max+1];
                for(int i = 0; i<s.Length; i++)
                {
                    string current = s[i];
                    char cur = current[columns];
                    count[(int)cur]++;
                }
                for(int i = count.Length - 1; i>0; i--)
                {
                    count[i-1] += count[i];
                }
                string[] sorted = new string[s.Length];
                for(int i = sorted.Length - 1; i>=0; i--)
                {
                    string current = s[i];
                    char cur = current[columns];
                    sorted[count[(int)cur]-1] = s[i];
                    count[(int)cur]--;
                }
                s = sorted;
                columns--;
            }
            return s;
        }
        static bool CheckInput(string s)
        {
            if(s.Length != 8)
            {
                return false;
            }
            for(int i = 0; i<s.Length; i++)
            {
                if(i == 0 || i == 1 || i == 7)
                {
                    bool letter = char.IsLetter(s[i]);
                    if(!letter)
                    {
                        return false;
                    }
                }
                else
                {
                    bool number = char.IsDigit(s[i]);
                    if(!number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
