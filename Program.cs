using System;

class Program
{
    static void Main()
    {
        int playerHp = 100;
        int playerMaxHp = 100;
        int favor = 10;
        int favorMax = 10;
        int healAmount = 25;
        int restoreAmount = 3;
        int restoreCount = 2;

        string[] enenmies = { "Бешеный волк", "Морозный великан", "Огненный змей" };
        int[] enemiesHp = { 60, 90, 120 };
        int[] enemiesMaxHp = { 60, 90, 120 };
        int[] enemiesDmgMin = { 8, 10, 12 };
        int[] enemiesDmgMax = { 14, 16, 20 };

        Random rnd = new Random();

        Console.WriteLine("===== СКАНДИНАВСКИЙ ЭПОС: ВОИН-ЯРЛ =====");
        Console.WriteLine("Чтобы вступить в бой, воин, нажми на Enter...");
        Console.ReadLine();

        bool gameOver = false;

        for (int wave = 0; wave < enenmies.Length && !gameOver; wave++)

        {
            string enemyName = enenmies[wave];
            int enemyHp = enemiesHp[wave];
            int enemyMaxHp = enemiesMaxHp[wave];
            int eMin = enemiesDmgMin[wave];
            int eMax = enemiesDmgMax[wave];

            Console.WriteLine($"===== ВОЛНА {wave + 1}: {enemyName} =====");

            while (playerHp > 0 && enemyHp > 0)
            {
                ShowBars(playerHp, playerMaxHp, favor, favorMax, enemyName, enemyHp, enemyMaxHp);
                int action = GetAction(favor, healAmount, restoreAmount, restoreCount);

                switch (action)
                {
                    case 1:
                        int dmg1 = rnd.Next(13, 20);
                        enemyHp -= dmg1;
                        Console.WriteLine($"Ярл наносит удар топором: {dmg1} урона.");
                        break;
                    case 2:
                        favor -= 1;
                        int dmg2 = rnd.Next(25, 34);
                        enemyHp -= dmg2;
                        Console.WriteLine($"Ярость Ярла: {dmg2} урона! Милость Одина: {favor}/{favorMax}");
                        break;
                    case 3:
                        favor -= 1;
                        playerHp += healAmount;
                        if (playerHp > playerMaxHp)
                        {
                            playerHp = playerMaxHp;
                        }
                        Console.WriteLine($"Один дарует {healAmount} здовровья. Милость: {favor}/{favorMax}. HP: {playerHp}/{playerMaxHp}");
                        break;
                    case 4:
                        restoreCount--;
                        favor += restoreAmount;
                        if (favor > favorMax)
                        {
                            favor = favorMax;
                        }
                        Console.WriteLine($"Молитва Одина: +{restoreAmount} милости. Осталось молитв: {restoreCount}. Милость: {favor}/{favorMax}");
                        break;
                }

                if (enemyHp <= 0)

                {
                    enemyHp = 0;
                    Console.WriteLine($"{enemyName} повержен!\n");
                    break;
                }

                int enemyDmg = rnd.Next(eMin, eMax + 1);
                playerHp -= enemyDmg;
                if (playerHp < 0)
                {
                    playerHp = 0;
                }
                Console.WriteLine($"{enemyName} наносит {enemyDmg} урона. HP Ярла: {playerHp}/{playerMaxHp}");
                Console.WriteLine();
            }

            if (playerHp <= 0)
            {
                gameOver = true;
            }
        }

        if (playerHp <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ярл пал в бою. Вальгалла ждёт достойных.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Все враги повержены! Ярл одержал победу!");
            Console.ResetColor();
        }
    }

    static void ShowBars(int playerHp, int playerMaxHp, int favor, int favorMax, string enemyName, int enemyHp, int EnemyMaxHp)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Ярл HP: [");
        for (int i = 0; i < playerHp; i++)
        {
            Console.Write("#");
        }
        for (int i = 0; i < (playerMaxHp - playerHp); i++)
        {
            Console.Write("-");
        }
        Console.WriteLine($"] ({playerHp}/{playerMaxHp})");
        Console.ResetColor();



        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Мана Ярла: [");
        for (int m = 0; m < favor; m++)
        {
            Console.Write("+");
        }
        for (int m = 0; m < (favorMax - favor); m++)
        {
            Console.Write("-");
        }
        Console.WriteLine($"] ({favor}/{favorMax})");
        Console.ResetColor();


        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write($"HP {enemyName}:[");
        for (int o = 0; o < enemyHp; o++)
        {
            Console.Write("#");
        }
        for (int o = 0; o < (EnemyMaxHp - enemyHp); o++)
        {
            Console.Write("-");
        }
        Console.WriteLine($"] ({enemyHp}/{EnemyMaxHp})");
        Console.ResetColor();
    }

    static int GetAction(int favor, int healAmount, int restoreAmount, int restoreCount)
    {
        int action;
        bool isValid;


        do
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 — Атака");
            Console.WriteLine("2 — Яростная атака (1 мана)");
            Console.WriteLine($"3 — Лечение (+{healAmount})");
            Console.WriteLine($"4 — Милость Одина (+{restoreAmount} востановление маны, осталось: {restoreCount})");
            Console.Write("Ваш выбор: \n");

            isValid = int.TryParse(Console.ReadLine(), out action) && action >= 1 && action <= 4;

            if (!isValid)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: введите цифру от 1 до 4!\n");
                Console.ResetColor();
            }
            
            else if (action == 2 && favor == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: Нет маны!\n");
                Console.ResetColor();
                isValid = false;
            }
            else if (action == 3 && favor == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: Нет маны!\n");
                Console.ResetColor();
                isValid = false;
            }
        } while (!isValid);

        return action;
    }
}
