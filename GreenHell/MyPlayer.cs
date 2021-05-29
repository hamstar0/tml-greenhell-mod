using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.Debug;
using GreenHell.Logic;
using GreenHell.NetProtocols;


namespace GreenHell {
	class GreenHellPlayer : ModPlayer {
		public int InfectionStage { get; internal set; } = 0;

		public bool HasVerdantBlessing { get; internal set; } = false;


		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Initialize() {
			this.InfectionStage = 0;
		}

		public override void Load( TagCompound tag ) {
			this.InfectionStage = 0;

			if( tag.ContainsKey("infection_stage") ) {
				this.InfectionStage = tag.GetInt( "infection_stage" );
			}
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "infection_stage", this.InfectionStage }
			};
		}


		////////////////

		public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				if( this.player.whoAmI == Main.myPlayer ) {
					PlayerStatePayload.SendToServer();
				}
			} else {
				if( fromWho != -1 ) {
					PlayerStatePayload.SendToClients( toWho, fromWho );
				}
			}
		}

		public override void SendClientChanges( ModPlayer clientPlayer ) {
			if( Main.netMode != NetmodeID.MultiplayerClient || Main.myPlayer != this.player.whoAmI ) {
				return;
			}

			var myclone = (GreenHellPlayer)clientPlayer;

			if( myclone.InfectionStage != this.InfectionStage ) {
				PlayerStatePayload.SendToServer();
			}
		}


		////////////////

		public override void Hurt( bool pvp, bool quiet, double damage, int hitDirection, bool crit ) {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				GreenHellPlayerLogic.ApplyInfectionIf(
					myplayer: this,
					damage: damage,//crit ? damage * 2d : damage,
					sync: Main.netMode == NetmodeID.Server
				);
			}
		}


		////////////////

		public override void ResetEffects() {
			this.HasVerdantBlessing = false;
		}


		////////////////

		public override void PreUpdate() {
			GreenHellPlayerLogic.UpdateInfectionStateIf( this );
			GreenHellPlayerLogic.UpdateBrambleStateIf( this );
			GreenHellPlayerLogic.UpdateParasiteStateIf( this.player );
		}


		public override void UpdateLifeRegen() {
			GreenHellPlayerLogic.UpdateLifeEffectsIfInfection( this.player );
			GreenHellPlayerLogic.UpdateLifeEffectsIfParasites( this.player );
		}
	}
}
