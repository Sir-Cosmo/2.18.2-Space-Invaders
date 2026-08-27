using System;
using System.Collections.Generic;
using System.Text;

namespace _2._18._2_Space_Invaders
{
    public class Enemy
    {
        public int[] Location = new int[2];
        public bool IsDead { get; set; }

        public bool ShotHit { get; set; }
        public Enemy(int[] location)
        {
            Location = location;
        }
        public void Shoot()
        {
            int shotpos = - 1;
            bool done = false;
            bool Second = false;

            while (!done)
            {
                try
                {

                    if (Location[0] - shotpos == Draw.ActiveField.GetLength(0) - 1)
                    {
                        Draw.ActiveField[Location[0] - shotpos, Location[1]] = ' ';
                        Draw.ActiveField[Location[0] - shotpos - 1, Location[1]] = ' ';

                        done = true;
                        break;
                    }
                    if (Draw.ActiveField[Location[0] - shotpos, Location[1]] == '^')
                    {
                        ShotHit = true;
                        break;
                    }
                    else if (Location[0] - shotpos != Draw.ActiveField.GetLength(0))
                    {
                        Draw.ActiveField[Location[0] - shotpos, Location[1]] = '|';
                        if (Second)
                        {
                            Draw.ActiveField[Location[0] - shotpos - 1, Location[1]] = ' ';
                        }
                        Thread.Sleep(80);
                        shotpos--;
                        Second = true;
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