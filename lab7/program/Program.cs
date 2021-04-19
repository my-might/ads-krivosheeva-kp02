using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace program
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable table = new Hashtable();
            Hashtable forControl = new Hashtable();
            bool authorized = false;
            Key user = new Key();
            while(true)
            {
                if(!authorized)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(@"Available options:
                    1. Control example (how algorithm works)
                    2. Control insert (insert data to process on your own)
                    3. Add new user
                    4. Remove user by key
                    5. Find user by key
                    6. Show hash-table
                    7. Authorize
                    8. Exit");
                    Console.ResetColor();
                    string command = Console.ReadLine();
                    if(command == "1")
                    {
                        ProcessControlExample(forControl);
                        forControl.Clear();
                    }
                    else if(command == "2")
                    {
                        ProcessControlInsert(table);
                    }
                    else if(command == "3")
                    {
                        ProcessAdding(table);
                    }
                    else if(command == "4")
                    {
                        ProcessRemoving(table);
                    }
                    else if(command == "5")
                    {
                        ProcessFinding(table);
                    }
                    else if(command == "6")
                    {
                        table.PrintTable();
                    }
                    else if(command == "7")
                    {
                        user = ProcessAuthorize(table);
                        if(user.firstName != null)
                        {
                            authorized = true;
                        }
                    }
                    else if(command == "8")
                    {
                        Console.WriteLine("End.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Unavailable command. Try again.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(@"Available options:
                    1. Add friend
                    2. Remove friend
                    3. View my friend list
                    4. View my profile information
                    5. Exit from profile
                    6. Exit");
                    Console.ResetColor();
                    string command = Console.ReadLine();
                    if(command == "1")
                    {
                        ProcessAddingFriend(table, user);
                    }
                    else if(command == "2")
                    {
                        ProcessRemovingFriend(table, user);
                    }
                    else if(command == "3")
                    {
                        table.GetFriendsList(user);
                    }
                    else if(command == "4")
                    {
                        table.PrintUser(user);
                    }
                    else if(command == "5")
                    {
                        authorized = false;
                        Console.WriteLine("You are logged out of your profile.");
                    }
                    else if(command == "6")
                    {
                        Console.WriteLine("End.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Unavailable command. Try again.");
                    }
                }  
            }
        }
        static void ProcessControlExample(Hashtable table)
        {
            Entry e1 = new Entry(new Key("John", "Lock"), new Value("123456789", "a@gmail.com"));
            table.InsertEntry(e1.key, e1.value);
            Entry e2 = new Entry(new Key("Jack", "Hash"), new Value("12345678", "h@gmail.com"));
            table.InsertEntry(e2.key, e2.value);
            Entry e3 = new Entry(new Key("Hanna", "Ret"), new Value("1234567890", "r@gmail.com"));
            table.InsertEntry(e3.key, e3.value);
            Entry e4 = new Entry(new Key("Kate", "Kleq"), new Value("123456789j", "k@gmail.com"));
            table.InsertEntry(e4.key, e4.value);
            Entry e5 = new Entry(new Key("Valeria", "Kriv"), new Value("123456789i", "leraK@gmail.com"));
            table.InsertEntry(e5.key, e5.value);
            Entry e6 = new Entry(new Key("Ben", "Baw"), new Value("123456789g", "ben@gmail.com"));
            table.InsertEntry(e6.key, e6.value);
            Entry e7 = new Entry(new Key("Charlie", "Chak"), new Value("k123456789", "chak@gmail.com"));
            table.InsertEntry(e7.key, e7.value);
            Entry e8 = new Entry(new Key("Red", "Horp"), new Value("l123456789", "redHorp@gmail.com"));
            table.InsertEntry(e8.key, e8.value);   
            Console.WriteLine();  
            table.PrintTable();
            Console.WriteLine();
            Key user = e3.key;
            string password = e3.value.password;
            bool isAuthorized = table.Authorize(user, password);
            if(isAuthorized)
            {
                Console.WriteLine($"{e3.key.firstName} {e3.key.surname} authorized successfully.\n");
            } 
            else
            {
                Console.WriteLine("Error: Can`t authorize.");
                return;
            }
            table.GetFriendsList(user);
            Console.WriteLine();
            try
            {
                Console.WriteLine($"Adding '{e1.key.firstName} {e1.key.surname}' to friend list...");
                table.AddFriend(user, e1.key);
                Console.WriteLine("Friend was added successfully.\n");
                Console.WriteLine($"Adding '{e7.key.firstName} {e7.key.surname}' to friend list...");
                table.AddFriend(user, e7.key);
                Console.WriteLine("Friend was added successfully.\n");
                Console.WriteLine($"Adding '{e5.key.firstName} {e5.key.surname}' to friend list...");
                table.AddFriend(user, e5.key);
                Console.WriteLine("Friend was added successfully.\n");
                Console.WriteLine($"Adding '{e5.key.firstName} {e5.key.surname}' to friend list...");
                table.AddFriend(user, e5.key);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message + "\n");
            }
            Console.WriteLine("Friend list: ");
            table.GetFriendsList(user);
            Console.WriteLine();
            try
            {
                Console.WriteLine($"Removing '{e1.key.firstName} {e1.key.surname}' from friend list...");
                table.RemoveFriend(user, e1.key);
                Console.WriteLine("Friend was removed successfully.\n");
                Console.WriteLine($"Removing '{e7.key.firstName} {e7.key.surname}' from friend list...");
                table.RemoveFriend(user, e7.key);
                Console.WriteLine("Friend was removed successfully.\n");
                Console.WriteLine($"Removing '{e2.key.firstName} {e2.key.surname}' from friend list...");
                table.RemoveFriend(user, e2.key);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message + "\n");
            }
            Console.WriteLine("Friend list: ");
            table.GetFriendsList(user);
        }
        static void ProcessControlInsert(Hashtable table)
        {
            Entry e1 = new Entry(new Key("John", "Lock"), new Value("123456789", "a@gmail.com"));
            table.InsertEntry(e1.key, e1.value);
            Entry e2 = new Entry(new Key("Jack", "Hash"), new Value("12345678", "h@gmail.com"));
            table.InsertEntry(e2.key, e2.value);
            Entry e3 = new Entry(new Key("Hanna", "Ret"), new Value("1234567890", "r@gmail.com"));
            table.InsertEntry(e3.key, e3.value);
            Entry e4 = new Entry(new Key("Kate", "Kleq"), new Value("123456789j", "k@gmail.com"));
            table.InsertEntry(e4.key, e4.value);
            Entry e5 = new Entry(new Key("Valeria", "Kriv"), new Value("123456789i", "leraK@gmail.com"));
            table.InsertEntry(e5.key, e5.value);
            Entry e6 = new Entry(new Key("Ben", "Baw"), new Value("123456789g", "ben@gmail.com"));
            table.InsertEntry(e6.key, e6.value);
            Entry e7 = new Entry(new Key("Charlie", "Chak"), new Value("k123456789", "chak@gmail.com"));
            table.InsertEntry(e7.key, e7.value);
            Entry e8 = new Entry(new Key("Red", "Horp"), new Value("l123456789", "redHorp@gmail.com"));
            table.InsertEntry(e8.key, e8.value);  
        }
        static bool IsName(string input)
        {
            if(input.Length == 0)
            {
                return false;
            }
            foreach(char c in input)
            {
                if(!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
        static bool IsEmail(string input)
        {
            if(input.Length == 0)
            {
                return false;
            }
            try
            {
                MailAddress address = new MailAddress(input);
                return address.Address == input;
            }
            catch
            {
                return false;
            }
        }
        static bool IsPassword(string input)
        {
            if(input.Length < 8)
            {
                return false;
            }
            foreach(char c in input)
            {
                if(!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        static string GetFirstName()
        {
            while(true)
            {
                Console.WriteLine("Enter first name: ");
                string input = Console.ReadLine();
                bool isAvailable = IsName(input);
                if(isAvailable)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Error: Wrong data format. Try again.");
                    continue;
                }
            }
        }
        static string GetSurname()
        {
            while(true)
            {
                Console.WriteLine("Enter surname: ");
                string input = Console.ReadLine();
                bool isAvailable = IsName(input);
                if(isAvailable)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Error: Wrong data format. Try again.");
                    continue;
                }
            }
        }
        static void ProcessAdding(Hashtable table)
        {
            Entry entry = new Entry();
            entry.key.firstName = GetFirstName();
            entry.key.surname = GetSurname();
            while(true)
            {
                Console.WriteLine("Enter email adress: ");
                string input = Console.ReadLine();
                bool isAvailable = IsEmail(input);
                if(isAvailable)
                {
                    entry.value.emailAddress = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Wrong email format. Try again.");
                    continue;
                }
            }
            while(true)
            {
                Console.WriteLine("Create password, which contains at least 8 symbols: ");
                string input = Console.ReadLine();
                bool isAvailable = IsPassword(input);
                if(isAvailable)
                {
                    entry.value.password = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Wrong password format. Try again.");
                    continue;
                }
            }
            int result = table.InsertEntry(entry.key, entry.value);
            if(result == 1)
            {
                Console.WriteLine("User was added successfully.");
            }
            else if(result == 2)
            {
                Console.WriteLine("User`s information was updated.");
            }
            else if(result == 0)
            {
                Console.WriteLine("User wasn`t added.");
            }
        }
        static void ProcessRemoving(Hashtable table)
        {
            Key key = new Key();
            key.firstName = GetFirstName();
            key.surname = GetSurname();
            try
            {
                table.RemoveEntry(key);
                Console.WriteLine("User was removed successufully.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        static void ProcessFinding(Hashtable table)
        {
            Key key =  new Key();
            key.firstName = GetFirstName();
            key.surname = GetSurname();
            Entry found = table.FindEntry(key);
            if(found.value.password == null)
            {
                Console.WriteLine("Error: User not found.");
            }
            else
            {
                Console.Write("Found user: ");
                table.PrintUser(key);
            }
        }
        static Key ProcessAuthorize(Hashtable table)
        {
            string firstName = GetFirstName();
            string surname = GetSurname();
            Key user = new Key();
            user.firstName = firstName;
            user.surname = surname;
            string password = "";
            while(true)
            {
                Console.WriteLine("Enter your password: ");
                password = Console.ReadLine();
                if(IsPassword(password))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Wrong password format. Try again.");
                    continue;
                }
            }
            bool isAuthorized = table.Authorize(user, password);
            if(isAuthorized)
            {
                Console.WriteLine("Authorized successfully!");
            }
            else
            {
                Console.WriteLine("Error: Login or password is not correct.");
                user = new Key();
            }
            return user;
        }
        static void ProcessAddingFriend(Hashtable table, Key user)
        {
            string firstName = GetFirstName();
            string surname = GetSurname();
            Key friend = new Key();
            friend.firstName = firstName;
            friend.surname = surname;
            try
            {
                table.AddFriend(user, friend);
                Console.WriteLine("Friend was added successfully.");
                return;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return;
            }
        }
        static void ProcessRemovingFriend(Hashtable table, Key user)
        {
            string firstName = GetFirstName();
            string surname = GetSurname();
            Key friend = new Key();
            friend.firstName = firstName;
            friend.surname = surname;
            try
            {
                table.RemoveFriend(user, friend);
                Console.WriteLine("Friend was removed successfully.");
                return;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return;
            }
        }
    }
    struct Key
    {
        public string firstName;
        public string surname;
        public Key(string firstName, string surname)
        {
            this.firstName = firstName;
            this.surname = surname;
        }
    }
    struct Value
    {
        public string password;
        public string emailAddress;
        public LinkedList<Key> friends;
        public Value(string password, string emailAddress)
        {
            this.password = password;
            this.emailAddress = emailAddress;
            this.friends = new LinkedList<Key>();
        }
    }
    struct Entry
    {
        public Key key;
        public Value value;
        public Entry(Key key, Value value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
