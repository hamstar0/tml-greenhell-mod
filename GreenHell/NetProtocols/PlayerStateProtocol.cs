using System;
using Terraria;
using HamstarHelpers.Services.Network.NetIO;
using HamstarHelpers.Services.Network.NetIO.PayloadTypes;


namespace GreenHell.NetProtocols {
	[Serializable]
	class PlayerStateProtocol : NetIOBidirectionalPayload {
		public static void SendToServer() {
			var myplayer = Main.LocalPlayer.GetModPlayer<GreenHellPlayer>();
			var protocol = new PlayerStateProtocol( myplayer.InfectionStage );
			protocol.PlayerWho = Main.myPlayer;

			NetIO.SendToServer( protocol );
		}

		public static void SendToClients( int toWho, int fromWho ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<GreenHellPlayer>();
			var protocol = new PlayerStateProtocol( myplayer.InfectionStage );
			protocol.PlayerWho = fromWho;

			NetIO.SendToClients( protocol, toWho, fromWho );
		}



		////////////////

		public int PlayerWho;
		public int InfectionState;



		////////////////

		private PlayerStateProtocol() { }

		private PlayerStateProtocol( int infectionState ) {
			this.InfectionState = infectionState;
		}


		////////////////

		public override void ReceiveOnServer( int fromWho ) {
			this.Receive();
		}

		public override void ReceiveOnClient() {
			this.Receive();
		}


		////

		private void Receive() {
			var myplayer = Main.player[ this.PlayerWho ].GetModPlayer<GreenHellPlayer>();
			myplayer.InfectionStage = this.InfectionState;
		}
	}
}
