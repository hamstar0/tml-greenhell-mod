using System;
using Terraria;
using Terraria.ModLoader;


namespace GreenHell.Buffs {
	public class EmbrambledDeBuff : ModBuff {
		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Embrambled" );
			this.Description.SetDefault(
				"You are covered in sticky, stinging brambles"
				+"\nYou're poisoned and move slowly"
			);
			Main.debuff[this.Type] = true;
			Main.buffNoSave[this.Type] = false;
			this.longerExpertDebuff = true;
		}

		public override void Update( Player player, ref int buffIndex ) {
			player.poisoned = true;
			player.slow = true;
		}
	}
}
