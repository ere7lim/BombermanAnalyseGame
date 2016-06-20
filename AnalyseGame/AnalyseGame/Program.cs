using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AnalyseGame
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Count() > 0)
            {
                var dirPath = args[0];
                char playerKey = 'Z';
                if(args.Count() > 1)
                {
                    playerKey = args[1][0];
                }
                if (!Directory.Exists(dirPath))
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: Working directory \"" + dirPath + "\" does not exist.");
                    Environment.Exit(1);
                }

                List<string> files = new List<string> { };

                var dirs = Directory.EnumerateDirectories(dirPath)
                                    .OrderBy(dir => dir.Length)
                                    .ToList();               

                foreach (var dir in dirs)
                {
                    if (File.Exists(dir + "\\map.txt"))
                    {
                        files.Add(dir + "\\map.txt");
                    }
                }

                int index = 0;

                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Round: " + index + "\n\r");

                    StreamReader reader = new StreamReader(files[index]);
                    string map = "";
                    string line =  "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line[0] == '#')
                        {
                            map += line;
                            map += "\n\r";
                        }
                    }

                    RenderToConsolePretty(map, playerKey);

                    ConsoleKeyInfo select = Console.ReadKey();
                    while((select.Key != ConsoleKey.RightArrow) && (select.Key != ConsoleKey.LeftArrow))
                    {
                        if(select.Key == ConsoleKey.Escape)
                            Environment.Exit(0);
                        select = Console.ReadKey();
                    }

                    if (select.Key == ConsoleKey.RightArrow)
                    {
                        if (index < files.Count() - 1)
                            index++;
                    }
                    if (select.Key == ConsoleKey.LeftArrow)
                    {
                        if (index > 0)
                            index--;
                    }
                }
            }
            Console.WriteLine("Error: No directory specified");
            Environment.Exit(1);
        }

        public static void RenderToConsolePretty(string gameMap, char playerKey)
        {
            bool insideMap = false;

            foreach (var character in gameMap)
            {
                if (character == '#')
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    insideMap = true;
                }
                if (character == '\t')
                {
                    insideMap = false;
                }
                if (character == '+')
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                if (character == '!')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                if (character == '&')
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                if (character == '$')
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (character == '*')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (insideMap && (character == playerKey || Char.ToUpperInvariant(character) == playerKey))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }

                Console.Write(character);

                Console.ResetColor();
            }
        }
    }
}
