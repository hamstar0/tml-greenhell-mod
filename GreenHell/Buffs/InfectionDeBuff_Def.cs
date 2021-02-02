using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace GreenHell.Buffs {
	public partial class InfectionDeBuff : ModBuff {
		public const int Stages = 4;
		
		////

		public const string BaseDescription = "Your injuries are becoming infected"
			+"\nMovements now cause damage"
			+"\nInfection stage worsens damage";

		public static Texture2D[] Textures { get; private set; } = new Texture2D[ InfectionDeBuff.Stages ];



		////////////////

		internal static void LoadTextures() {
			for( int i = 0; i < InfectionDeBuff.Stages; i++ ) {
				InfectionDeBuff.Textures[i] = GreenHellMod.Instance.GetTexture( "Buffs/InfectionDeBuff_" + (i + 1) );
			}
		}

		internal static void UpdateIcon( GreenHellPlayer myplayer ) {
			int buffType = ModContent.BuffType<InfectionDeBuff>();
			Main.buffTexture[ buffType ] = InfectionDeBuff.Textures[ myplayer.InfectionStage - 1 ];
		}



		////////////////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Infection" );
			this.Description.SetDefault( InfectionDeBuff.BaseDescription );
			Main.debuff[this.Type] = true;
			Main.buffNoSave[this.Type] = false;
			this.longerExpertDebuff = true;
		}
	}
}
