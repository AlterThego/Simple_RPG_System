class Inventory
{
    private List<Item> items = new List<Item>();

    public Inventory AddItem(Item item)
    {
        items.Add(item);
        return this; // Method chaining
    }

    public void UseItem(Item item, Player player)
    {
        if (items.Contains(item))
        {
            player.Strength += item.StrengthBuff;
            player.Defense += item.DefenseBuff;
            items.Remove(item);
            Console.WriteLine($"{player.Name} used {item.Name}.");
        }
        else
        {
            Console.WriteLine($"{player.Name} doesn't have {item.Name} in the inventory.");
        }
    }

    public void GetItems(Player player)
    {
        Console.WriteLine($"Inventory of {player.Name}:");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name} (+{item.StrengthBuff} Strength, +{item.DefenseBuff} Defense)");
        }
    }
}