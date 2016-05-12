using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp1
{
    class Program
    {
        private struct Node
        {
            public string Current;
            public string Next;
        }
        static void Main(string[] args)
        {
            List<Node> list = getNodes();
            List<Node> sortedList = new List<Node>();
            list.ForEach(node => Console.WriteLine("{0} > {1}", node.Current, node.Next)); Console.WriteLine();
            
            Stopwatch s = Stopwatch.StartNew();
            Node head = getHeadNode(list);
            list.Remove(head);
            sortedList.Add(head);
            while (list.Count > 0)
            {
                sortedList.Add(list.Find(node => node.Current == sortedList.Last().Next));
                list.Remove(sortedList.Last());
            }
            s.Stop();

            sortedList.ForEach(node => Console.WriteLine("{0} > {1}", node.Current, node.Next));
            Console.WriteLine("Elapsed Time: {0} ms", s.ElapsedMilliseconds);
            Console.ReadLine();
        }
        private static Node getHeadNode(List<Node> list)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            for (int i = 0; i < list.Count(); i++)
            {
                if (counts.ContainsKey(list[i].Current))
                    counts[list[i].Current] = 2;
                else
                    counts.Add(list[i].Current, 1);

                if (counts.ContainsKey(list[i].Next))
                    counts[list[i].Next] = 2;
                else
                    counts.Add(list[i].Next, 1);
            }
            return list.Find(node => counts.Where(x => x.Value == 1).Select(x => x.Key).Any(x => x == node.Current));
        }
        private static List<Node> getNodes()
        {
            Random rand = new Random();
            List<Node> list = new List<Node>();
            List<string> cities = new List<string>() { "Мельбурн", "Кельн", "Москва", "Париж", "Вашингтон", "Франкфурт", "Токио", "Дрезден", "Бостон", "Хьюстон", "Мадрид" };

            int counter = rand.Next(0, cities.Count - 1);
            string Prev = cities[counter];
            cities.RemoveAt(counter);

            while (cities.Count > 0)
            {
                counter = rand.Next(0, cities.Count - 1);
                list.Add(new Node { Current = Prev, Next = cities[counter] });
                Prev = cities[counter];
                cities.RemoveAt(counter);
            }
            list.Shuffle();

            return list;
        }
    }
    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rand = new Random();
            int count = list.Count;
            while (count > 1)
            {
                count--;
                int position = rand.Next(count + 1);
                T value = list[position];
                list[position] = list[count];
                list[count] = value;
            }
        }
    }  
}
