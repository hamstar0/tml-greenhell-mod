using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace GreenHell.Buffs {
	public partial class ParasitesDeBuff : ModBuff {
		public static void GiveTo( Player player, bool sync ) {
			var config = GreenHellConfig.Instance;
			int buffType = ModContent.BuffType<ParasitesDeBuff>();
			int duration = config.Get<int>( nameof( config.ParasiteTickDuration ) );

			player.AddBuff( buffType, duration, !sync );
			
			if( sync ) {
				if( Main.netMode == NetmodeID.Server ) {
					duration = (int)( Main.expertDebuffTime * (float)duration );

					NetMessage.SendData(
						MessageID.AddPlayerBuff,
						-1,
						-1,
						null,
						player.whoAmI,
						(float)buffType,
						(float)duration
						//0f,
						//0,
						//0,
						//0
					);
				}
			}
		}
	}
}
