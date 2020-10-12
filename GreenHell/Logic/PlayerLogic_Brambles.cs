using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using GreenHell.Buffs;


namespace GreenHell.Logic {
	static partial class GreenHellPlayerLogic {
		public static void UpdateBrambleState( Player player ) {
			var config = GreenHellConfig.Instance;
			int embrambleDuration = config.Get<int>( nameof( config.EmbrambledDuration ) );
			
			Rectangle plrRect = player.getRect();
			plrRect.X += (int)player.velocity.X;
			plrRect.Y += (int)player.velocity.Y;
			int fromTileX = plrRect.X / 16;
			int fromTileY = plrRect.Y / 16;
			int toTileX = ( plrRect.X + plrRect.Width ) / 16;
			int toTileY = ( plrRect.Y + plrRect.Height ) / 16;

			for( int tileX = fromTileX; tileX <= toTileX; tileX++ ) {
				for( int tileY = fromTileY; tileY <= toTileY; tileY++ ) {
					Tile tile = Main.tile[tileX, tileY];
					if( tile?.active() != true || tile.type != TileID.JungleThorns ) {
						continue;
					}

					player.AddBuff( ModContent.BuffType<EmbrambledDeBuff>(), embrambleDuration );
				}
			}
		}


		public static void UpdateBrambleLifeEffects( Player player ) {
			if( player.lifeRegen > 0 ) {
				if( player.HasBuff( ModContent.BuffType<ParasitesDeBuff>() ) ) {
DebugHelpers.Print( "parasite", "lifeRegen: " + player.lifeRegen + " (" + ( player.lifeRegen / 2 ) + ")" );
					player.lifeRegen /= 2;
					//this.player.lifeRegenTime /= 2;
				}
			}
		}
	}
}
