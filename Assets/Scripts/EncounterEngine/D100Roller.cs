using System;

public class D100Roller
{
    Random randomizer;
    public D100Roller(int seed)
    {
        randomizer = new Random(seed);
    }

    public bool DoRoll(int hitChance, int modifier)
    {
        var roll = randomizer.Next(1, 101);
        return roll <= hitChance+modifier;
    }
}
