using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreenHell.Buffs;


namespace GreenHell.Items {
	class PanaceaItem : ModItem {
		public static void SetupShopIf( int npcType, Chest shop, ref int nextSlot ) {
			var config = GreenHellConfig.Instance;

			switch( npcType ) {
			case NPCID.WitchDoctor:
				if( !config.Get<bool>( nameof(config.PanaceaSoldByWitchDoctor) ) ) {
					return;
				}
				break;
			case NPCID.Dryad:
				if( !config.Get<bool>( nameof(config.PanaceaSoldByDryad) ) ) {
					return;
				}
				break;
			default:
				return;
			}

			var panacea = new Item();
			panacea.SetDefaults( ModContent.ItemType<PanaceaItem>(), true );

			shop.item[nextSlot++] = panacea;
		}



		////////////////

		public override void SetStaticDefaults() {
			this.Tooltip.SetDefault( "Removes common non-magic maladies and protects against poisons for a while" );
		}

		public override void SetDefaults() {
			this.item.width = 20;
			this.item.height = 26;
			this.item.useStyle = ItemUseStyleID.EatingUsing;
			this.item.useAnimation = 17;
			this.item.useTime = 17;
			this.item.useTurn = true;
			this.item.UseSound = SoundID.Item3;
			this.item.maxStack = 30;
			this.item.consumable = true;
			this.item.rare = ItemRarityID.Orange;
			//this.item.potion = true;
			this.item.value = Item.buyPrice( gold: 1 );
		}

		////////////////

		public override void OnConsumeItem( Player player ) {
			var config = GreenHellConfig.Instance;
			int duration = config.Get<int>( nameof(config.PanaceaBuffTickDuration) );

			player.AddBuff( ModContent.BuffType<AntiveninBuff>(), duration );
			player.ClearBuff( BuffID.Darkness );
			player.ClearBuff( BuffID.Blackout );
			player.ClearBuff( BuffID.Weak );
			player.ClearBuff( BuffID.Confused );
			player.ClearBuff( BuffID.Rabies );
			player.ClearBuff( BuffID.Tipsy );
			player.ClearBuff( ModContent.BuffType<InfectionDeBuff>() );
			player.ClearBuff( ModContent.BuffType<ParasitesDeBuff>() );
		}
	}
}
