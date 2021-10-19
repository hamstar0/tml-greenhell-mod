using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Services.Network.SimplePacket;
using GreenHell.Buffs;


namespace GreenHell.NetProtocols {
	[Serializable]
	class PlayerStatePayload : SimplePacketPayload {
		public static void SendToServer() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				throw new ModLibsException( "Not client" );
			}

			Player plr = Main.LocalPlayer;
			var myplayer = plr.GetModPlayer<GreenHellPlayer>();
			int buffIdx = plr.FindBuffIndex( ModContent.BuffType<InfectionDeBuff>() );
			int buffTime = buffIdx >= 0 ? plr.buffTime[ buffIdx ] : 0;

			var packet = new PlayerStatePayload( Main.myPlayer, myplayer.InfectionStage, buffTime );

			SimplePacket.SendToServer( packet );
		}

		public static void SendToClients( int playerWho, int toWho = -1 ) {
			if( Main.netMode != NetmodeID.Server ) {
				throw new ModLibsException( "Not server" );
			}

			Player plr = Main.player[ playerWho ];
			var myplayer = plr.GetModPlayer<GreenHellPlayer>();
			int buffIdx = plr.FindBuffIndex( ModContent.BuffType<InfectionDeBuff>() );
			int buffTime = buffIdx >= 0 ? plr.buffTime[buffIdx] : 0;

			var protocol = new PlayerStatePayload( playerWho, myplayer.InfectionStage, buffTime );

			SimplePacket.SendToClient( protocol, toWho, playerWho );
		}



		////////////////

		public int PlayerWho;
		public int InfectionState;
		public int InfectionDuration;



		////////////////

		private PlayerStatePayload() { }

		private PlayerStatePayload( int playerWho, int infectionState, int infectionDuration ) {
			this.PlayerWho = playerWho;
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
