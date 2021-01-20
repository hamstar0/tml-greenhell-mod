using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;


namespace GreenHell.Buffs {
	partial class ParasitesDeBuff : ModBuff {
		public static void UpdateLifeEffects( Player player ) {
			if( player.lifeRegen > 0 ) {
//DebugHelpers.Print( "parasite", "lifeRegen: " + player.lifeRegen + " (" + ( player.lifeRegen / 2 ) + ")" );
				player.lifeRegen /= 2;
				//player.lifeRegenTime /= 2;
			}
		}



		////////////////
		
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Parasites" );
			this.Description.SetDefault(
				"You are covered in nasty parasites"
				+"\nMay induce bleeding and weakness at random"
			);
			Main.debuff[this.Type] = true;
			Main.buffNoSave[this.Type] = false;
			this.longerExpertDebuff = true;
		}


		////////////////

		public override void Update( Player player, ref int buffIndex ) {
			if( Timers.GetTimerTickDuration( "GreenHellParasiteBleedingCheck" ) > 0 ) {
				return;
			}

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
