using System;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter d1: ");
            int d1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter m1: ");
            int m1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter y1: ");
            int y1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter d2: ");
            int d2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter m2: ");
            int m2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter y2: ");
            int y2 = int.Parse(Console.ReadLine());
            if (correctdate(d1, m1, y1) && correctdate(d2, m2, y2))
            {
                int feb1;
                int feb2;
                int dy1;
                int dy2;
                if (leap(y1))
                {
                    feb1 = 29;
                    dy1 = 366;
                }
                else
                {
                    feb1 = 28;
                    dy1 = 365;
                }
                if (leap(y2))
                {
                    feb2 = 29;
                    dy2 = 366;
                }
                else
                {
                    feb2 = 28;
                    dy2 = 365;
                }
                int dm1 = daysinmonths(d1, m1, feb1) + d1;
                int dm2 = daysinmonths(d2, m2, feb2) + d2;
                int w = Math.Abs(y2 - y1);
                int daysbetween = 0;
                int yearsbetween = 0;
                int leaps = 0;
                if(y2>y1)
                {
                    for(int i=y1+1; i<y2; i++)
                    {
                        if(leap(i))
                            leaps++;
                    }
                }
                else
                {
                    for(int i=y2+1; i<y1; i++)
                    {
                        if(leap(i))
                            leaps++;
                    }
                }    
                if (w == 0)
                {
                    daysbetween = Math.Abs(dm1 - dm2);
                }
                else
                {
                    if (y2 > y1)
                        daysbetween = 366 * leaps + 365 * (w - leaps - 1) + (dy1 - dm1) + dm2;
                    else
                        daysbetween = 366 * leaps + 365 * (w - leaps - 1) + (dy2 - dm2) + dm1;
                }
                
                if(y1-y2!=0)
                {
                    if(y2>y1)
                    {    
                        if (dm2>=dm1)
                            yearsbetween = Math.Abs(y2 - y1);
                        else
                            yearsbetween = Math.Abs(y2 - y1) - 1;
                    }
                    else
                    {
                        if (dm1>=dm2)
                            yearsbetween = Math.Abs(y2 - y1);
                        else
                            yearsbetween = Math.Abs(y2 - y1) - 1;
                    }    
                }
                else
                {
                    yearsbetween = 0;
                }              
                Console.WriteLine("Days between dates: {0}", daysbetween);
                Console.WriteLine("Years between dates: {0}", yearsbetween);
            }
            else
            {
                Console.WriteLine("Wrong date format");
            }
        }
        static bool leap(int y)
        {
            if (y % 400 == 0)
            {
                return true;
            }
            else
            {
                if (y % 100 == 0)
                {
                    return false;
                }
                else if (y % 4 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static int daysinmonths(int d, int m, int feb)
        {
            int days = 0;
            if (m == 1)
            {
                days = 0;
            }
            else if (m <= 7)
            {
                if (m % 2 == 0)
                {
                    if (m == 2)
                        days = 31;
                    else
                        days = feb + 30 * (m - 2) + m / 2;
                }
                else
                {
                    days = feb + 30 * (m - 2) + (m - 1) / 2;
                }
            }
            else
            {
                if (m % 2 == 0)
                    days = 184 + feb + 61 * ((m - 8) / 2);
                else
                    days = 184 + feb + 31 * ((m - 7) / 2) + 30 * ((m - 9) / 2);
            }
            return days;
        }
        static bool correctdate(int d, int m, int y)
        {
            if (d > 0 && m > 0 && y > 0)
            {
                if (m <= 7)
                {
                    if (m % 2 == 0)
                    {
                        if (m == 2)
                        {
                            if (leap(y))
                            {
                                if (d <= 29)
                                    return true;
                                else
                                    return false;
                            }
                            else
                            {
                                if (d <= 28)
                                    return true;
                                else
                                    return false;
                            }
                        }
                        else
                        {
                            if (d <= 30)
                                return true;
                            else
                                return false;
                        }
                    }
                    else
                    {
                        if (d <= 31)
                            return true;
                        else
                            return false;
                    }
                }
                else if (m > 7 && m < 13)
                {
                    if (m % 2 == 0)
                    {
                        if (d <= 31)
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        if (d <= 30)
                            return true;
                        else
                            return false;
                    }
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}
