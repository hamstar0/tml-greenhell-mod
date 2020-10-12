using System;
using Terraria;
using Terraria.ModLoader;
using GreenHell.Buffs;
using HamstarHelpers.Services.Timers;


namespace GreenHell.Logic {
	static partial class GreenHellPlayerLogic {
		public static void UpdateParasiteState( Player player ) {
			var config = GreenHellConfig.Instance;

			if( player.ZoneJungle && player.wet && !player.honeyWet && !player.lavaWet ) {
				if( Timers.GetTimerTickDuration( "GreenHellParasiteCheck" ) == 0 ) {
					Timers.SetTimer( "GreenHellParasiteCheck", 60, false, () => false );

					float chance = config.Get<float>( nameof( config.ParasiteChancePerSecond ) );
					int parasiteDuration = config.Get<int>( nameof( config.ParasiteTickDuration ) );

					if( chance > Main.rand.NextFloat() ) {
						player.AddBuff( ModContent.BuffType<ParasitesDeBuff>(), parasiteDuration );
					}
				}
			}
		}
	}
}
