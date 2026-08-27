using System;
using System.Collections.Generic;
using System.Text;

namespace _2._18._2_Space_Invaders
{
    public static class Player
    {
        static public int[] Location = new int[2];
        static public bool PlayerIsAlive { get; set; }
        static public void Move()
        {
            try
            {


                Draw.ActiveField[Location[0], Location[1]] = '^';

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);


                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (Draw.ActiveField[Location[0] - 1, Location[1]] == ' ')
                            {
                                Draw.ActiveField[Location[0], Location[1]] = ' ';
                                Location[0] = Location[0] - 1;
                                Draw.ActiveField[Location[0], Location[1]] = '^';

                                break;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (Draw.ActiveField[Location[0] + 1, Location[1]] == ' ' && Location[0] != Draw.ActiveField.GetLength(0) - 2)
                            {
                                Draw.ActiveField[Location[0], Location[1]] = ' ';
                                Location[0] = Location[0] + 1;
                                Draw.ActiveField[Location[0], Location[1]] = '^';

                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (Draw.ActiveField[Location[0], Location[1] - 1] == ' ')
                            {
                                Draw.ActiveField[Location[0], Location[1]] = ' ';
                                Location[1] = Location[1] - 1;
                                Draw.ActiveField[Location[0], Location[1]] = '^';

                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (Draw.ActiveField[Location[0], Location[1] + 1] == ' ')
                            {
                                Draw.ActiveField[Location[0], Location[1]] = ' ';
                                Location[1] = Location[1] + 1;
                                Draw.ActiveField[Location[0], Location[1]] = '^';

                            }
                            break;
                        case ConsoleKey.Spacebar:
                            {
                                Task.Run(() => SendShot());
                                break;
                            }
                    }
                }
            }
            catch
            {

            }
        }
        static public void SetStartLocation()
        {
            Location[0] = Draw.ActiveField.GetLength(0) - 2;
            Location[1] = Draw.ActiveField.GetLength(0) / 2;
        }
        static public void SendShot()
        {

            int startRow = Location[0];
            int startCol = Location[1];
            int shotpos = 1;
            bool done = false;
            while (!done)
            {
                try
                {
                    if (Draw.ActiveField[startRow - shotpos, startCol] == '*' && startRow - shotpos != Draw.ActiveField.GetLength(0))
                    {
                        Draw.ActiveField[startRow - shotpos, startCol] = ' ';
                        Draw.ActiveField[startRow - shotpos + 1, startCol] = ' ';
                        Game.KillCount++;
                        done = true;
                        break;
                    }
                    else if (startRow - shotpos == 0)
                    {
                        Draw.ActiveField[startRow - shotpos, startCol] = ' ';
                        Draw.ActiveField[startRow - shotpos + 1, startCol] = ' ';

                        done = true;
                        break;
                    }
                    else if (startRow - shotpos != Draw.ActiveField.GetLength(0))
                    {
                        Draw.ActiveField[startRow - shotpos, startCol] = '.';
                        Draw.ActiveField[startRow - shotpos + 1, startCol] = ' ';
                        Thread.Sleep(50);
                        shotpos++;
                    }
                }
                catch
                {
                    break;
                }


            }


        }
    }
}

