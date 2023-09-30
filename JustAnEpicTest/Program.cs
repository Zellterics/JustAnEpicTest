using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustAnEpicTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //temp func
            String[] lines;
            lines = System.IO.File.ReadAllLines("Map.csv");
            foreach (string line in lines) {
                Console.WriteLine(line);
            }
            Console.ReadLine();
        }
    }
}
