using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreenHell.Buffs;


namespace GreenHell.Items {
	class AntidoteItem : ModItem {
		public static void SetupShopIf( Chest shop, ref int nextSlot ) {
			var config = GreenHellConfig.Instance;

			if( !config.Get<bool>( nameof( config.AntidoteSoldByWitchDoctor ) ) ) {
				return;
			}

			var antidote = new Item();
			antidote.SetDefaults( ModContent.ItemType<AntidoteItem>(), true );

			shop.item[nextSlot++] = antidote;
		}



		////////////////

		public override void SetStaticDefaults() {
			this.Tooltip.SetDefault( "Removes and protects against poisons for a while" );
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
			int duration = config.Get<int>( nameof(config.AntidoteBuffTickDuration) );

			player.AddBuff( ModContent.BuffType<AntiveninBuff>(), duration );
		}
	}
}
