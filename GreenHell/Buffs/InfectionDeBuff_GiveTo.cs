using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreenHell.NetProtocols;


namespace GreenHell.Buffs {
	public partial class InfectionDeBuff : ModBuff {
		internal static void GiveTo( GreenHellPlayer myplayer, bool sync ) {
			var config = GreenHellConfig.Instance;
			int duration = config.Get<int>( nameof(config.InfectionTickDuration) );

			myplayer.player.AddBuff( ModContent.BuffType<InfectionDeBuff>(), duration, !sync );

			if( myplayer.InfectionStage < InfectionDeBuff.Stages ) {
				myplayer.InfectionStage++;
			}

			if( sync ) {
				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					if( myplayer.player.whoAmI == Main.myPlayer ) {
						PlayerStatePayload.SendToServer();
					}
				} else if( Main.netMode == NetmodeID.Server ) {
					PlayerStatePayload.SendToClients( -1, myplayer.player.whoAmI );
				}
			}

			InfectionDeBuff.UpdateIcon( myplayer );
		}
	}
}
