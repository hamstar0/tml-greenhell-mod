using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using GreenHell.Buffs;


namespace GreenHell.Logic {
	static partial class GreenHellPlayerLogic {
		public static bool UpdateBrambleStateIf( GreenHellPlayer myplayer ) {
			if( myplayer.HasVerdantBlessing() ) {
				return false;
			}

			var config = GreenHellConfig.Instance;
			int embrambleDuration = config.Get<int>( nameof( config.EmbrambledDuration ) );

			Player plr = myplayer.player;
			Rectangle plrRect = plr.getRect();
			plrRect.X += (int)plr.velocity.X;
			plrRect.Y += (int)plr.velocity.Y;
			int fromTileX = Math.Max( plrRect.Left / 16, 0 );
			int fromTileY = Math.Max( plrRect.Top / 16, 0 );
			int toTileX = Math.Min( plrRect.Right / 16, Main.maxTilesX );
			int toTileY = Math.Min( plrRect.Bottom / 16, Main.maxTilesY );

			for( int tileX = fromTileX; tileX <= toTileX; tileX++ ) {
				for( int tileY = fromTileY; tileY <= toTileY; tileY++ ) {
					Tile tile = Main.tile[ tileX, tileY ];

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
