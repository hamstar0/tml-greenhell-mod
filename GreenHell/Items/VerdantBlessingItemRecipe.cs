using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace GreenHell.Items {
	class VerdantBlessingItemRecipe : ModRecipe {
		public VerdantBlessingItemRecipe( VerdantBlessingItem myitem ) : base( GreenHellMod.Instance ) {
			this.AddIngredient( ItemID.LifeCrystal, 2 );
			this.AddIngredient( ItemID.JungleSpores, 10 );
			this.AddIngredient( ItemID.JungleRose, 1 );
			this.AddIngredient( ItemID.DirtRod, 1 );
			this.AddTile( TileID.Anvils );
			this.SetResult( myitem );
		}


		public override bool RecipeAvailable() {
			var config = GreenHellConfig.Instance;
			return config.Get<bool>( nameof(config.VerdantBlessingRecipeEnabled) );
		}
	}
}
