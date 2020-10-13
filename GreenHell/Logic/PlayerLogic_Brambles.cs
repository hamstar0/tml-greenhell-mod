using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using GreenHell.Buffs;


namespace GreenHell.Logic {
	static partial class GreenHellPlayerLogic {
		public static bool UpdateBrambleState( GreenHellPlayer myplayer ) {
			if( myplayer.HasVerdantBlessing ) {
				return false;
			}

			var config = GreenHellConfig.Instance;
			int embrambleDuration = config.Get<int>( nameof( config.EmbrambledDuration ) );

			Player plr = myplayer.player;
			Rectangle plrRect = plr.getRect();
			plrRect.X += (int)plr.velocity.X;
			plrRect.Y += (int)plr.velocity.Y;
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

					plr.AddBuff( ModContent.BuffType<EmbrambledDeBuff>(), embrambleDuration );
					return true;
				}
			}

			return false;
		}
	}
}
