using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace GreenHell.Items {
	public class VerdantBlessingItem : ModItem {
		public static void SetupShopIf( int npcType, Chest shop, ref int nextSlot ) {
			var config = GreenHellConfig.Instance;

			switch( npcType ) {
			case NPCID.WitchDoctor:
				if( !config.Get<bool>( nameof(config.VerdantBlessingByWitchDoctor) ) ) {
					return;
				}
				break;
			case NPCID.Dryad:
				if( !config.Get<bool>( nameof(config.VerdantBlessingSoldByDryad) ) ) {
					return;
				}
				break;
			default:
				return;
			}

			var blessing = new Item();
			blessing.SetDefaults( ModContent.ItemType<VerdantBlessingItem>() );
			
			shop.item[ nextSlot++ ] = blessing;
		}



		////////////////

		public override void SetStaticDefaults() {
			this.Tooltip.SetDefault( "Allows safe passage through the jungle."
				+"\nPrevents infection, parasites, embrambling, and calms snakes"
			);
		}

		public override void SetDefaults() {
			this.item.width = 20;
			this.item.height = 20;
			this.item.accessory = true;
			this.item.value = Item.sellPrice( gold: 3 );
			this.item.rare = ItemRarityID.Pink;
		}


		////////////////

		public override void AddRecipes() {
			var recipe = new VerdantBlessingItemRecipe( this );
			recipe.AddRecipe();
		}
	}
}
