using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using GreenHell.NPCs;


namespace GreenHell.Logic {
	class GreenHellWorldData : ILoadable {
		internal IDictionary<int, int> JungleFoliageClearedAt = new ConcurrentDictionary<int, int>();



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnModsUnload() { }

		void ILoadable.OnPostModsLoad() { }
	}




	static partial class GreenHellWorldLogic {
		public static void SpawnSnakeIfFromGrass( int i, int j, int tileType ) {
			if( tileType != TileID.JungleGrass
					&& tileType != TileID.JunglePlants
					&& tileType != TileID.JunglePlants2
					&& tileType != TileID.JungleVines ) {
				return;
			}

			var config = GreenHellConfig.Instance;
			float spawnChance = config.Get<float>( nameof(config.SnakeSpawnChanceFromGrassCut) );

			if( Main.rand.NextFloat() < spawnChance ) {
				var myworlddata = ModContent.GetInstance<GreenHellWorldData>();

				if( !myworlddata.JungleFoliageClearedAt.ContainsKey(i) || myworlddata.JungleFoliageClearedAt[i] != j ) {
					myworlddata.JungleFoliageClearedAt[i] = j;

					int npcWho = NPC.NewNPC( i * 16, j * 16, ModContent.NPCType<JungleSnakeNPC>() );
					if( Main.netMode == NetmodeID.Server ) {
						NetMessage.SendData( MessageID.SyncNPC, -1, -1, null, npcWho );
					}
				}
			}
		}
	}
}
