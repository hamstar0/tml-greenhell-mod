using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Timers;


namespace GreenHell.Buffs {
	public partial class InfectionDeBuff : ModBuff {
		public static int ComputeDamage( Player player, int infectionStage ) {
			if( player.mount.Active ) {
				return 0;
			}

			var config = GreenHellConfig.Instance;
			float scale = config.Get<float>( nameof(config.InfectionDamagePerVelocityScale) );

			float vel = Math.Abs( player.velocity.X );
			vel += (float)player.jump / (float)Player.jumpHeight;
			vel = vel <= 0.5f ? 0f : vel - 0.5f;

			return (int)( vel * (float)infectionStage * scale );
		}


		////////////////

		public static void UpdateLifeEffects( Player player ) {
			if( player.dead || player.velocity.Y != 0 ) {
				return;
			}

			var myplayer = player.GetModPlayer<GreenHellPlayer>();
			int dmg = InfectionDeBuff.ComputeDamage( player, myplayer.InfectionStage );
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
					Timers.SetTimer( "GreenHellInfectionAlert", 60 * 10, false, () => false );

					Main.NewText( "Movements agitate your condition.", Color.OrangeRed );
				}
			}
		}
	}
}
