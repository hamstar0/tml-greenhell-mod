using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace GreenHell.Items {
	class VerdantBlessingItem : ModItem {
		public static void SetupShopIf( int npcType, Chest shop, ref int nextSlot ) {
			if( npcType != NPCID.Dryad ) {
				return;
			}

			var config = GreenHellConfig.Instance;
			if( !config.Get<bool>( nameof( config.VerdantBlessingSoldByDryad ) ) ) {
				return;
			}

			var blessing = new Item();
			blessing.SetDefaults( ModContent.ItemType<VerdantBlessingItem>(), true );

			shop.item[ nextSlot++ ] = blessing;
		}



		////////////////

		public override void SetStaticDefaults() {
			this.Tooltip.SetDefault( "Allows safe passage through the jungle." );
		}

		public override void SetDefaults() {
			this.item.width = 20;
			this.item.height = 20;
			this.item.accessory = true;
			this.item.value = Item.sellPrice( gold: 3 );
			this.item.rare = ItemRarityID.Pink;
		}


		////////////////

		public override void UpdateAccessory( Player player, bool hideVisual ) {
			player.GetModPlayer<GreenHellPlayer>().HasVerdantBlessing = true;
		}


		////////////////

		public override void AddRecipes() {
			var config = GreenHellConfig.Instance;
			if( !GreenHellConfig.Instance.Get<bool>( nameof(config.VerdantBlessingRecipeEnabled) ) ) {
				return;
			}

			var recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.LifeCrystal, 2 );
			recipe.AddIngredient( ItemID.JungleSpores, 10 );
			recipe.AddIngredient( ItemID.JungleRose, 1 );
			recipe.AddIngredient( ItemID.DirtRod, 1 );
			recipe.AddTile( TileID.Anvils );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}
