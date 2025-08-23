using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace MLib.Common.Items;

public abstract class ModdedBossBag : ModItem
{
    public override string LocalizationCategory => "Items.TreasureBags";
    public abstract bool PreHardmodeBossBag { get; }

    public override void SetStaticDefaults()
    {
        ItemID.Sets.BossBag[Type] = true;
        ItemID.Sets.PreHardmodeLikeBossBag[Type] = PreHardmodeBossBag;
    }

    public override void SetDefaults()
    {
        Item.maxStack = Item.CommonMaxStack;
        Item.consumable = true;
        Item.width = 24;
        Item.height = 24;
        Item.rare = ItemRarityID.Cyan;
        Item.expert = true;
    }

    public override bool CanRightClick()
    {
        return true;
    }

    public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor,
        Color itemColor, Vector2 origin, float scale)
    {
        var texture = (Texture2D)TextureAssets.Item[Item.type];
        for (var i = 0; i < 4; ++i)
        {
            var offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 3;
            spriteBatch.Draw(texture, position + offsetPositon, null, Main.DiscoColor, 0, origin, scale,
                SpriteEffects.None, 0);
        }

        return true;
    }

    public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation,
        ref float scale, int whoAmI)
    {
        //UsefulFunctions.DustRing(Item.Center, 32, DustID.ShadowbeamStaff);
        var texture = (Texture2D)TextureAssets.Item[Item.type];

        Lighting.AddLight(Item.Center, Main.DiscoColor.ToVector3());
        for (var i = 0; i < 4; ++i)
        {
            var offsetPositon = Vector2.UnitY.RotatedBy(Main.GameUpdateCount % 300 / 30f + MathHelper.PiOver2 * i) * 5;
            spriteBatch.Draw(texture,
                offsetPositon + new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f),
                new Rectangle(0, 0, texture.Width, texture.Height), Main.DiscoColor, rotation, texture.Size() * 0.5f,
                scale, SpriteEffects.None, 0);
        }

        return true;
    }
}