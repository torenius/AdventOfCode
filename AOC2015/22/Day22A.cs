namespace AOC2015._22;

public class Day22A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var bossHp = input[0].Split(": ")[1].ToInt();
        var bossDamage = input[1].Split(": ")[1].ToInt();

        // bossHp = 14;
        // bossDamage = 8;

        var minMana = int.MaxValue;
        var winning = new List<Player>();
        var effects = new char[] {'M', 'D', 'S', 'P', 'R'};
        foreach (var e in effects)
        {
            var boss = new Boss {Hp = bossHp, Damage = bossDamage};
            var player = new Player
            {
                Hp = 50,
                Mana = 500
            };
            // player.Hp = 10;
            // player.Mana = 250;
            DoSpell(boss, player, e);
            var result = Simulate(boss, player, effects, e, minMana);
            if (result.Count > 0)
            {
                var min = result.Min(x => x.ManaSpent);
                if (min < minMana)
                {
                    minMana = min;
                }
            }
            winning.AddRange(result.Where(x => x.ManaSpent == minMana));
            PrintElapsedTime($"Spell: {e} Winning: {result.Count}");
        }

        return "" + winning.Min(x => x.ManaSpent);
    }

    private static List<Player> Simulate(Boss boss, Player player, char[] effects, char spell, int minMana)
    {
        // Boss turn
        var playerArmor = DoEffects(boss, player);
        if (boss.Hp <= 0)
        {
            return new List<Player> {player};
        }
        
        var damage = boss.Damage - playerArmor;
        if (damage < 1) damage = 1;
        player.Hp -= damage;
        if (player.Hp <= 0)
        {
            return new List<Player>();
        }
        
        // Player turn
        DoEffects(boss, player);
        var possibleSpells = effects.Where(player.CanCast).ToList();
        if (possibleSpells.Count == 0)
        {
            // Cant cast, so player lose
            return new List<Player>();
        }
        
        var result = new List<Player>();
        foreach (var possibleSpell in possibleSpells)
        {
            var newBoss = new Boss {Hp = boss.Hp, Damage = boss.Damage};
            var newPlayer = new Player
            {
                Hp = player.Hp,
                Mana = player.Mana,
                ShieldEffect = player.ShieldEffect,
                PoisonEffect = player.PoisonEffect,
                RechargeEffect = player.RechargeEffect,
                ManaSpent = player.ManaSpent,
                SpellOrder = player.SpellOrder.ToList()
            };
            DoSpell(newBoss, newPlayer, possibleSpell);
            if (newPlayer.ManaSpent > minMana) continue;
            
            var results = Simulate(newBoss, newPlayer, effects, possibleSpell, minMana);
            result.AddRange(results);
            if (results.Count > 0)
            {
                var min = result.Min(x => x.ManaSpent);
                if (min < minMana)
                {
                    minMana = min;
                }
            }
        }

        return result;
    }

    private static int DoEffects(Boss boss, Player player)
    {
        var playerArmor = player.ShieldEffect > 0 ? 7 : 0;
        boss.Hp -= player.PoisonEffect > 0 ? 3 : 0;
        player.Mana += player.RechargeEffect > 0 ? 101 : 0;
        
        if (player.ShieldEffect > 0) player.ShieldEffect--;
        if (player.PoisonEffect > 0) player.PoisonEffect--;
        if (player.RechargeEffect > 0) player.RechargeEffect--;

        return playerArmor;
    }

    private static void DoSpell(Boss boss, Player player, char spell)
    {
        switch (spell)
        {
            case 'M':
                player.ManaSpent += 53;
                player.Mana -= 53;
                boss.Hp -= 4;
                break;
            case 'D':
                player.ManaSpent += 73;
                player.Mana -= 73;
                player.Hp += 2;
                boss.Hp -= 2;
                break;
            case 'S':
                player.ManaSpent += 113;
                player.Mana -= 113;
                player.ShieldEffect = 6;
                break;
            case 'P':
                player.ManaSpent += 173;
                player.Mana -= 173;
                player.PoisonEffect = 6;
                break;
            case 'R':
                player.ManaSpent += 229;
                player.Mana -= 229;
                player.RechargeEffect = 5;
                break;
        }
        player.SpellOrder.Add(spell);
    }

    private class Player
    {
        public int Hp { get; set; }
        public int Mana { get; set; }
        public int ShieldEffect { get; set; }
        public int PoisonEffect { get; set; }
        public int RechargeEffect { get; set; }
        public int ManaSpent { get; set; } = 0;
        public List<char> SpellOrder { get; set; } = new();

        public bool CanCast(char spell)
        {
            return spell switch
            {
                'M' => Mana >= 53,
                'D' => Mana >= 73,
                'S' => Mana >= 113 && ShieldEffect == 0,
                'P' => Mana >= 173 && PoisonEffect == 0,
                'R' => Mana >= 229 && RechargeEffect == 0,
                _ => false
            };
        }
    }
    
    private class Boss
    {
        public int Hp { get; set; }
        public int Damage { get; set; }
    }
}
