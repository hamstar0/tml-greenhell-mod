using System;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using GreenHell.NetProtocols;


namespace GreenHell {
	class GreenHellCustomPlayer : CustomPlayerData {
		private int InfectionStageSnapshot;



		////////////////

		protected override void OnEnter( bool isCurrentPlayer, object data ) {
			// Finally got sick of SyncPlayer/SendClientData

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				if( isCurrentPlayer ) {
					PlayerStatePayload.SendToServer();
				}
			} else if( Main.netMode == NetmodeID.Server ) {
				var myplayer = this.Player.GetModPlayer<GreenHellPlayer>();

				this.InfectionStageSnapshot = myplayer.InfectionStage;
			}
		}


		////////////////

		protected override void Update() {
			if( Main.netMode == NetmodeID.Server ) {
				var myplayer = this.Player.GetModPlayer<GreenHellPlayer>();

				if( this.InfectionStageSnapshot != myplayer.InfectionStage ) {
					PlayerStatePayload.SendToClients( this.PlayerWho, -1 );
				}
			}
		}
	}
}
