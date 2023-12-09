namespace AOC2015._21;

public class Day21A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var bossHp = input[0].Split(": ")[1].ToInt();
        var bossDamage = input[1].Split(": ")[1].ToInt();
        var bossArmor = input[2].Split(": ")[1].ToInt();
        var playerHp = 100;
        
        // ceiling (bossHp / (playerDamage - boss armor)) = number of turns
        // (Boss damage - player armor) * (number of turns -1) >= player HP = loss
        // (Boss damage - player armor) * (number of turns -1) < player HP = win

        var weapons = Shop.Where(x => x.ItemType == ItemType.Weapon).ToList();
        var armors = Shop.Where(x => x.ItemType == ItemType.Armor).ToList();
        var rings = Shop.Where(x => x.ItemType == ItemType.Ring).ToArray();
        var winningGear = new List<Gear>();
        foreach (var weapon in weapons)
        {
            foreach (var armor in armors)
            {
                for (var i = 0; i < rings.Length - 1; i++)
                {
                    for (var k = i + 1; k < rings.Length; k++)
                    {
                        var gear = new Gear
                        {
                            Weapon = weapon,
                            Armor = armor,
                            RingOne = rings[i],
                            RingTwo = rings[k]
                        };

                        var playerDamage = gear.TotalDamage - bossArmor;
                        if (playerDamage < 1)
                        {
                            playerDamage = 1;
                        }

                        var numberOfTurns = (int)Math.Ceiling(bossHp * 1.0 / playerDamage);
                        if ((bossDamage - gear.TotalArmor) * (numberOfTurns - 1) < playerHp)
                        {
                            winningGear.Add(gear);
                        }
                    }
                }
            }
        }

        var minCost = winningGear.MinBy(x => x.TotalCost);
        
        return "" + minCost.TotalCost;
    }

    private class Gear
    {
        public Item Weapon { get; set; }
        public Item Armor { get; set; }
        public Item RingOne { get; set; }
        public Item RingTwo { get; set; }

        public int TotalDamage => Weapon.Damage + RingOne.Damage + RingTwo.Damage;
        public int TotalArmor => Armor.Armor + RingOne.Armor + RingTwo.Armor;
        public int TotalCost => Weapon.Cost + Armor.Cost + RingOne.Cost + RingTwo.Cost;

        public override string ToString()
        {
            return $"Dmg: {TotalDamage} Def: {TotalArmor} Cost: {TotalCost}";
        }
    }

    private List<Item> Shop = new List<Item>
    {
        new() {Name = "Dagger", Cost = 8, Damage = 4, Armor = 0, ItemType = ItemType.Weapon},
        new() {Name = "Shortsword", Cost = 10, Damage = 5, Armor = 0, ItemType = ItemType.Weapon},
        new() {Name = "Warhammer", Cost = 25, Damage = 6, Armor = 0, ItemType = ItemType.Weapon},
        new() {Name = "Longsword", Cost = 40, Damage = 7, Armor = 0, ItemType = ItemType.Weapon},
        new() {Name = "Greataxe", Cost = 74, Damage = 8, Armor = 0, ItemType = ItemType.Weapon},
        
        new() {Name = "No Armor", Cost = 0, Damage = 0, Armor = 0, ItemType = ItemType.Armor},
        new() {Name = "Leather", Cost = 13, Damage = 0, Armor = 1, ItemType = ItemType.Armor},
        new() {Name = "Chainmail", Cost = 31, Damage = 0, Armor = 2, ItemType = ItemType.Armor},
        new() {Name = "Splintmail", Cost = 53, Damage = 0, Armor = 3, ItemType = ItemType.Armor},
        new() {Name = "Bandedmail", Cost = 75, Damage = 0, Armor = 4, ItemType = ItemType.Armor},
        new() {Name = "Platemail", Cost = 102, Damage = 0, Armor = 5, ItemType = ItemType.Armor},
        
        new() {Name = "No Ring 1", Cost = 0, Damage = 0, Armor = 0, ItemType = ItemType.Ring},
        new() {Name = "No Ring 2", Cost = 0, Damage = 0, Armor = 0, ItemType = ItemType.Ring},
        new() {Name = "Damage +1", Cost = 25, Damage = 1, Armor = 0, ItemType = ItemType.Ring},
        new() {Name = "Damage +2", Cost = 50, Damage = 2, Armor = 0, ItemType = ItemType.Ring},
        new() {Name = "Damage +3", Cost = 100, Damage = 3, Armor = 0, ItemType = ItemType.Ring},
        new() {Name = "Defense +1", Cost = 20, Damage = 0, Armor = 1, ItemType = ItemType.Ring},
        new() {Name = "Defense +2", Cost = 40, Damage = 0, Armor = 2, ItemType = ItemType.Ring},
        new() {Name = "Defense +3", Cost = 80, Damage = 0, Armor = 3, ItemType = ItemType.Ring}
    };
    private class Item
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public ItemType ItemType { get; set; }
    }

    private enum ItemType
    {
        Weapon,
        Armor,
        Ring
    }
}