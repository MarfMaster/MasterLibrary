using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MLib.Common.Buffs;

public abstract class ModdedFlaskBuff : ModBuff
{
    public override string LocalizationCategory => "Buffs.Flasks";

    public override void SetStaticDefaults()
    {
        BuffID.Sets.IsAFlaskBuff[Type] = true;
        Main.meleeBuff[Type] = true;
        Main.persistentBuff[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        player.MeleeEnchantActive = true;
    }
}