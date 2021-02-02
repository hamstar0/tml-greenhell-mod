using System;
using Terraria;
using Terraria.ModLoader;


namespace GreenHell.Buffs {
	public partial class ParasitesDeBuff : ModBuff {
		public static void GiveTo( Player player, bool sync ) {
			var config = GreenHellConfig.Instance;
			int buffType = ModContent.BuffType<ParasitesDeBuff>();
			int duration = config.Get<int>( nameof( config.ParasiteTickDuration ) );

			player.AddBuff( buffType, duration, !sync );
		}
	}
}
