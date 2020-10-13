﻿using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;
using GreenHell.Buffs;


namespace GreenHell.Logic {
	static partial class GreenHellPlayerLogic {
		public static bool UpdateParasiteState( Player player ) {
			if( !player.ZoneJungle || !player.wet || player.honeyWet || player.lavaWet ) {
				return false;
			}

			var myplayer = player.GetModPlayer<GreenHellPlayer>();
			if( myplayer.HasVerdantBlessing ) {
				return false;
			}

			if( Timers.GetTimerTickDuration( "GreenHellParasiteCheck" ) > 0 ) {
				return false;
			}
			Timers.SetTimer( "GreenHellParasiteCheck", 60, false, () => false );

			var config = GreenHellConfig.Instance;
			float chance = config.Get<float>( nameof( config.ParasiteChancePerSecond ) );

			if( chance <= Main.rand.NextFloat() ) {
				return false;
			}

			int parasiteDuration = config.Get<int>( nameof( config.ParasiteTickDuration ) );
			player.AddBuff( ModContent.BuffType<ParasitesDeBuff>(), parasiteDuration );

			return true;
		}


		public static void UpdateLifeEffectsIfParasited( Player player ) {
			if( player.lifeRegen > 0 ) {
				if( player.HasBuff( ModContent.BuffType<ParasitesDeBuff>() ) ) {
DebugHelpers.Print( "parasite", "lifeRegen: " + player.lifeRegen + " (" + ( player.lifeRegen / 2 ) + ")" );
					player.lifeRegen /= 2;
					//this.player.lifeRegenTime /= 2;
				}
			}
		}
	}
}
