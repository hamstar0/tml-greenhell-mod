using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;
using GreenHell.Buffs;


namespace GreenHell.Logic {
	static partial class GreenHellPlayerLogic {
		public static bool UpdateParasiteStateIf( Player player ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				return false;	// `Update`s are responsible for net context
			}

			string timerName = "GreenHellParasiteCheck_"+player.whoAmI;

			if( Timers.GetTimerTickDuration(timerName) <= 0 ) {
				Timers.SetTimer( timerName, 60, false, () => false );

				if( GreenHellPlayerLogic.ApplyParasitesIf(player, Main.netMode == NetmodeID.Server) ) {
					return true;
				}
			}

			return false;
		}


		////

		public static void UpdateLifeEffectsIfParasites( Player player ) {
			if( player.HasBuff( ModContent.BuffType<ParasitesDeBuff>() ) ) {
				ParasitesDeBuff.UpdateLifeEffects( player );
			}
		}


		////////////////

		private static bool ApplyParasitesIf( Player player, bool sync ) {
			if( !player.ZoneJungle || !player.wet || player.honeyWet || player.lavaWet ) {
				return false;
			}

			var myplayer = player.GetModPlayer<GreenHellPlayer>();
			if( myplayer.HasVerdantBlessing() ) {
				return false;
			}

			var config = GreenHellConfig.Instance;
			float chance = config.Get<float>( nameof(config.ParasiteChancePerSecond) );
			bool parasiteGet = chance > Main.rand.NextFloat();

			if( parasiteGet ) {
				ParasitesDeBuff.GiveTo( player, sync );
			}

			return parasiteGet;
		}
	}
}
