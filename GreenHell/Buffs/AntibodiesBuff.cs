using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace GreenHell.Buffs {
	class AntibodiesBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Antibodies" );
			this.Description.SetDefault( "You are protected from poisons and venom" );
			Main.debuff[this.Type] = false;
		}

		public override void Update( Player player, ref int buffIndex ) {
			if( player.HasBuff(BuffID.Poisoned) ) {
				player.ClearBuff( BuffID.Poisoned );
			}

			if( player.HasBuff(BuffID.Venom) ) {
				player.ClearBuff( this.Type );	// Venom trades
				player.ClearBuff( BuffID.Venom );
			}
		}
	}
}
