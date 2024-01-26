

using System.Collections.Generic;
using System.Net.Mime;

namespace app
{
       class Program
    {
        public static void Main(string[] args) {


            // Array 

            int[] num = new int[5];

            for(int i=0;i<num.Length; i++)
            {
                Console.WriteLine("enter some data");
                num[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("ELEMENTS IN YOUR ARRAY");
            for (int i = 0; i < num.Length; i++) Console.Write(num[i] + " ");
            

            // List Collection (dynamic array)

            List<int> li = new List<int>();
            
            for (int i = 1; i < 6; i++) {
                Console.WriteLine("enter some data");
                li.Add(Convert.ToInt32(Console.ReadLine()));
            }

            li.Insert(0, 5);

            li.Sort();
           =bool 
            Console.WriteLine("ELMEENTS IN YOUR LIST");
            for (int i = 0; i < li.Count; i++) Console.Write(li[i] + " ");
              


            // Dictonary Collection

            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict["ajay"] = "xyz school";
            dict["arun"] = "abc school";
            dict["rajesh"] = "abc school";

            Console.WriteLine("ELEMENTS IN YOUR DICTIONARY");
            foreach(KeyValuePair<string,string> kvp in dict)
            {
                Console.Write(kvp.Value + " ");
                   
            }


            // KeyValuePair collection

            KeyValuePair<int, int> p = new KeyValuePair<int, int>(1, 2);

           
            p = new KeyValuePair<int, int>(3, 4);

            Console.WriteLine($"{p.Key}:{p.Value}");

            // list of keyValuePair collections 
            List<KeyValuePair<string, int>> keyValuePairsList = new List<KeyValuePair<string, int>>();

            keyValuePairsList.Add(new KeyValuePair<string, int>("One", 1));
            keyValuePairsList.Add(new KeyValuePair<string, int>("Two", 2));

            foreach (var pair in keyValuePairsList)
            {
                Console.WriteLine($"Key: {pair.Key}, Value: {pair.Value}");
            }

            // Queue 

            Queue<string> namesQueue = new Queue<string>();

            namesQueue.Enqueue("xyz");
            namesQueue.Enqueue("abc");
            namesQueue.Enqueue("abc2");

            while (namesQueue.Count > 0)
            {
                string name = namesQueue.Dequeue();
                Console.WriteLine(name);
            }

        // Stack 
           
           Stack<int> ints = new Stack<int>();

            ints.Push(1);
            ints.Push(2);
            ints.Push(3);   

            while(ints.Count > 0)
            {
                Console.WriteLine(ints.Pop());
            }

        }
    }
}
