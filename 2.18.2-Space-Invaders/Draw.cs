using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace _2._18._2_Space_Invaders
{
    public class Draw
    {
        static public char[,] BaseField =
                {
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', },
                };

        static public char[,] ActiveField = new char[11,11];

        static public void DrawField(int Killcount)
        {
            Console.Clear();
            Console.WriteLine($"Kill Count: {Killcount}" );
            Console.WriteLine("");
            for (int x = 0; x < ActiveField.GetLength(0); x++)
            {
                for (int y = 0; y < ActiveField.GetLength(1); y++)
                {
                    Console.Write(ActiveField[x, y]);
                }

                Console.WriteLine();
            }
        }

        static public void ActivateField()
        {
            for (int x = 0; x < BaseField.GetLength(0); x++)
            {
                for (int y = 0; y < BaseField.GetLength(1); y++)
                {
                    ActiveField[x, y] = BaseField[x, y];
                }
            }
        }

        public static void DrawEndScreen()
        {
            for (int i = 0; i < 6; i++)
            {
                Console.BackgroundColor = (i % 2 == 0) ? ConsoleColor.Yellow : ConsoleColor.DarkRed;
                Console.Clear();
                Thread.Sleep(80);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Thread.Sleep(400);

            string[] victoryArt = new string[]
            {
"  __     ______  _    _  __          ______  _   _  _ ",
"  \\ \\   / / __ \\| |  | | \\ \\        / / __ \\| \\ | || |",
"   \\ \\_/ / |  | | |  | |  \\ \\  /\\  / / |  | |  \\| || |",
"    \\   /| |  | | |  | |   \\ \\/  \\/ /| |  | | . ` || |",
"     | | | |__| | |__| |    \\  /\\  / | |__| | |\\  ||_|",
"     |_|  \\____/ \\____/      \\/  \\/   \\____/|_| \\_|(_)"
            };

            ConsoleColor[] rainbow = new ConsoleColor[]
            {
        ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green,
        ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta
            };

            int screenCenterTop = 4;
            int colorIndex = 0;

            foreach (string line in victoryArt)
            {
                Console.SetCursorPosition(10, screenCenterTop++);

                foreach (char c in line)
                {
                    Console.ForegroundColor = rainbow[colorIndex % rainbow.Length];
                    colorIndex++;
                    Console.Write(c);
                    Thread.Sleep(5);
                }
                Console.WriteLine();
            }

            Thread.Sleep(300);

            
            for (int pulse = 0; pulse < 3; pulse++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(8, 3);
                Console.Write(new string('=', 58));
                Console.SetCursorPosition(8, screenCenterTop);
                Console.Write(new string('=', 58));
                Thread.Sleep(120);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(8, 3);
                Console.Write(new string('=', 58));
                Console.SetCursorPosition(8, screenCenterTop);
                Console.Write(new string('=', 58));
                Thread.Sleep(120);
            }

            Thread.Sleep(300);

           
            Console.SetCursorPosition(10, screenCenterTop + 2);
            Console.ForegroundColor = ConsoleColor.White;
            for (int k = 0; k <= Game.KillCount; k++)
            {
                Console.SetCursorPosition(10, screenCenterTop + 2);
                Console.Write($"Total Kills: {k}   ");
                Thread.Sleep(Math.Max(5, 300 / Math.Max(1, Game.KillCount)));
            }

            Thread.Sleep(400);

            Random rand = new Random();

            
            for (int wave = 0; wave < 3; wave++)
            {
                int sparkleCount = 15 + wave * 10;
                for (int i = 0; i < sparkleCount; i++)
                {
                    int pX = rand.Next(5, 70);
                    int pY = rand.Next(1, 12);

                    if (pY >= 4 && pY <= screenCenterTop && pX >= 10 && pX <= 65) continue;

                    Console.SetCursorPosition(pX, pY);
                    Console.ForegroundColor = rainbow[rand.Next(rainbow.Length)];
                    Console.Write(i % 3 == 0 ? "*" : "+");
                    Thread.Sleep(20);
                }
                Thread.Sleep(150);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();
        }

        public static void DrawLoseScreen()
        {
            for (int i = 0; i < 8; i++)
            {
                Console.BackgroundColor = (i % 2 == 0) ? ConsoleColor.DarkRed : ConsoleColor.Black;
                Console.Clear();
                Thread.Sleep(70);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Thread.Sleep(400);

            string[] gameOverArt = new string[]
            {
        "    _____                        ____                 ",
        "   / ____|                      / __ \\                ",
        "  | |  __  __ _ _ __ ___   ___ | |  | |_   _____ _ __  ",
        "  | | |_ |/ _` | '_ ` _ \\ / _ \\| |  | \\ \\ / / _ \\ '__| ",
        "  | |__| | (_| | | | | | |  __/| |__| |\\ V /  __/ |    ",
        "   \\_____|\\__,_|_| |_| |_|\\___| \\____/  \\_/ \\___|_|    "
            };

            Console.ForegroundColor = ConsoleColor.DarkRed;
            int screenTop = 4;

            foreach (string line in gameOverArt)
            {
                Console.SetCursorPosition(10, screenTop++);
                foreach (char c in line)
                {
                    Console.Write(c);
                    Thread.Sleep(5);
                }
                Console.WriteLine();
            }

            Thread.Sleep(300);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(10, screenTop + 1);
            Console.Write($"Total Kills: {Game.KillCount}");
            Thread.Sleep(600);

            Random rand = new Random();
            for (int i = 0; i < 40; i++)
            {
                int pX = rand.Next(5, 70);
                int pY = rand.Next(1, 12);

                if (pY >= 4 && pY <= 9 && pX >= 10 && pX <= 65) continue;

                Console.SetCursorPosition(pX, pY);
                Console.ForegroundColor = (i % 2 == 0) ? ConsoleColor.DarkGray : ConsoleColor.Red;
                Console.Write(i % 3 == 0 ? "x" : "-");
                Thread.Sleep(30);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();
        }
    }
}
