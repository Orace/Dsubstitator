// 
// This file is a part of the Dsubstitator project.
// https://github.com/Orace
// 

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Dsubstitator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please provide a file path for words database");
                Console.ReadLine();
            }

            var data = new Data();
            using (var file = File.OpenRead(args[0]))
            {
                using (var sr = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                {
                    var count = 0;
                    while (!sr.EndOfStream)
                    {
                        var l = sr.ReadLine();
                        if (l == null) continue;
                        if (l.StartsWith("#")) continue;
                        count += l.Split(' ').Count(data.Register);
                    }

                    Console.WriteLine($"{count} words registered.");
                }
            }

            for (;;)
            {
                Console.Write("Enter a word: ");
                var w = Console.ReadLine();
                var matches = data.GetMatch(w);
                Console.WriteLine($"{matches.Count} matchs for: '{w}'");
                foreach (var match in matches)
                {
                    Console.WriteLine(match);
                }

                Console.WriteLine("---");
                Console.WriteLine();
            }
        }
    }
}