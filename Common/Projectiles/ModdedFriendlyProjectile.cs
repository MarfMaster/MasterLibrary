using Terraria;
using Terraria.ModLoader;

namespace MLib.Common.Projectiles;

/// <summary>
///     A basic projectile. This projectile gets updated twice as often as normal projectiles, to allow for a more accurate
///     hitcheck, so keep that in mind.
/// </summary>
public abstract class ModdedFriendlyProjectile : ModProjectile
{
    /// <summary>
    ///     How many frames the sprite has. Leave at 1 for no animation.
    /// </summary>
    public abstract int Frames { get; }

    /// <summary>
    ///     Determines how many ticks the current frame is displayed for, so higher means the animation is slower. Leave at 0
    ///     if there is no animation.
    /// </summary>
    public abstract int AnimationDuration { get; }

    public abstract int Width { get; }
    public abstract int Height { get; }
    public abstract DamageClass DamageType { get; }

    /// <summary>
    ///     This determines how long the projectile lasts. Terraria runs at 60 ticks per second, so 60 would normally be 1
    ///     second, but this projectile updates twice as often, so 120 is 1 second.
    /// </summary>
    public abstract int LifeSpan { get; }

    public abstract bool PassThroughBlocks { get; }

    /// <summary>
    ///     Determines how often the projectile pierces. Default is 0 for no pierce. For infinite pierce, set this to -2.
    /// </summary>
    public abstract int Pierce { get; }

    /// <summary>
    ///     This determines the rotation of the projectile, either leave this at 0 or use a float in the range of -3.6f to
    ///     3.6f, adjust it in small increments to get it right.
    /// </summary>
    public abstract float RotationHelper { get; }

    public float AITimer1
    {
        get => Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    public float AITimer2
    {
        get => Projectile.ai[1];
        set => Projectile.ai[1] = value;
    }

    public float AITimer3
    {
        get => Projectile.ai[2];
        set => Projectile.ai[2] = value;
    }

    public virtual void CustomSetStaticDefaults()
    {
    }

    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = Frames;
        CustomSetStaticDefaults();
    }

    public virtual void CustomSetDefaults()
    {
    }

    public override void SetDefaults()
    {
        Projectile.width = Width;
        Projectile.height = Height;
        Projectile.DamageType = DamageType;
        Projectile.timeLeft = LifeSpan;
        Projectile.extraUpdates = 1;
        Projectile.friendly = true;
        Projectile.tileCollide = !PassThroughBlocks;
        Projectile.penetrate = 1 + Pierce;
        CustomSetDefaults();
    }

    public virtual void CustomAI()
    {
    }

    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + RotationHelper;

        if (Frames > 1)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= AnimationDuration)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type]) Projectile.frame = 0;
            }
        }

        CustomAI();
    }
}