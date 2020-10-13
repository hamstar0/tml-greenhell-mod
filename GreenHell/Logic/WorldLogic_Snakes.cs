using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreenHell.NPCs;


namespace GreenHell.Logic {
	static partial class GreenHellWorldLogic {
		public static void SpawnSnakeIfFromGrass( int i, int j, int tileType ) {
			if( tileType != TileID.JungleGrass
					&& tileType != TileID.JunglePlants
					&& tileType != TileID.JunglePlants2
					&& tileType != TileID.JungleVines ) {
				return;
			}

			var config = GreenHellConfig.Instance;
			float spawnChance = config.Get<float>( nameof( config.SnakeSpawnChanceFromGrassCut ) );

			if( Main.rand.NextFloat() > spawnChance ) {
				NPC.NewNPC( i * 16, j * 16, ModContent.NPCType<JungleSnakeNPC>() );
			}
		}
	}
}
