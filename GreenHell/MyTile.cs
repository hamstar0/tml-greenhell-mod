using GreenHell.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace GreenHell {
	class GreenHellTile : GlobalTile {
		public override void KillTile( int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return;
			}
				
			Tile tile = Main.tile[i, j];
			if( type != TileID.JungleGrass && type != TileID.JunglePlants && type != TileID.JunglePlants2 ) {
				return;
			}

			var config = GreenHellConfig.Instance;
			float spawnChance = config.Get<float>( nameof(config.SnakeSpawnChanceFromGrassCut) );

			if( Main.rand.NextFloat() > spawnChance ) {
				NPC.NewNPC( i*16, j*16, ModContent.NPCType<JungleSnakeNPC>() );
			}
		}
	}
}
