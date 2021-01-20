using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreenHell.NetProtocols;


namespace GreenHell.Buffs {
	partial class InfectionDeBuff : ModBuff {
		public static void GiveTo( GreenHellPlayer myplayer, bool sync ) {
			var config = GreenHellConfig.Instance;
			int duration = config.Get<int>( nameof(config.InfectionTickDuration) );

			myplayer.player.AddBuff( ModContent.BuffType<InfectionDeBuff>(), duration, !sync );

			if( myplayer.InfectionStage < InfectionDeBuff.Stages ) {
				myplayer.InfectionStage++;
			}

			if( sync ) {
				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					if( myplayer.player.whoAmI == Main.myPlayer ) {
						PlayerStateProtocol.SendToServer();
					}
				} else if( Main.netMode == NetmodeID.Server ) {
					PlayerStateProtocol.SendToClients( -1, myplayer.player.whoAmI );
				}
			}

			InfectionDeBuff.UpdateIcon( myplayer );
		}
	}
}
