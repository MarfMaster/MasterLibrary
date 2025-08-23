using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace MLib.Common.NPCs;

public abstract class ModdedHostileNPC : ModNPC
{
    public abstract int TotalAnimationFrames { get; }
    public int FrameDuration { get; set; }
    public abstract int Width { get; }
    public abstract int Height { get; }
    public abstract int BaseLifePoints { get; }
    public abstract int BaseDefense { get; }
    public abstract float BaseKnockbackReceivedMultiplier { get; }
    public abstract int BaseContactDamage { get; }
    public abstract SoundStyle OnHitSound { get; }
    public abstract SoundStyle OnDeathSound { get; }

    /// <summary>
    ///     How many copper coins this enemy will drop on death. 1 silver = 100, 1 gold = 10000, 1 plat = 1000000
    /// </summary>
    public abstract int Value { get; }

    public abstract int BannerItemType { get; }


    public ref float AiState => ref NPC.ai[0]; // set this to any ActionState
    public ref float AiTimer => ref NPC.ai[1];
    public ref float AiTimer2 => ref NPC.ai[2];

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = TotalAnimationFrames;
        CustomSetStaticDefaults();
    }

    //public enum ActionState;
    //Declare and use these two, they're handy and make code readable
    //public enum Frames;

    public virtual void CustomSetStaticDefaults()
    {
    }

    public override void SetDefaults()
    {
        NPC.width = Width;
        NPC.height = Height;
        NPC.aiStyle = -1;
        NPC.lifeMax = BaseLifePoints;
        NPC.defense = BaseDefense;
        NPC.knockBackResist = BaseKnockbackReceivedMultiplier;
        NPC.damage = BaseContactDamage;
        NPC.HitSound = OnHitSound;
        NPC.DeathSound = OnDeathSound;
        NPC.value = Value;

        Banner = Type;
        BannerItem = BannerItemType;
        CustomSetDefaults();
    }

    public virtual void CustomSetDefaults()
    {
    }
}