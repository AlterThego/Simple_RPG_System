using System;

Random Random = new();

int turn = 0;

Console.WriteLine("Character Creation System");
Console.Write("Enter your character's name: ");
string? playerName = Console.ReadLine();
Player player = new(playerName);
Console.WriteLine("\nWelcome to RNG RPG Game " + playerName + "!");
Console.Write("Press enter to continue...");
Console.ReadLine();


while (player.IsAlive())
{
    Enemy enemy = GenerateRandomEnemy();
    Console.WriteLine(" ");
    Console.WriteLine("*NOTE: Higher Speed gets to attack first!");
    Console.WriteLine(" ");
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|", " ", "Player:", "Enemy: ");
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|", "Name: ", player.Name, enemy.Name);
    Console.WriteLine("-------------------------------------------------");

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|", "Strength:", player.Strength, enemy.Strength);
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|", "Health:", player.Health, enemy.Health);
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|", "Defense:", player.Defense, enemy.Defense);
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|", "Speed", player.Speed, enemy.Speed);
    Console.ResetColor();

    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine($"You encounter an enemy: {enemy.Name}");
    Console.Write("Press enter to start...");
    Console.ReadLine();

    if (enemy.Speed == player.Speed)
    {
        enemy.Speed++;
    }

    bool playerGoesFirst = player.Speed > enemy.Speed;

    turn++;

    while (enemy.IsAlive() && player.IsAlive())
    {
        Console.WriteLine();
        Console.WriteLine("---------");
        Console.WriteLine($"|Round {turn}|");
        Console.WriteLine("---------");

        if (playerGoesFirst)
        {
            PlayerTurn(player, enemy);
            if (!enemy.IsAlive())
                break;

            EnemyTurn(player, enemy);
            if (!player.IsAlive())
                break;
        }
        else
        {
            EnemyTurn(player, enemy);
            if (!player.IsAlive())
                break;

            PlayerTurn(player, enemy);
            if (!enemy.IsAlive())
                break;
        }
    }

    if (player.IsAlive())
    {
        Console.WriteLine($"You defeated {enemy.Name}!");
        if (Random.Next(1, 3) == 1)
        {
            Item newItem = GenerateRandomItem();
            player.Inventory.AddItem(newItem);
            Console.WriteLine($"You found an item: {newItem.Name} (+{newItem.StrengthBuff} Strength, +{newItem.DefenseBuff} Defense)");
        }
    }
    else
    {
        Console.WriteLine($"You were defeated by {enemy.Name}. Game Over!");
        break;
    }
}

Console.WriteLine("\nGame Over!");

Enemy GenerateRandomEnemy()
{
    string[] enemyNames = { "Goblin", "Orc", "Dragon", "Skeleton", "Troll" };
    string randomName = enemyNames[Random.Next(enemyNames.Length)];
    return new Enemy(randomName);
}

Item GenerateRandomItem()
{
    string[] itemNames = { "Health Potion", "Strength Elixir", "Defense Shield", "Speed Boots" };
    string randomName = itemNames[Random.Next(itemNames.Length)];
    int strengthBuff = Random.Next(1, 6);
    int defenseBuff = Random.Next(1, 6);
    return new Item(randomName, strengthBuff, defenseBuff);
}

void PlayerTurn(Player player, Enemy enemy)
{
    Console.WriteLine($"Your turn.");

    Console.WriteLine("");
    Console.WriteLine(">>> Command Options:");
    Console.WriteLine("1. Attack");
    Console.WriteLine("2. Heal");
    Console.WriteLine("3. Quit Game");
    Console.Write("Choose an action (1/3): ");
    
    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        switch (choice)
        {
            case 1:
                int playerDamage = Math.Max(player.Attack() - enemy.Defense, 0);
                if (playerDamage > 0)
                {
                    enemy.Health -= playerDamage;
                    Console.WriteLine($"{player.Name} attacks {enemy.Name} for {playerDamage} damage.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|", " ", player.Name, enemy.Name);
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|", "Updated Health:", player.Health, enemy.Health);
                    Console.WriteLine("-------------------------------------------------");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{player.Name} dealt no damage.");
                }
                break;
            case 2:
                player.Heal();
                break;
            case 3:
                Console.WriteLine("Thank you, see you next time!");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Try again.");
    }
}


void EnemyTurn(Player player, Enemy enemy)
{
    Console.WriteLine($"{enemy.Name}'s turn.");
    int enemyDamage = Math.Max(enemy.Attack() - player.Defense, 0);
    if (enemyDamage > 0)
    {
        player.Health -= enemyDamage;
        Console.WriteLine($"{enemy.Name} attacks {player.Name} for {enemyDamage} damage.");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|", " ", player.Name, enemy.Name);
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("|{0,-15}|{1,-15}|{2,-15}|", "Updated Health:", player.Health, enemy.Health);
        Console.WriteLine("-------------------------------------------------");
        Console.ResetColor();
    }
    else
    {
        Console.WriteLine($"{enemy.Name} dealt no damage.");
    }
}
