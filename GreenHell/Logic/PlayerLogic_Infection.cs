using GreenHell.Buffs;
using System;
using Terraria;


namespace GreenHell.Logic {
	static partial class GreenHellPlayerLogic {
		public static bool ApplyInfectionIf( GreenHellPlayer myplayer, double damage ) {
			if( !myplayer.player.ZoneJungle || myplayer.HasVerdantBlessing ) {
				return false;
			}

			var config = GreenHellConfig.Instance;
			float infectionChancePerChunk = config.Get<float>( nameof( config.InfectionChancePer10LifeLostInJungle ) );

			for( ; damage > 10d; damage -= 10d ) {
				if( Main.rand.NextFloat() < infectionChancePerChunk ) {
					InfectionDeBuff.GiveTo( myplayer );
					return true;
				}
			}

			return false;
		}


		////////////////

		public static int GetInfectionMaxLife( int currentMaxLife, int infectionStage ) {
			float extraHp = currentMaxLife - 100;
			float infectionPercent = (float)infectionStage / (float)InfectionDeBuff.Stages;

			return 100 + (int)( extraHp * (1f - infectionPercent) );
		}


		////////////////

		public static void UpdateInfectionEffects( GreenHellPlayer myplayer ) {
			myplayer.player.statLifeMax2 = GreenHellPlayerLogic.GetInfectionMaxLife(
				myplayer.player.statLifeMax2,
				myplayer.InfectionStage
			);
		}
	}
}
