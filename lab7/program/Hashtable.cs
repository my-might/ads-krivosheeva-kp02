using System;
using System.Collections.Generic;
namespace program
{
    class Hashtable
    {
        private Entry[] _table;
        private double _loadness;
        private int _size;
        private bool _rehashing;
        public Hashtable()
        {
            this._table = new Entry[13];
            this._loadness = 0;
            this._size = 0;
            this._rehashing = false;
        }
        public int InsertEntry(Key key, Value value)
        {
            if(_loadness > 0.5)
            {
                Console.WriteLine("Process rehashing...");
                _rehashing = true;
                Rehashing();
                _rehashing = false;
                Console.WriteLine("Rehashed successfully.");
            }
            int hash1 = GetHash1(key);
            int hash2 = GetHash2(key);
            for(int i = 0; i<_table.Length; i++)
            {
                int h = (hash1 + i*hash2) % _table.Length;
                if((_table[h].value.password == null || _table[h].value.password == "DELETED") && FindEntry(key).value.password == null)
                {
                    _table[h].key = key;
                    if(_rehashing)
                    {
                        _table[h].value.password = value.password;
                        _table[h].value.friends = value.friends;
                    }
                    else
                    {
                        _table[h].value.password = PasswordHashCode(value.password);
                        _table[h].value.friends = new LinkedList<Key>();
                    }
                    _table[h].value.emailAddress = value.emailAddress;
                    _size++;
                    _loadness = (double)_size/_table.Length;
                    return 1;
                }
                else if(_table[h].key.Equals(key))
                {
                    _table[h].value.password = PasswordHashCode(value.password);
                    _table[h].value.emailAddress = value.emailAddress;
                    return 2;
                }
            }
            return 0;
        }
        public void RemoveEntry(Key key)
        {
            if(FindEntry(key).value.password == null)
            {
                throw new Exception("User doesn`t exist in the system.");
            }
            int hash1 = GetHash1(key);
            int hash2 = GetHash2(key);
            for(int i = 0; i<_table.Length; i++)
            {
                int h = (hash1 + i*hash2) % _table.Length;
                if(key.Equals(_table[h].key))
                {
                    RemoveFromFriends(_table[h].key);
                    _table[h].key.firstName = null;
                    _table[h].key.surname = null;
                    _table[h].value.password = "DELETED";
                    _table[h].value.emailAddress = null;
                    _table[h].value.friends = null;
                    _size--;
                    _loadness = (double)_size/_table.Length;
                    return;
                }
            }
        }
        private void RemoveFromFriends(Key key)
        {
            for(int i = 0; i<_table.Length; i++)
            {
                if(_table[i].value.password != null && _table[i].value.password != "DELETED")
                {
                    if(_table[i].value.friends.Contains(key))
                    {
                        _table[i].value.friends.Remove(key);
                    }
                }
            }
        }
        public Entry FindEntry(Key key)
        {
            int hash1 = GetHash1(key);
            int hash2 = GetHash2(key);
            Entry nullEntry = new Entry();
            for(int i = 0; i<_table.Length; i++)
            {
                int h = (hash1 + i*hash2) % _table.Length;
                if(key.Equals(_table[h].key))
                {
                    return _table[h];
                }
                else if(_table[h].value.password == "DELETED")
                {
                    continue;
                }
                else if(_table[h].key.firstName == null)
                {
                    break;
                }
            }
            return nullEntry;
        }
        private void Rehashing()
        {
            Entry[] oldTable = _table;
            int oldCapacity = _table.Length;
            _table = new Entry[oldCapacity*2];
            _loadness = 0;
            _size = 0;
            for(int i = 0; i<oldCapacity; i++)
            {
                if(oldTable[i].key.firstName != null)
                {
                    InsertEntry(oldTable[i].key, oldTable[i].value);
                }
            }
        }
        public void PrintUser(Key key)
        {
            Entry entry = FindEntry(key);
            Console.Write("{0} {1}, email: {2}, password: {3}, ", entry.key.firstName, entry.key.surname,
                            entry.value.emailAddress, entry.value.password);
            PrintFriends(key);
            Console.WriteLine();
        }
        private void PrintFriends(Key user)
        {
            Entry userInfo = FindEntry(user);
            if(userInfo.value.friends.Count == 0)
            {
                Console.Write("friend list is empty,");
                return;
            }
            Key[] array = new Key[userInfo.value.friends.Count];
            userInfo.value.friends.CopyTo(array, 0);
            Console.Write("friends: ");
            foreach(Key k in array)
            {
                Console.Write("{0} {1}, ", k.firstName, k.surname);
            }
        }
        public void PrintTable()
        {
            if(_size == 0)
            {
                Console.WriteLine("Hashtable is empty.");
                return;
            }
            Console.WriteLine("Current users: ");
            for(int i = 0; i<_table.Length; i++)
            {
                if(_table[i].key.firstName != null)
                {
                    Console.Write("{0} {1}, email: {2}, password: {3}, ", _table[i].key.firstName, _table[i].key.surname,
                                     _table[i].value.emailAddress, _table[i].value.password);
                    PrintFriends(_table[i].key);
                    Console.WriteLine();
                }
            }
        }
        private long HashCode(Key key)
        {
            string toHash = key.firstName + key.surname;
            long hash = 0;
            for(int i = 0; i<toHash.Length; i++)
            {
                hash += (int)toHash[i] * (i+1) * 83;
            } 
            return hash;
        }
        private string PasswordHashCode(string password)
        {
            string toHash = password;
            long hash = 0;
            const int N = 26;
            int current = 0;
            for(int i = toHash.Length - 1; i>=0; i--)
            {
                hash += ((int)toHash[current]-47) * (long)Math.Pow(N, i);
                current++;
            }
            return hash.ToString();
        }
        public bool Authorize(Key key, string password)
        {
            Entry user = FindEntry(key);
            if(user.value.password == PasswordHashCode(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddFriend(Key user, Key friend)
        {
            if(user.Equals(friend))
            {
                throw new Exception("You can`t add yourself to your friend list.");
            }
            Entry friendInfo = FindEntry(friend);
            if(friendInfo.value.password == null)
            {
                throw new Exception("User doesn`t exist in the system.");
            }
            Entry userInfo = FindEntry(user);
            if(userInfo.value.friends.Contains(friend))
            {
                throw new Exception("User is already in your friend list.");
            }
            userInfo.value.friends.AddLast(friend);
        }
        public void RemoveFriend(Key user, Key friend)
        {
            Entry userInfo = FindEntry(user);
            if(userInfo.value.friends.Count == 0)
            {
                throw new Exception("Your friend list is empty.");
            }
            Entry friendInfo = FindEntry(friend);
            if(friendInfo.value.password == null)
            {
                throw new Exception("User doesn`t exist in the system.");
            }
            if(!userInfo.value.friends.Contains(friend))
            {
                throw new Exception("User isn`t in your friend list.");
            }
            userInfo.value.friends.Remove(friend);
        }
        public void GetFriendsList(Key user)
        {
            Entry userInfo = FindEntry(user);
            if(userInfo.value.friends.Count == 0)
            {
                Console.WriteLine("Friend list is empty.");
                return;
            }
            Key[] array = new Key[userInfo.value.friends.Count];
            userInfo.value.friends.CopyTo(array, 0);
            foreach(Key k in array)
            {
                PrintUser(k);
            }
        }
        private int GetHash1(Key key)
        {
            return (int)(HashCode(key) % _table.Length);
        }
        private int GetHash2(Key key)
        {
            return (int)((HashCode(key) % (_table.Length-1)) + 1);
        }
        public void Clear()
        {
            _table = new Entry[_table.Length];
            _loadness = 0;
            _size = 0;
            _rehashing = false;
        }
    }
}