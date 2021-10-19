using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using GreenHell.NetProtocols;


namespace GreenHell.Buffs {
	public partial class InfectionDeBuff : ModBuff {
		internal static void GiveTo( GreenHellPlayer myplayer, bool syncIfServer ) {
			var config = GreenHellConfig.Instance;
			int duration = config.Get<int>( nameof(config.InfectionTickDuration) );

			myplayer.player.AddBuff( ModContent.BuffType<InfectionDeBuff>(), duration, !syncIfServer );

			if( myplayer.InfectionStage < InfectionDeBuff.Stages ) {
				myplayer.InfectionStage++;
			}

			if( syncIfServer && Main.netMode == NetmodeID.Server ) {
				PlayerStatePayload.SendToClients( myplayer.player.whoAmI );
			}

			InfectionDeBuff.UpdateIcon( myplayer );
		}
	}
}
