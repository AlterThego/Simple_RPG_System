class Item
{
    public string Name { get; set; }
    public int StrengthBuff { get; set; }
    public int DefenseBuff { get; set; }

    public Item(string name, int strengthBuff, int defenseBuff)
    {
        Name = name;
        StrengthBuff = strengthBuff;
        DefenseBuff = defenseBuff;
    }
}