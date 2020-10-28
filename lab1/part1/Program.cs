using System;



    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter x: ");
            double x = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter y: ");
            double y = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter z: ");
            double z = double.Parse(Console.ReadLine());
            if(x==-z || y==x || Math.Abs(y-x)==Math.Pow(Math.E, -2))
            {
                Console.WriteLine("Error");
            }
            else
            {
                double a = 1+Math.Log10(Math.Abs(x+z))/(1+Math.Log(Math.Abs(y-x), Math.E)/2);
                double b = 1/(Math.Pow(a, 4)+1);
                Console.WriteLine("a = {0}", a);
                Console.WriteLine("b = {0}", b);
            }
        }
    }

