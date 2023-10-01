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
        public bool SetStringOn(int x, int y, String value)
        {
            bool edited = false;
            string tempFile = "../../temp.csv";
            for(int i = 0; i < lines.Length; i++)
            {
                String[] fields = lines[i].Split(',');
                if(i != y)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(tempFile, true))
                    {
                        for (int j = 0; j < fields.Length - 1; j++) {
                            file.Write(fields[j] + ",");
                        }
                        file.Write(fields[fields.Length - 1] + '\n');
                    }
                }
                else
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(tempFile, true))
                    {
                        for (int j = 0; j < fields.Length - 1; j++)
                        {
                            if(j == x)
                            {
                                file.Write(value + ",");
                                edited = true;
                                continue;
                            }
                            file.Write(fields[j] + ",");
                        }

                        if (fields.Length - 1 == x)
                        {
                            file.Write(value + '\n');
                            edited = true;
                            continue;
                        }
                        file.Write(fields[fields.Length - 1] + '\n');
                    }
                }
            }
            File.Copy(tempFile, path, true);
            File.Delete(tempFile);
            lines = System.IO.File.ReadAllLines(path);
            return edited;
        }
    }
}
