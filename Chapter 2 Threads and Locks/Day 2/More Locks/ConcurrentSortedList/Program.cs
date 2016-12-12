using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentSortedList
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Node
    {
        public int value { get; set; }
        public Node prev { get; set; }
        public Node next { get; set; }

        public Node(int value, Node prev, Node next)
        {
            this.value = value;
            this.prev = prev;
            this.next = next;
        }
    }
}
