using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MLib.Common.Tiles.Furniture;

public abstract class ModdedTorchTile : ModTile
{
    private Asset<Texture2D> flameTexture;
    public abstract int SparkleDustType { get; }
    public abstract Color LightColor { get; }
    public abstract Color MapColor { get; }
    public abstract bool CanFunctionInLiquids { get; }
    public abstract int VanillaFallbackTile { get; }

    /// <summary>
    ///     Whether this is a biome-related torch or not. If it's not biome-related, it won't affect luck.
    ///     Biome torches grant luck if they're placed in their respective biomes.
    /// </summary>
    public abstract bool BelongsToAModdedBiome { get; }

    /// <summary>
    ///     The ModBiome this torch belongs to. Leave at get; if there isn't one and BelongsToAModdedBiome is false.
    /// </summary>
    public abstract ModBiome ModdedBiomeForLuck { get; }

    public override void SetStaticDefaults()
    {
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileSolid[Type] = false;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileWaterDeath[Type] = CanFunctionInLiquids;
        TileID.Sets.FramesOnKillWall[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.DisableSmartInteract[Type] = true;
        TileID.Sets.Torch[Type] = true;

        DustType = SparkleDustType;
        AdjTiles = [TileID.Torches];
        VanillaFallbackOnModDeletion = (ushort)VanillaFallbackTile;

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);

        // Placement
        TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Torches, 0));
        /*  This is what is copied from the Torches tile
        TileObjectData.newTile.CopyFrom(TileObjectData.StyleTorch);
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
        TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
        TileObjectData.newAlternate.AnchorAlternateTiles = [124, 561, 574, 575, 576, 577, 578];
        TileObjectData.addAlternate(1);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
        TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.Tree | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
        TileObjectData.newAlternate.AnchorAlternateTiles = [124, 561, 574, 575, 576, 577, 578];
        TileObjectData.addAlternate(2);
        TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
        TileObjectData.newAlternate.AnchorWall = true;
        TileObjectData.addAlternate(0);
        */
        TileObjectData.newTile.WaterDeath = !CanFunctionInLiquids;
        TileObjectData.newTile.WaterPlacement =
            CanFunctionInLiquids ? LiquidPlacement.Allowed : LiquidPlacement.NotAllowed;
        TileObjectData.newTile.LavaDeath = !CanFunctionInLiquids;
        TileObjectData.newTile.LavaPlacement = CanFunctionInLiquids ? LiquidPlacement.Allowed : LiquidPlacement.NotAllowed;

        TileObjectData.addTile(Type);

        // Etc
        AddMapEntry(MapColor, Language.GetText("ItemName.Torch"));

        // Assets
        flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
    }

    public override void MouseOver(int i, int j)
    {
        var player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;

        // We can determine the item to show on the cursor by getting the tile style and looking up the corresponding item drop.
        var style = TileObjectData.GetTileStyle(Main.tile[i, j]);
        player.cursorItemIconID = TileLoader.GetItemDropFromTypeAndStyle(Type, style);
    }

    public override float GetTorchLuck(Player player)
    {
        // GetTorchLuck is called when there is an ExampleTorch nearby the client player
        // In most use-cases you should return 1f for a good luck torch, or -1f for a bad luck torch.
        // You can also add a smaller amount (eg 0.5) for a smaller positive/negative luck impact.
        // Remember that the overall torch luck is decided by every torch around the player, so it may be wise to have a smaller amount of luck impact.
        // Multiple example torches on screen will have no additional effect.

        // Positive and negative luck are accumulated separately and then compared to some fixed limits in vanilla to determine overall torch luck.
        // Positive luck is capped at 1, any value higher won't make any difference and negative luck is capped at 2.
        // A negative luck of 2 will cancel out all torch luck bonuses.

        // The influence positive torch luck can have overall is 0.1 (if positive luck is any number less than 1) or 0.2 (if positive luck is greater than or equal to 1)

        if (!BelongsToAModdedBiome) return 0f;

        var InModBiome = player.InModBiome(ModdedBiomeForLuck);
        return InModBiome ? 1f : -0.1f;
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        num = Main.rand.Next(1, 3);
    }

    public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        var tile = Main.tile[i, j];

        if (tile.TileFrameX < 66)
        {
            r = LightColor.R;
            g = LightColor.G;
            b = LightColor.B;
        }
    }

    public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height,
        ref short tileFrameX, ref short tileFrameY)
    {
        // This code slightly lowers the draw position if there is a solid tile above, so the flame doesn't overlap that tile. Terraria torches do this same logic.
        offsetY = 0;

        if (WorldGen.SolidTile(i, j - 1)) offsetY = 4;
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        var tile = Main.tile[i, j];

        if (!TileDrawing.IsVisible(tile)) return;

        // The following code draws multiple flames on top our placed torch.

        var offsetY = 0;

        if (WorldGen.SolidTile(i, j - 1)) offsetY = 4;

        var zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

        var randSeed = Main.TileFrameSeed ^ (ulong)(((long)j << 32) | (uint)i); // Don't remove any casts.
        var width = 20;
        var height = 20;
        int frameX = tile.TileFrameX;
        int frameY = tile.TileFrameY;

        for (var k = 0; k < 7; k++)
        {
            var xx = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
            var yy = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;

            spriteBatch.Draw(flameTexture.Value,
                new Vector2(i * 16 - (int)Main.screenPosition.X - (width - 16f) / 2f + xx,
                    j * 16 - (int)Main.screenPosition.Y + offsetY + yy) + zero,
                new Rectangle(frameX, frameY, width, height), LightColor, 0f, default, 1f, SpriteEffects.None, 0f);
        }
    }

    public override void EmitParticles(int i, int j, Tile tileCache, short tileFrameX, short tileFrameY,
        Color tileLight, bool visible)
    {
        if (!visible) return;

        if (Main.rand.NextBool(40) && tileFrameX < 66)
        {
            var dustChoice = SparkleDustType;
            Dust dust;
            var spawnPosition = tileFrameX switch
            {
                22 => new Vector2(i * 16 + 6, j * 16),
                44 => new Vector2(i * 16 + 2, j * 16),
                _ => new Vector2(i * 16 + 4, j * 16)
            };

            dust = Dust.NewDustDirect(new Vector2(i * 16 + 4, j * 16), 4, 4, dustChoice, 0f, 0f, 100);

            if (!Main.rand.NextBool(3)) dust.noGravity = true;

            dust.velocity *= 0.3f;
            dust.velocity.Y -= 1.5f;
        }
    }
}