using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCache
{
    class Cache
    {
        private int capacity;
        private int fullness = 0;
        public DoubleLL mylist = new DoubleLL();
        public Dictionary<string, int> myDictionary = new Dictionary<string, int>();

        // cache constructor
        public Cache(int n)
        {
            this.capacity = n;
            
        }

        static void Main(string[] args)
        {
            //create a new linked list
            //DoubleLL mylist = new DoubleLL();
            //mylist.add("alpha");
            //mylist.add("beta");
            //mylist.printList();
            //Console.WriteLine(mylist.size);
            //Console.WriteLine("THe tail is " + mylist.tail.data);
            //mylist.removeFromTail();
            //Console.WriteLine("THe tail is " + mylist.tail.data);
            //Console.WriteLine(mylist.size);
             
            Cache mycache = new Cache(5);   // I'm creating a new cache is size 100

            // add to the cachce, not the dictionary itself.
            mycache.Add("cat", 22);
            Console.WriteLine("The size of my cache is" + mycache.fullness);
            Console.WriteLine("The size of my DLL is " + mycache.mylist.size);


            mycache.Add("doggy", 22);
            Console.WriteLine("The size of my cache is" + mycache.fullness);
            Console.WriteLine("The size of my DLL is " + mycache.mylist.size);

            mycache.Add("doggy", 22);
            Console.WriteLine("Added doggy again");
            Console.WriteLine("The size of my cache is" + mycache.fullness);
            Console.WriteLine("The size of my DLL is " + mycache.mylist.size);

            mycache.Add("rabbit", 22);
            mycache.Add("chicken", 22);
            mycache.Add("hot dog", 22);
            mycache.Add("sharks", 22);
            mycache.mylist.printList();
            mycache.Clear();
            Console.WriteLine("The size of the dll is now: " + mycache.mylist.size);
        }

        void Add(string key, int pages)   // this cache takes in a string key, and an array of pages
        {

            //  1.  Check if string (key) already exists in the dictionary
            //  2. If exists in dictionary, move the LL node to the head (doesn't matter if the cache is full or not)
            //  3.  if not in the dictionary, and the cache is not full, add to the linked list and the dictionary
            //  4. if not in the dictionary, and the cache is full, drop oldest item from linked list and dictionary. Add new item to LL and dictionary

            //bool ishere = this.myDictionary.ContainsKey("cat");
            //Console.WriteLine("Cat is in the cache's dictionary " + ishere);

            //   3   Key is not in the dictionary, and the Cache is not full
            if(this.myDictionary.ContainsKey(key) == false && this.fullness < this.capacity)
            {
                this.myDictionary.Add(key, pages);
                this.fullness++;
                this.mylist.add(key);

            }

            // 4  Key is not in the dictionary, and the cache is full
            //  add to the dictionary and LL. Drop the oldest item from Dictionary and Linked List
            if(this.myDictionary.ContainsKey(key) == false && this.fullness >= this.capacity)
            {
                // save the string from removeOldest, and use that to remove from dictionary
                string toRemove = this.mylist.removeFromTail(); // Remove oldest from list
                this.myDictionary.Remove(toRemove);  //  remove key from oldest 
                this.myDictionary.Add(key, pages);
                this.mylist.add(key);

            }

            // 2  Key Exists in the dictionary (doesn't matter if the cache is full)
            // move the key from the linked list to the top
            //if(this.myDictionary.ContainsKey(key) == true)
            //{
            //    this.mylist.removeFromDLL(key);
            //    this.mylist.add(key);
            //}

        }

        void Clear()   // this will clear the cache and the dictionary
        {
            this.mylist.deleteList();
        }

        //bool TryGetValue(string key, out int val)
        //{
        //    val = 0;
        //}
    }





    public class Node
    {
        public string data;
        public Node next = null;
        public Node prev = null;

        public Node(string s)
        {
            //LinkedListNode<int> ll;
            //ll.Previous
            this.data = s;
             
        }
    }

    public class DoubleLL
    {
        public Node head;
        public Node tail;
        public int size = 0;

        public DoubleLL()
        {

        }
        public void add(string s)
        {
            Node newest = new Node(s);   // created a new node for this list

            if (head == null && tail == null)  // meaning empty list
            {
                head = newest;
                tail = newest;
                size++;
            } else  // non empty list  (the logic should go here to make sure that the size is not over 100)
            {
                this.head.prev = newest;
                newest.next = head;
                head = newest;
                size++;
            }

        }

        public void removeFromDLL(string s)
        {
            // edge cases,  assuming that the node definitely is in the list
            //  node is at the head, node is at the tail, node is in the middle somewhere
            Node iter = head; 
            while(iter != null)
            {
                if(iter.data == s)
                {
                    iter.prev.next = iter.next;
                    iter.next.prev = iter.prev;
                    break;
                }
                iter = iter.next;
            }
            size--;
        }

        public string removeFromTail()
        {
            string last = tail.data;
            tail = tail.prev;
            tail.next = null;
            size--;
            return last;
        }

        public void printList()
        {
            if(head == null)
            {
                Console.WriteLine("The list is empty");
                return;
            }

            Console.WriteLine("Below is your linked list");
            Node iter = head;
            while(iter != null)
            {
                Console.WriteLine(iter.data);
                iter = iter.next;
            }
        }

        public void deleteList()
        {
            if(head == null)
            {
                Console.WriteLine("The list is empty, nothing to delete");
                return;
            }
            Node del  = head;  // del will be the node to delete
            Node temp = head.next; // temp is the node that is one ahead of del
            while(del != null)
            {
                del.prev = null;
                del.next = null;
                size--; // each time a node is deleted
                del = temp;
                if (temp != null)   // only if temp is not null, then we'll move temp
                {
                    temp = temp.next;
                }
            }
        }
    }
}
