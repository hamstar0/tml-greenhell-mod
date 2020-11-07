using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;


namespace GreenHell.Buffs {
	class ParasitesDeBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Parasites" );
			this.Description.SetDefault( "You are covered in nasty parasites" );
			Main.debuff[this.Type] = true;
			this.longerExpertDebuff = true;
		}

		public override void Update( Player player, ref int buffIndex ) {
			if( Timers.GetTimerTickDuration( "GreenHellParasiteBleedingCheck" ) == 0 ) {
				Timers.SetTimer( "GreenHellParasiteBleedingCheck", 60, false, () => false );

				var config = GreenHellConfig.Instance;
				float bleedChance = config.Get<float>( nameof(config.ParasiteAfflictChancePerSecond) );

				if( bleedChance > Main.rand.NextFloat() ) {
					player.AddBuff( BuffID.Bleeding, 60 * 5 );
					player.AddBuff( BuffID.Weak, 60 * 5 );
				}
			}
		}
	}
}
