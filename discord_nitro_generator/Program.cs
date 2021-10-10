using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Discord;
using Discord.Gateway;
using Discord.Commands;
using Discord.Media;
using System.IO;

namespace discord_nitro_generator
{
    class Program
    {
        public static DiscordSocketClient client;
        public static Random rand = new Random(new Random(DateTime.Now.Second).Next(0, DateTime.Now.Hour));

        public static string token = $"";
        public static bool is_loggedIn = false;

        public static int max_code_count = 1;
        public static int codes_generated = 0;
        public static int codes_checked = 0;
        public static int seconds_between_checks = 5;

        static void Main(string[] args)
        {
            if (token == "") { ConsoleSystem.Log("Token", " ", false); token = Console.ReadLine(); } else { ConsoleSystem.Log("Token", $"{token}", true); }

            client = new DiscordSocketClient(new DiscordSocketConfig() { ApiVersion = 7u });

            client.OnLoggedIn += OnLoggedIn;

            client.Login(token);

            Console.Clear();

            if (File.Exists("nitro_codes_unchecked.txt"))
                File.Delete("nitro_codes_unchecked.txt");

            ConsoleSystem.Log("Number of codes to generate", " ", false);
            max_code_count = Int32.Parse(Console.ReadLine().Trim());

            Console.Clear();

            StreamWriter sw = new StreamWriter("nitro_codes_unchecked.txt", true);
            for (int x = 1; x <= max_code_count; x++)
            {
                sw.WriteLine(NitroCode(24));
            }
            sw.Dispose();

            codes_generated = File.ReadAllLines("nitro_codes_unchecked.txt").ToList().Count();
            ConsoleSystem.Log($"Finished generating codes", $"Generated {codes_generated} codes");

            Thread.Sleep(2000);

            Console.Clear();

            ConsoleSystem.Log("Number of seconds between each nitro checked", " ", false);
            seconds_between_checks = Int32.Parse(Console.ReadLine().Trim());

            Console.Clear();

            foreach (var code in File.ReadAllLines("nitro_codes_unchecked.txt").ToList())
            {
                ConsoleSystem.Log($"Nitro Checker", $"Checking discord.gift/{code}");
                try
                {
                    client.RedeemGift(code);
                    ConsoleSystem.Log($"Nitro Checker", $"discord.gift/{code} is valid and was redeemed");
                }
                catch (DiscordHttpException ex)
                {
                    ConsoleSystem.Log($"Nitro Checker", $"discord.gift/{code} is invalid");
                }
                Console.WriteLine();
                codes_checked++;
                Thread.Sleep(seconds_between_checks * 1000);
            }

            Thread.Sleep(-1);
        }

        private static void OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            if (!is_loggedIn)
            {
                new Thread(TitleLoop).Start();
                is_loggedIn = true;
            }
        }

        public static void TitleLoop()
        {
            while (true)
            {
                ConsoleSystem.SetTitle($"{DateTime.Now}      |     JBellford nitro generator     |      Logged in as : {client.User.Username}      |      generated codes : {codes_generated}      |      codes checked : {codes_checked}");
                Thread.Sleep(10);
            }
        }
        static string NitroCode(int length)
        {
            string c = "";
            string alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            for (int x = 0; x < length; x++)
            {
                c += alph[rand.Next(0, alph.Length - 1)];
            }
            return c;
        }
    }
}
