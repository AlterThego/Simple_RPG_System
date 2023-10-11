class Player
{
    public string? Name { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
    public bool CanHeal { get; set; }
    public Inventory Inventory { get; set; } = new Inventory(); // Initialize the Inventory property

    public Player(string? name)
    {
        Name = name;
        RandomizeStats();
        CanHeal = true;
    }

    private void RandomizeStats()
    {
        Random rand = new Random();
        Health = rand.Next(20, 36);
        Strength = rand.Next(99, 100);
        Defense = rand.Next(0, 6);
        Speed = rand.Next(1, 11);
    }

    public int Attack()
    {
        return Strength;
    }

    public void Heal()
    {
        if (CanHeal)
        {
            int healAmount = 10;
            Health += healAmount;
            CanHeal = false;
            Console.WriteLine($"{Name} healed for {healAmount} HP.");
        }
        else
        {
            Console.WriteLine($"You can't heal yet. Wait for the cooldown.");
        }
    }

    public bool IsAlive()
    {
        return Health > 0;
    }
}