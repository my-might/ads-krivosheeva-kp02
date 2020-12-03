using System;

namespace program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter N: ");
            int n;
            bool n1 = int.TryParse(Console.ReadLine(), out n);
            if(n1 == false || n<=0)
            {
                Console.WriteLine("Unavailable N value.");
                Environment.Exit(1);
            }
            Console.Write("Enter M: ");
            int m;
            bool m1 = int.TryParse(Console.ReadLine(), out m);
            if(m1 == false || m<=0)
            {
                Console.WriteLine("Unavailable M value.");
                Environment.Exit(1);
            }
            if(n*m > 900)
            {
                Console.WriteLine("There are not enough elements to be unique.");
                Environment.Exit(1);
            }
            int[,] arr = new int[n,m];
            Random random = new Random();
            int count = 0;
            for(int i = 0; i<n; i++)
            {
                for(int j = 0; j<m; j++)
                {
                    arr[i,j] = random.Next(100,1000);
                    do
                    {
                        count = 0;
                        foreach(int item in arr)
                        {
                            if(item == arr[i,j])
                            count++;
                        }
                        if(count>1)
                        arr[i,j] = random.Next(100,1000);
                    }
                    while(count!=1);
                }
            }
            Console.WriteLine("Input array: ");
            PrintArray(arr);
            Console.WriteLine();
            for(int i = 1; i<n; i++)
            {
                for(int j = 0; j<m; j++)
                {
                    if(i==j && i+j!=m-1)
                    {
                        int key = arr[i,j];
                        int k = i - 1;
                        if(2*k==m-1)
                        {
                            k = i-2;
                        }
                        while (k>=0 && arr[k,k]<key)
                        {
                            if(2*(k+1)==m-1)
                            {
                                arr[k+2, k+2] = arr[k,k];
                            }
                            else
                            {
                                arr[k+1, k+1] = arr[k,k];
                            }
                            k = k-1;
                            if(2*k==m-1)
                            {
                                k = k-1;
                            }
                        }
                        if(2*(k+1)==m-1)
                        {
                            arr[k+2,k+2] = key;
                        }
                        else
                        {
                            arr[k+1,k+1] = key;
                        }
                    }
                    else if(i+j==m-1 && i!=j)
                    {
                        int key = arr[i,j];
                        int k = i - 1;
                        int l = j + 1;
                        if(k==l)
                        {
                            k = i-2;
                            l = j+2;
                        }
                        while (k>=0 && arr[k,l]>key)
                        {
                            if(k+1==l-1)
                            {
                                arr[k+2, l-2] = arr[k,l];
                            }
                            else
                            {
                                arr[k+1, l-1] = arr[k,l];
                            }
                            k = k-1;
                            l = l+1;
                            if(k==l)
                            {
                                k = k-1;
                                l = l+1;
                            }
                        }
                        if(k+1==l-1)
                        {
                            arr[k+2,l-2] = key;
                        }
                        else
                        {
                            arr[k+1,l-1] = key;
                        }
                    }
                }
            }
            Console.WriteLine("Sorted array: ");
            PrintArray(arr);
        }
        static void PrintArray(int[,] arr)
        {
            int n = arr.GetLength(0);
            int m = arr.GetLength(1);
            for(int i = 0; i<n; i++)
            {
                for(int j = 0; j<m; j++)
                {
                    if(i==j && i+j == m-1)
                    {
                        Console.Write("{0} ", arr[i,j]);
                    }
                    else if(i==j)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("{0} ", arr[i,j]);
                        Console.ResetColor();
                    }
                    else if(i+j == m-1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write("{0} ", arr[i,j]);
                        Console.ResetColor();
                    }
                    else 
                    {
                        Console.Write("{0} ", arr[i,j]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
