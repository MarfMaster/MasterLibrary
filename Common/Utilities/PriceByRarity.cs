using Terraria;

namespace MLib.Common.Utilities;

public static class PriceByRarity
{
    //minimal exploration. pre-hardmode ores.
    public static readonly int White_0 = Item.buyPrice(0, 0, 40);

    //underground chest loots (shoe spikes, CiaB, etc), shadow orb items, floating island
    public static readonly int Blue_1 = Item.buyPrice(0, 1, 25);

    //gold dungeon chest (handgun, cobalt shield, etc), goblin invasion
    public static readonly int Green_2 = Item.buyPrice(0, 2, 50);

    //hell, underground jungle
    public static readonly int Orange_3 = Item.buyPrice(0, 3, 50);

    //early hardmode (hm ores), mimics
    public static readonly int LightRed_4 = Item.buyPrice(0, 12, 50);

    //hallowed tier. post mech, pre plantera. pirates.
    public static readonly int Pink_5 = Item.buyPrice(0, 18, 50);

    //some biome mimic gear, high level tinkerer combinations (ankh charm, mechanical glove). seldom used in vanilla
    public static readonly int LightPurple_6 = Item.buyPrice(0, 24, 50);

    //plantera, golem, chlorophyte
    public static readonly int Lime_7 = Item.buyPrice(0, 30, 50);

    //post-plantera dungeon, martian madness, pumpkin/frost moon
    public static readonly int Yellow_8 = Item.buyPrice(0, 36);

    //lunar fragments, dev armor. seldom used in vanilla
    public static readonly int Cyan_9 = Item.buyPrice(0, 42, 50);

    //luminite, lunar fragment gear, moon lord drops
    public static readonly int Red_10 = Item.buyPrice(0, 48);

    //no vanilla items have purple rarity base. only cyan and red with modifiers can be purple. im guessing on the price.
    public static readonly int Purple_11 = Item.buyPrice(0, 54);

    // Returns whether the value is within the lower and upper bounds, inclusive
    public static bool InRange(int x, int lower, int upper)
    {
        return x >= lower && x <= upper;
    }

    public static int fromItem(Item item)
    {
        // Item has a vanilla rarity
        if (InRange(item.rare, 0, 11)) return fromRarity(item.rare);

        return White_0;
    }

    public static int fromRarity(int rarity)
    {
        switch (rarity)
        {
            case 0:
            {
                return White_0;
            }
            case 1:
            {
                return Blue_1;
            }
            case 2:
            {
                return Green_2;
            }
            case 3:
            {
                return Orange_3;
            }
            case 4:
            {
                return LightRed_4;
            }
            case 5:
            {
                return Pink_5;
            }
            case 6:
            {
                return LightPurple_6;
            }
            case 7:
            {
                return Lime_7;
            }
            case 8:
            {
                return Yellow_8;
            }
            case 9:
            {
                return Cyan_9;
            }
            case 10:
            {
                return Red_10;
            }
            case 11:
            {
                return Purple_11;
            }
        }

        return 0;
    }
}