class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }

    public Enemy(string name)
    {
        Name = name;
        RandomizeStats();
    }

    private void RandomizeStats()
    {
        Random rand = new Random();
        Health = rand.Next(20, 36);
        Strength = rand.Next(5, 11);
        Defense = rand.Next(0, 6);
        Speed = rand.Next(1, 11);
        
    }

    public int Attack()
    {
        return Strength;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }
}