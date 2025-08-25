using Terraria.ID;
using Terraria.ModLoader;

namespace MLib.Common.Projectiles;

public abstract class ModdedSandballProjectile : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.FallingBlockDoesNotFallThroughPlatforms[Type] = true;
        ProjectileID.Sets.ForcePlateDetection[Type] = true;
    }
}