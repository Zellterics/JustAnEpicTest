using System;
using System.IO;

namespace JustAnEpicTest
{
    internal class Map
    {
        private readonly String path;
        private String[] lines;
        public Map(string path)
        {
            this.path = path;
            ReadMapFromFile();
        }

        private void ReadMapFromFile()
        {
            lines = File.ReadAllLines(path);
        }
        public void Print()
        {
            foreach (string line in lines)
            {
                PrintLine(line);
            }
        }

        private void PrintLine(string line)
        {
            char last = '0';
            foreach (char ch in line)
            {
                switch (ch)
                {
                    case ',':
                        continue;
                    case '0':
                        PrintCharacter('0', ConsoleColor.DarkGray, "▓▓", ref last);
                        break;
                    case '1':
                        PrintCharacter('1', ConsoleColor.White, "  ", ref last);
                        break;
                    case 'C':
                        PrintCharacter('C', ConsoleColor.Green, "C ", ref last);
                        break;
                    case 'P':
                        PrintCharacter('P', ConsoleColor.Magenta, "<>", ref last);
                        break;
                }
            }
            Console.WriteLine();
        }

        private void PrintCharacter(char character, ConsoleColor color, string output, ref char last)
        {
            if (last != character)
            {
                Console.ForegroundColor = color;
                last = character;
            }
            Console.Write(output);
        }
        /// <returns>Char in x, y position or F for empty space</returns>
        public String GetStringOn(int x, int y)
        {
            try
            {
                string[] column = lines[y].Split(',');
                return column[x].Trim();
            }
            catch (Exception)
            {
                return "F";
            }
        }

        public bool SetStringOn(int x, int y, string value)
        {
            string tempFile = "../../temp.csv";

            using (StreamWriter file = new StreamWriter(tempFile))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    if (i == y)
                    {
                        fields[x] = value;
                    }
                    file.WriteLine(string.Join(",", fields));
                }
            }

            File.Copy(tempFile, path, true);
            File.Delete(tempFile);
            ReadMapFromFile();

            return true;
        }
    }
}
