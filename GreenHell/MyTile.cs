using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreenHell.Logic;


namespace GreenHell {
	class GreenHellTile : GlobalTile {
		public override void KillTile( int i, int j, int tileType, ref bool fail, ref bool effectOnly, ref bool noItem ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}

			GreenHellWorldLogic.SpawnSnakeIfFromGrass( i, j, tileType );
		}
	}
}
