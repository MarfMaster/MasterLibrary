using Terraria;
using Terraria.ModLoader;

namespace MLib.Common.Buffs;

public abstract class ModdedMinionBuff : ModBuff
{
    public override string LocalizationCategory => "Buffs.Minions";
    public abstract int MinionProjectileType { get; }

    public override void SetStaticDefaults()
    {
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        // If the minions exist reset the buff time, otherwise remove the buff from the player
        if (player.ownedProjectileCounts[MinionProjectileType] > 0)
        {
            player.buffTime[buffIndex] = 18000;
        }
        else
        {
            player.DelBuff(buffIndex);
            buffIndex--;
        }
    }
}