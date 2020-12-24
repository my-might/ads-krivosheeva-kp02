using System;

namespace program
{
    class Program
    {
        static void Main(string[] args)
        {
            DLList node = new DLList();
            while(true)
            {
                Console.WriteLine(@"Available commands:
                1. addFirst
                2. addLast
                3. addAtPosition
                4. deleteFirst
                5. deleteLast
                6. deleteAtPosition
                7. addAfterMinimal
                8. deleteEvenNumsAndCreateNewList
                9. print
                10. exit");
                Console.Write("Enter command: ");
                string command = Console.ReadLine();
                if(command == "1")
                {
                    Console.Write("Enter data: ");
                    int data;
                    if(!int.TryParse(Console.ReadLine(), out data))
                    {
                        Console.WriteLine("Data must be integer. Try again.");
                        continue;
                    }
                    node.AddFirst(data);
                    node.Print();
                }
                else if(command == "2")
                {
                    Console.Write("Enter data: ");
                    int data;
                    if(!int.TryParse(Console.ReadLine(), out data))
                    {
                        Console.WriteLine("Data must be integer. Try again.");
                        continue;
                    }
                    node.AddLast(data);
                    node.Print();
                }
                else if(command == "3")
                {
                    Console.Write("Enter data: ");
                    int data;
                    if(!int.TryParse(Console.ReadLine(), out data))
                    {
                        Console.WriteLine("Data must be integer. Try again.");
                        continue;
                    }
                    Console.Write("Enter position: ");
                    int position;
                    if(!int.TryParse(Console.ReadLine(), out position))
                    {
                        Console.WriteLine("Position must be integer. Try again.");
                        continue;
                    }
                    node.AddAtPosition(data, position);
                    node.Print();
                }
                else if(command == "4")
                {
                    node.DeleteFirst();
                    node.Print();
                }
                else if(command == "5")
                {
                    node.DeleteLast();
                    node.Print();
                }
                else if(command == "6")
                {
                    Console.Write("Enter position: ");
                    int position;
                    if(!int.TryParse(Console.ReadLine(), out position))
                    {
                        Console.WriteLine("Position must be integer. Try again.");
                        continue;
                    }
                    node.DeleteAtPosition(position);
                    node.Print();
                }
                else if(command == "7")
                {
                    Console.Write("Enter data: ");
                    int data;
                    if(!int.TryParse(Console.ReadLine(), out data))
                    {
                        Console.WriteLine("Data must be integer. Try again.");
                        continue;
                    }
                    node.AddAfterMinimal(data);
                    node.Print();
                }
                else if(command == "8")
                {
                    node.DeleteEvenNums();
                }
                else if(command == "9")
                {
                    node.Print();
                }
                else if(command == "10")
                {
                    Console.WriteLine("End.");
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect command. Try again.");
                }
                Console.WriteLine();
            }
        }
    }
    class DLList
    {
        private Node tail;
        private int size;
        public int Size => size;

        private class Node
        {
            public int data;
            public Node next;
            public Node prev;

            public Node(int data)
            {
                this.data = data;
            }
        }
        public void AddFirst(int data)
        {
            Node first = new Node(data);

            if (tail == null)
            {
                tail = first;
            }
            else
            {
                Node current = tail;
                while(current.prev != null)
                {
                    current = current.prev;
                }
                current.prev = first;
                first.next = current;
            }
            size++;
        }
        public void AddLast(int data)
        {
            Node last = new Node(data);

            if (tail == null)
            {
                tail = last;
            }
            else
            {
                Node current = tail;
                current.next = last;
                last.prev = current;
                tail = last;
            }
            size++;
        }
        public void AddAtPosition(int data, int position)
        {
            if(position<0 || position > size)
            {
                Console.WriteLine("Position is outside the borders.");
                return;
            }
            if(position == 0)
            {
                AddFirst(data);
                return;
            }
            else if(position == size)
            {
                AddLast(data);
                return;
            }
            else
            {
                Node current = tail;
                Node toAdd = new Node(data);
                int count = 0;
                while(count != size - position)
                {
                    current = current.prev;
                    count++;
                }
                toAdd.next = current.next;
                current.next = toAdd;
                toAdd.prev = current;
                toAdd.next.prev = toAdd;
            }
            size++;
        }
        public void DeleteFirst()
        {
            if(tail != null)
            {
                if(tail.prev == null)
                {
                    tail = null;
                }
                else
                {
                    Node current = tail;
                    while(current.prev != null)
                    {
                        current = current.prev;
                    }
                    current.next.prev = null;
                    current.next = null;
                }
                size--;
            }
            else
            {
                Console.WriteLine("There are no elements to delete. Try again.");
                return;
            }
        }
        public void DeleteLast()
        {
            if(tail != null)
            {
                if(tail.prev == null)
                {
                    tail = null;
                }
                else
                {
                    tail = tail.prev;
                    tail.next = null;
                }
                size--;
            }
            else
            {
                Console.WriteLine("There are no elements to delete. Try again.");
                return;
            }
        }
        public void DeleteAtPosition(int position)
        {
            if(position<0 || position > size)
            {
                Console.WriteLine("Position is outside the borders.");
                return;
            }
            if(position == 0)
            {
                DeleteFirst();
                return;
            }
            else if(position == size - 1)
            {
                DeleteLast();
                return;
            }
            else
            {
                Node current = tail;
                int count = 0;
                while(count != size - position - 1)
                {
                    current = current.prev;
                    count++;
                }
                current.prev.next = current.next;
                current.next.prev = current.prev;
            }
            size--;
        }
        public void AddAfterMinimal(int data)
        {
            if(size == 0)
            {
                AddFirst(data);
                return;
            }
            double min = double.PositiveInfinity;
            Node current = tail;
            int counter = 0;
            int position = 0;
            while(current != null)
            {
                if(current.data < min)
                {
                    min = current.data;
                    position = size - counter;
                }
                counter++;
                current = current.prev;
            }
            AddAtPosition(data, position);
        }
        public void DeleteEvenNums()
        {
            if(tail == null)
            {
                Console.WriteLine("There are no elements in list. Try again.");
                return;
            }
            SLList copy = new SLList();
            Node current = tail;
            while(current.prev != null)
            {
                current = current.prev;
            }
            int counter = 0;
            int startSize = size;
            int position = 0;
            while(current != null)
            {
                if(current.data % 2 == 0)
                {
                    copy.AddToNewList(current.data);
                    current = current.next;
                    position = counter - (startSize - size);
                    DeleteAtPosition(position);
                }
                else
                {
                    current = current.next;
                }
                counter++;
                Console.Write("Deleting...");
                Print();
                Console.Write("Adding...");
                copy.Print();
            }
            Console.WriteLine();
            Console.Write("Result:");
            Print();
            copy.Print();
            Console.WriteLine();
        }

        public void Print()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            if(tail == null)
            {
                Console.Write("Current list is empty.");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }
            Node current = tail;
            while(current.prev != null)
            {
                current = current.prev;
            }
            Console.Write("Current list: ");
            while (current != null)
            {
                Console.Write(current.data);
                if (current.next != null)
                {
                    Console.Write(" <--> ");
                }
                current = current.next;
            }
            Console.ResetColor();
            Console.WriteLine();
        }

    }
    class SLList
    {
        public SLNode head;
        public int size;
        public int Size => size;
        public class SLNode
        {
            public int data;
            public SLNode next;
            public SLNode(int data)
            {
                this.data = data;
            }
        }
        public void AddToNewList(int data)
        {
            SLNode add = new SLNode(data);
            if(head == null)
            {
                head = add;
            }
            else if(size == 2)
            {
                SLNode current = head;
                add.next = current;
                head = add;
            }
            else if(size%2 == 0)
            {
                SLNode current = head;
                for(int i = 1; i<size/2 - 1; i++)
                {
                    current = current.next;
                }
                add.next = current.next;
                current.next = add;
            }
            else
            {
                SLNode current = head;
                add.next = current.next;
                current.next = add;
            }
            size++;
        }
        public void Print()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            if(head == null)
            {
                Console.Write("Current list of deleted is empty.");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }
            Console.Write("Current list of deleted: ");
            SLNode current = head;
            while (current != null)
            {
                Console.Write(current.data);
                if (current.next != null) Console.Write(" -> ");
                current = current.next;
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
