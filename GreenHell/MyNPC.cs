using System;
using Terraria;
using Terraria.ModLoader;
using GreenHell.Items;


namespace GreenHell {
	class GreenHellNPC : GlobalNPC {
		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			VerdantBlessingItem.SetupShopIf( type, shop, ref nextSlot );
			AntidoteItem.SetupShopIf( type, shop, ref nextSlot );
		}
	}
}
