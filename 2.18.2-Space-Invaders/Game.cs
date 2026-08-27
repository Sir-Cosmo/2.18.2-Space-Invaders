namespace _2._18._2_Space_Invaders
{
    public class Game
    {
        public static int KillCount = 0;

        public static async Task StartGame()
        {
            Player.SetStartLocation();
            Console.CursorVisible = false;

            bool playAgain = true;
            while (playAgain)
            {
                playAgain = false;
                Enemy[] enemies = GenerateEnemys();
                var cts = new CancellationTokenSource();
                bool restart = false;

                Task renderer = RunInterval(() => Draw.DrawField(KillCount), 0.1, cts.Token);
                Task enemyHandler = RunInterval(() => RandomEnemyShoots(enemies), 0.5, cts.Token);
                Task winCheck = RunInterval(() => restart = CheckWinOrLose(cts, enemies), 0.1, cts.Token);
                Task playerHandler = Task.Run(() =>
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        Player.Move();
                    }
                });

                try
                {
                    await Task.WhenAll(renderer, enemyHandler, winCheck, playerHandler);
                }
                catch (OperationCanceledException)
                {

                }

                playAgain = restart;
            }
        }
        private static async Task RunInterval(Action method, double time, CancellationToken cancellationToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(time));

            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                method.Invoke();
            }
        }



        static public Enemy[] GenerateEnemys()
        {
            Draw.ActivateField();
            Random rnd = new Random();
            int rowsofEnemys = rnd.Next(1, 4);
            Enemy[] enemies = new Enemy[rowsofEnemys * Draw.ActiveField.GetLength(0)];

            Console.Clear();
            for (int x = rowsofEnemys; x < Draw.ActiveField.GetLength(0); x++)
            {
                for (int y = 0; y < Draw.ActiveField.GetLength(1); y++)
                {
                    Draw.ActiveField[x, y] = ' ';
                }

                Console.WriteLine();

            }

            int enemyCount = 0;
            for (int x = 0; x < rowsofEnemys; x++)
            {
                for (int y = 0; y < Draw.ActiveField.GetLength(1); y++)
                {
                    int[] enemyLocation = { x, y };
                    enemies[enemyCount] = new Enemy(enemyLocation);
                    enemyCount++;
                }
            }

            return enemies;

        }

        public static void RandomEnemyShoots(Enemy[] enemies)
        {
            Random rnd = new Random();

            int randomEnemy = rnd.Next(0, enemies.Length);

            if (Draw.ActiveField[enemies[randomEnemy].Location[0], enemies[randomEnemy].Location[1]] == '*' && Draw.ActiveField[enemies[randomEnemy].Location[0] + 1, enemies[randomEnemy].Location[1]] != '*')
            {
                enemies[randomEnemy].Shoot();
            }
            else
            {
                enemies[randomEnemy].IsDead = true;
            }
        }

        public static bool AreAllEnemiesDead(Enemy[] enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                if (Draw.ActiveField[enemy.Location[0], enemy.Location[1]] != '*')
                {
                    enemy.IsDead = true;
                }
            }

            return enemies.All(enemy => enemy.IsDead);
        }

        public static bool CheckIfPlayerGotHit(Enemy[] enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.ShotHit)
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }
            return false;
        }


        private static bool CheckWinOrLose(CancellationTokenSource cts, Enemy[] enemies)
        {
            if (AreAllEnemiesDead(enemies))
            {
                Thread.Sleep(1000);
                cts.Cancel();
                Console.WriteLine("Wollen sie eine weitere Runde spielen? [ y | n ]");
                char answer = Convert.ToChar(Console.ReadLine());

                if (answer == 'y')
                {
                    return true;
                }
                else
                {
                    Draw.DrawEndScreen();
                    return false;
                }
            }

            if (CheckIfPlayerGotHit(enemies))
            {
                cts.Cancel();
                Draw.DrawLoseScreen();
                return false;
            }

            return false;
        }
    }
}
