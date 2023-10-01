using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustAnEpicTest
{
    internal class Map
    {
        private String path;
        private String[] lines;
        public Map(String path) {
            this.path = path;
            lines = System.IO.File.ReadAllLines(path);
        }
        public void Print()
        {
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
                        case 'C':
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("C ");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
        /// <returns>Char in x, y position or F for empty space</returns>
        public String GetStringOn(int x, int y)
        {
            try
            {
                String[] column = lines[y].Split(',');
                String[] ch = column[x].Split(',');
                return ch[0];
            }
            catch (Exception)
            {
                return "F";
            }
        }
        public bool SetStringOn(int x, int y, string value)
        {
            bool edited = false;
            string tempFile = "../../temp.csv";

            using (StreamWriter file = new StreamWriter(tempFile, true))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    if (i == y)
                    {
                        fields[x] = value;
                        edited = true;
                    }
                    file.WriteLine(string.Join(",", fields));
                }
            }

            File.Copy(tempFile, path, true);
            File.Delete(tempFile);
            lines = File.ReadAllLines(path);

            return edited;
        }

    }
}
