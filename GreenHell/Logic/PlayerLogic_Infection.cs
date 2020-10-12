using GreenHell.Buffs;
using System;
using Terraria;


namespace GreenHell.Logic {
	static partial class GreenHellPlayerLogic {
		public static void ApplyInfectionIf( GreenHellPlayer myplayer, double damage ) {
			var config = GreenHellConfig.Instance;
			float infectionChancePerChunk = config.Get<float>( nameof( config.InfectionChancePer10LifeLostInJungle ) );

			for( ; damage > 10d; damage -= 10d ) {
				if( Main.rand.NextFloat() < infectionChancePerChunk ) {
					InfectionDeBuff.GiveTo( myplayer );
					break;
				}
			}
		}


		public static void UpdateInfectionEffects( GreenHellPlayer myplayer ) {
			float extraHp = myplayer.player.statLifeMax2 - 100;
			float infectionPercent = (float)myplayer.InfectionStage / (float)InfectionDeBuff.Stages;

			myplayer.player.statLifeMax2 = 100 + (int)( extraHp * ( 1f - infectionPercent ) );
		}
	}
}
