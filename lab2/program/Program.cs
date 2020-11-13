using System;

namespace program
{
    public struct Element
    {
        public int row;
        public int column;
        public int value;
        public Element(int row0, int collumn0, int value0)
        {
            row = row0;
            column = collumn0;
            value = value0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter N: ");
            int n = int.Parse(Console.ReadLine());
            int[,] arr = new int[n, n];
            if(correctdata(n))
            {
                bool condition = true;
                while(condition == true)
                {
                    Console.WriteLine("Commands: 'random'- generate matrix, 'control' - control matrix, 'exit' - exit");
                    string command = Console.ReadLine();
                    if(command == "random")
                    {
                        Console.WriteLine();
                        arr = randomMatrix(n);
                        Console.WriteLine();
                        Console.WriteLine("Traversal: ");
                        Traversal(arr, n);
                        Console.WriteLine();
                    }
                    else if(command == "control")
                    {
                        Console.WriteLine();
                        arr = controlMatrix(n);
                        Console.WriteLine();
                        Console.WriteLine("Traversal: ");
                        Traversal(arr, n);
                        Console.WriteLine();
                    }    
                    else if(command == "exit")
                    {
                        condition = false;
                    } 
                    else
                    {
                        Console.WriteLine("Command does not exist");
                    }
                }
            }
            else
            {
                Console.WriteLine("Unavailable N value");
            }

        }
        static bool correctdata(int n)
        {
            if(n>0)
                return true;
            else
                return false;
        }
        static int[,] randomMatrix(int n)
        {
            int[,] arr = new int[n, n];
            Random ran = new Random();
            for(int i=0; i<n; i++)
            {
                for(int j=0; j<n; j++)
                {
                    arr[i,j] = ran.Next(10,100);
                    Console.Write("{0} ", arr[i,j]);
                }
                Console.WriteLine();
            }  
            return arr;
        }
        static int[,] controlMatrix(int n)
        {
            int[,] arr = new int[n, n];
            int r = 0;
            for(int i=0; i<n; i++)
            {
                for(int j=0; j<n; j++)
                {
                    arr[i,j] = r;
                    Console.Write("{0,4}", r);
                    r = r+1;
                }
            Console.WriteLine();
            }        
            return arr;
        }
        static void Traversal(int[,] arr, int n)
        {
            Element min1 = new Element(0, 0, 10000);
            Element max1 = new Element(0, 0, 0);
            Element min2 = new Element(0, 0, 10000);
            Element max2 = new Element(0, 0, 0);
            Element min3 = new Element(0, 0, 10000);
            Element max3 = new Element(0, 0, 0);
            int c = 0;
            for(int i=(n-1); i>=0; i--)
            {

                if(c%2==0)
                {
                    for(int j=0;j<=(i-1);j++)
                    {    
                        Element curElem = new Element(i, j, arr[i,j]);
                        CheckMax(curElem, ref max1);
                        CheckMin(curElem, ref min1);
                        Console.Write("{0} ", arr[i,j]);
                    }
                }
                else
                {
                    for(int j=(i-1);j>=0;j--)
                    {
                        Element curElem = new Element(i, j, arr[i,j]);
                        CheckMax(curElem, ref max1);
                        CheckMin(curElem, ref min1);
                        Console.Write("{0} ", arr[i,j]);
                    }
                }
                
                c++;
            }
            for(int i=0; i<=(n-1); i++)
            {
                int j = i;
                Element curElem = new Element(i, j, arr[i,j]);
                CheckMax(curElem, ref max2);
                CheckMin(curElem, ref min2);
                Console.Write("{0} ", arr[i,j]);
            }
            int h = 0;
            for(int j=(n-1); j>0; j--)
            {
                if(h%2==0)
                {
                    for(int i=(j-1); i>=0; i--)
                    {
                        Element curElem = new Element(i, j, arr[i,j]);
                        CheckMax(curElem, ref max3);
                        CheckMin(curElem, ref min3);
                        Console.Write("{0} ", arr[i,j]);
                    }
                }
                else
                {
                    for(int i=0; i<=(j-1); i++)
                    {
                        Element curElem = new Element(i, j, arr[i,j]);
                        CheckMax(curElem, ref max3);
                        CheckMin(curElem, ref min3);
                        Console.Write("{0} ", arr[i,j]);
                    }
                }
                h++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Under the diagonal: min[{0},{1}], max[{2},{3}]", min1.row, min1.column, max1.row, max1.column);
            Console.WriteLine("On the diagonal: min[{0},{1}], max[{2},{3}]", min2.row, min2.column, max2.row, max2.column);
            Console.WriteLine("Over the diagonal: min[{0},{1}], max[{2},{3}]", min3.row, min3.column, max3.row, max3.column);
        }
        static void CheckMax(Element curElem, ref Element element)
        {
            if(curElem.value>element.value)
            {
                element = curElem;
            }
        }
        static void CheckMin(Element curElem, ref Element element)
        {
            if(curElem.value<element.value)
            {
                element = curElem;
            }
        }
    }
}
