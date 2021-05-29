using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Services.Network.SimplePacket;
using GreenHell.Buffs;


namespace GreenHell.NetProtocols {
	[Serializable]
	class PlayerStatePayload : SimplePacketPayload {
		public static void SendToServer() {
			Player plr = Main.LocalPlayer;
			var myplayer = plr.GetModPlayer<GreenHellPlayer>();
			int buffIdx = plr.FindBuffIndex( ModContent.BuffType<InfectionDeBuff>() );
			int buffTime = buffIdx >= 0 ? plr.buffTime[ buffIdx ] : 0;

			var packet = new PlayerStatePayload( myplayer.InfectionStage, buffTime );
			packet.PlayerWho = Main.myPlayer;

			SimplePacket.SendToServer( packet );
		}

		public static void SendToClients( int toWho, int fromWho ) {
			Player plr = Main.player[ fromWho ];
			var myplayer = plr.GetModPlayer<GreenHellPlayer>();
			int buffIdx = plr.FindBuffIndex( ModContent.BuffType<InfectionDeBuff>() );
			int buffTime = buffIdx >= 0 ? plr.buffTime[buffIdx] : 0;

			var protocol = new PlayerStatePayload( myplayer.InfectionStage, buffTime );
			protocol.PlayerWho = fromWho;

			SimplePacket.SendToClient( protocol, toWho, fromWho );
		}



		////////////////

		public int PlayerWho;
		public int InfectionState;
		public int InfectionDuration;



		////////////////

		private PlayerStatePayload() { }

		private PlayerStatePayload( int infectionState, int infectionDuration ) {
			this.InfectionState = infectionState;
			this.InfectionDuration = infectionDuration;
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
			Player player = Main.player[this.PlayerWho];
			var myplayer = player.GetModPlayer<GreenHellPlayer>();

			myplayer.InfectionStage = this.InfectionState;

			if( this.InfectionState > 0 ) {
				int infBuffType = ModContent.BuffType<InfectionDeBuff>();

				player.AddBuff( infBuffType, this.InfectionDuration );
			}
		}
	}
}
