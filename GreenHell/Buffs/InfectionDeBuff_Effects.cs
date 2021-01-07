using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;


namespace GreenHell.Buffs {
	partial class InfectionDeBuff : ModBuff {
		public static void UpdateLifeEffects( Player player ) {
			if( player.velocity.Y != 0 ) {
				return;
			}

			var myplayer = player.GetModPlayer<GreenHellPlayer>();
			float vel = Math.Abs( player.velocity.X );
			vel = vel <= 1f ? 0f : vel - 1f;
			int dmg = (int)(vel * (float)myplayer.InfectionStage);
//DebugHelpers.Print( "infection", "dmg: "+dmg.ToString()+", stage: "+myplayer.InfectionStage+", vel: "+vel );

			player.lifeRegen = -dmg;
			/*player.Hurt(
				damageSource: PlayerDeathReason.ByCustomReason("didn't wash their hands"),
				Damage: dmg,
				hitDirection: 0,
				pvp: false,
				quiet: true,
				Crit: false
			);*/

			if( dmg > 0 ) {
				if( Timers.GetTimerTickDuration( "GreenHellInfectionAlert" ) == 0 ) {
					Timers.SetTimer( "GreenHellInfectionAlert", 60 * 5, false, () => {
						Main.NewText( "Movements agitate your condition.", Color.OrangeRed );
						return false;
					} );
				}
			}
		}
	}
}
