﻿using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Linq;

namespace TheSpaceport
{
    public class Program : MainMenu
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(@"
 __    __     _                            _____        _____ _            __                                       _
/ / /\ \ \___| | ___ ___  _ __ ___   ___  /__   \___   /__   \ |__   ___  / _\_ __   __ _  ___ ___ _ __   ___  _ __| |_
\ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \   / /\/ _ \    / /\/ '_ \ / _ \ \ \| '_ \ / _` |/ __/ _ \ '_ \ / _ \| '__| __|
 \  /\  /  __/ | (_| (_) | | | | | |  __/  / / | (_) |  / /  | | | |  __/ _\ \ |_) | (_| | (_|  __/ |_) | (_) | |  | |_
  \/  \/ \___|_|\___\___/|_| |_| |_|\___|  \/   \___/   \/   |_| |_|\___| \__/ .__/ \__,_|\___\___| .__/ \___/|_|   \__|
                                                                             |_|                  |_|

");

            Console.WriteLine("Are you part of the Star Wars universe or just ordinary filthy peasant?");

            Login.AccessControl();
        }

        public static void SelectMenu()
        {
            Console.WriteLine("Press x to go back or press enter to continue");
            string press = Console.ReadLine().ToLower();
            if (press == "x")
            {
                Menu(Login.personLogIn);
            }
        }

        public static void BackToMenu()
        {
            Console.WriteLine($"[x] Back to main menu");
            string inputKey = Console.ReadLine().ToLower();
            if (inputKey == "x")
            {
                Menu(Login.personLogIn);
            }
        }
    }
}