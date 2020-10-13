using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreenHell.Items;


namespace GreenHell {
	class GreenHellNPC : GlobalNPC {
		public override void SetupShop( int type, Chest shop, ref int nextSlot ) {
			switch( type ) {
			case NPCID.Dryad:
				VerdantBlessingItem.SetupShopIf( shop, ref nextSlot );
				break;
			case NPCID.WitchDoctor:
				AntidoteItem.SetupShopIf( shop, ref nextSlot );
				break;
			}
		}
	}
}
