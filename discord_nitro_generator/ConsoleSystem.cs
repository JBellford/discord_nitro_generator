using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_nitro_generator
{
    class ConsoleSystem
    {
        public static void SetTitle(string title)
        {
            Console.Title = title;
        }
        public static void Log(string one, string two, bool writeline = true)
        {
            if (writeline)
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.Write("["); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("+");
                Console.ForegroundColor = ConsoleColor.Red; Console.Write("]"); Console.ForegroundColor = ConsoleColor.White; Console.Write(" - "); Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(one); Console.ForegroundColor = ConsoleColor.Red; Console.Write(" -> "); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(two);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.Write("["); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("+");
                Console.ForegroundColor = ConsoleColor.Red; Console.Write("]"); Console.ForegroundColor = ConsoleColor.White; Console.Write(" - "); Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(one); Console.ForegroundColor = ConsoleColor.Red; Console.Write(" -> "); Console.ForegroundColor = ConsoleColor.White; Console.Write(two);
            }
        }
    }
}
