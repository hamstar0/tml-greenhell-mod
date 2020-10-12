using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;
using GreenHell.Buffs;


namespace GreenHell {
	class GreenHellPlayer : ModPlayer {
		public int InfectionStage { get; internal set; } = 0;



		////////////////

		public override void Hurt( bool pvp, bool quiet, double damage, int hitDirection, bool crit ) {
			var config = GreenHellConfig.Instance;
			float infectionChancePerChunk = config.Get<float>( nameof( config.InfectionChancePer10LifeLostInJungle ) );

			for( ; damage > 10d; damage -= 10d ) {
				if( Main.rand.NextFloat() < infectionChancePerChunk ) {
					InfectionDeBuff.GiveTo( this );
					break;
				}
			}
		}


		////////////////

		public override void PreUpdate() {
			var config = GreenHellConfig.Instance;
			int embrambleDuration = config.Get<int>( nameof( config.EmbrambledDuration ) );

			Rectangle plrRect = this.player.getRect();
			plrRect.X += (int)this.player.velocity.X;
			plrRect.Y += (int)this.player.velocity.Y;
			int fromTileX = plrRect.X / 16;
			int fromTileY = plrRect.Y / 16;
			int toTileX = ( plrRect.X + plrRect.Width ) / 16;
			int toTileY = ( plrRect.Y + plrRect.Height ) / 16;

			for( int tileX = fromTileX; tileX <= toTileX; tileX++ ) {
				for( int tileY = fromTileY; tileY <= toTileY; tileY++ ) {
					Tile tile = Main.tile[tileX, tileY];
					if( tile?.active() != true || tile.type != TileID.JungleThorns ) {
						continue;
					}

					this.player.AddBuff( ModContent.BuffType<EmbrambledDeBuff>(), embrambleDuration );
				}
			}

			if( this.player.ZoneJungle && this.player.wet && !this.player.honeyWet && !this.player.lavaWet ) {
				if( Timers.GetTimerTickDuration("GreenHellParasiteCheck") == 0 ) {
					Timers.SetTimer( "GreenHellParasiteCheck", 60, false, () => false );

					float chance = config.Get<float>( nameof(config.ParasiteChancePerSecond) );
					int parasiteDuration = config.Get<int>( nameof(config.ParasiteTickDuration) );

					if( chance > Main.rand.NextFloat() ) {
						this.player.AddBuff( ModContent.BuffType<ParasitesDeBuff>(), parasiteDuration );
					}
				}
			}
		}


		public override void UpdateLifeRegen() {
			if( this.player.lifeRegen > 0 ) {
				if( this.player.HasBuff( ModContent.BuffType<ParasitesDeBuff>() ) ) {
DebugHelpers.Print( "parasite", "lifeRegen: "+this.player.lifeRegen+" ("+(this.player.lifeRegen/2)+")" );
					this.player.lifeRegen /= 2;
					//this.player.lifeRegenTime /= 2;
				}
			}
		}


		public override void PostUpdateEquips() {
			float extraHp = this.player.statLifeMax2 - 100;
			float infectionPercent = (float)this.InfectionStage / (float)InfectionDeBuff.Stages;

			this.player.statLifeMax2 = 100 + (int)(extraHp * (1f - infectionPercent));
		}
	}
}
