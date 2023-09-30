using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustAnEpicTest
{
    internal class Map
    {
        private String path;
        public Map(String path) {
            this.path = path;
        }
        public void Print()
        {
            String[] lines;
            lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                foreach (char ch in line)
                {
                    switch (ch)
                    {
                        case ',':
                            continue;
                        case '0':
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("▓▓");
                            break;
                        case '1':
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("░░");
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
