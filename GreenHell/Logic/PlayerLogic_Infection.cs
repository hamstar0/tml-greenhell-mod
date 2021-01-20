using System;
using Terraria;
using Terraria.ModLoader;
using GreenHell.Buffs;


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

		public static void UpdateInfectionStateIf( GreenHellPlayer myplayer ) {
			if( myplayer.player.dead ) {
				myplayer.InfectionStage = 0;
			}

			int infBuffType = ModContent.BuffType<InfectionDeBuff>();

			if( myplayer.InfectionStage == 0 ) {
				myplayer.player.ClearBuff( infBuffType );
			} else if( !myplayer.player.HasBuff(infBuffType) ) {
				//myplayer.player.AddBuff( infBuffType, 2 );
				myplayer.InfectionStage = 0;
			}
		}

		////

		public static void UpdateLifeEffectsIfInfection( Player player ) {
			if( player.HasBuff( ModContent.BuffType<InfectionDeBuff>() ) ) {
				InfectionDeBuff.UpdateLifeEffects( player );
			}
		}
	}
}
