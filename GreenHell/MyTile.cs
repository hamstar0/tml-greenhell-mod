using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreenHell.Logic;


namespace GreenHell {
	class GreenHellTile : GlobalTile {
		public override void KillTile( int i, int j, int tileType, ref bool fail, ref bool effectOnly, ref bool noItem ) {
			if( fail || effectOnly || /*noItem ||*/ Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}
			if( WorldGen.noTileActions || WorldGen.gen ) {
				return;
			}

			GreenHellWorldLogic.SpawnSnakeIfFromGrass( i, j, tileType );
		}
	}
}
