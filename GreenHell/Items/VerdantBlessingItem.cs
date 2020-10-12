using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace GreenHell.Items {
	class VerdantBlessingItem : ModItem {
		public override void SetStaticDefaults() {
			this.Tooltip.SetDefault( "Heals you by 20 health for every 200 mana consumed." );
		}

		public override void SetDefaults() {
			this.item.width = 20;
			this.item.height = 20;
			this.item.accessory = true;
			this.item.value = Item.sellPrice( silver: 30 );
			this.item.rare = ItemRarityID.Blue;
		}

		public override void UpdateAccessory( Player player, bool hideVisual ) {
			player.GetModPlayer<GreenHellPlayer>().HasVerdantBlessing = true;
		}


		////

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe( mod );
			recipe.AddIngredient( ItemID.LifeCrystal, 2 );
			recipe.AddIngredient( ItemID.ManaCrystal, 2 );
			recipe.AddTile( TileID.Anvils );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}
