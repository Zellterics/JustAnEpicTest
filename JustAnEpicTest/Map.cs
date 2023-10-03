using Newtonsoft.Json.Linq;
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
                        PrintCharacter('0', ConsoleColor.DarkGray, ConsoleColor.Black, "▓▓", ref last);
                        break;
                    case '1':
                        PrintCharacter('1', ConsoleColor.White, ConsoleColor.Black, "  ", ref last);
                        break;
                    case 'C':
                        PrintCharacter('C', ConsoleColor.Green, ConsoleColor.DarkGray, "C ", ref last);
                        break;
                    case 'P':
                        PrintCharacter('P', ConsoleColor.Magenta, ConsoleColor.Black, "<>", ref last);
                        break;
                    case 'p':
                        PrintCharacter('p', ConsoleColor.Magenta, ConsoleColor.DarkGray, "<>", ref last);
                        break;
                    case '2':
                        PrintCharacter('2', ConsoleColor.Green, ConsoleColor.DarkGray, "  ", ref last);
                        break;
                    case '3':
                        PrintCharacter('3', ConsoleColor.Black, ConsoleColor.DarkGray, "▓▓", ref last);
                        break;
                }
            }
            Console.WriteLine();
        }

        private void PrintCharacter(char character, ConsoleColor color, ConsoleColor backColor , string output, ref char last)
        {
            if(character == '1')
            {
                if(Console.BackgroundColor == ConsoleColor.DarkGray)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write(output);
                return;
            }
            if (last != character)
            {
                Console.BackgroundColor = backColor;
                Console.ForegroundColor = color;
                last = character;
            }
            Console.Write(output);
        }
        /// <returns>Char in x, y position or F for empty space</returns>
        public String GetStringOn(Pair position)
        {
            try
            {
                string[] column = lines[position.y].Split(',');
                return column[position.x].Trim();
            }
            catch (Exception)
            {
                return "F";
            }
        }

        public bool SetStringOn(Pair position, string value)
        {
            string tempFile = "../../temp.csv";

            using (StreamWriter file = new StreamWriter(tempFile))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    if (i == position.y)
                    {
                        fields[position.x] = value;
                    }
                    file.WriteLine(string.Join(",", fields));
                }
            }

            File.Copy(tempFile, path, true);
            File.Delete(tempFile);
            ReadMapFromFile();

            return true;
        }
        /// <summary>
        /// clean 1 type of character in all the map and replace it
        /// </summary>
        /// <param name="old">character to be replace</param>
        /// <param name="replace">replacement of the character</param>

        public void CleanCharacters(String old, String replace)
        {
            
            string tempFile = "../../temp.csv";

            using (StreamWriter file = new StreamWriter(tempFile))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    for(int j = 0; j < fields.Length; j++)
                    {
                        if (fields[j] == old)
                        {
                            fields[j] = replace;
                        }
                    }
                    file.WriteLine(string.Join(",", fields));
                }
            }

            File.Copy(tempFile, path, true);
            File.Delete(tempFile);
            ReadMapFromFile();
        }
    }
}
