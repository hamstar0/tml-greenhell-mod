using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace GreenHell.Buffs {
	class InfectionDeBuff : ModBuff {
		public const int Stages = 4;

		public static Texture2D[] Textures { get; private set; } = new Texture2D[ InfectionDeBuff.Stages ];



		////////////////

		internal static void LoadTextures() {
			for( int i = 0; i < InfectionDeBuff.Stages; i++ ) {
				InfectionDeBuff.Textures[i] = GreenHellMod.Instance.GetTexture( "Buffs/InfectionDeBuff_" + (i + 1) );
			}
		}

		internal static void UpdateIcon( GreenHellPlayer myplayer ) {
			int buffType = ModContent.BuffType<InfectionDeBuff>();
			Main.buffTexture[buffType] = InfectionDeBuff.Textures[ myplayer.InfectionStage - 1 ];
		}


		////////////////

		public static void GiveTo( GreenHellPlayer myplayer ) {
			var config = GreenHellConfig.Instance;
			int duration = config.Get<int>( nameof(config.InfectionTickDuration) );

			myplayer.player.AddBuff( ModContent.BuffType<InfectionDeBuff>(), duration );

			if( myplayer.InfectionStage < InfectionDeBuff.Stages ) {
				myplayer.InfectionStage++;
			}

			InfectionDeBuff.UpdateIcon( myplayer );
		}



		////

		public override void SetDefaults() {
			this.DisplayName.SetDefault( "Infection" );
			this.Description.SetDefault( "Your injuries are becoming infected" );
			Main.debuff[this.Type] = true;
			this.longerExpertDebuff = true;
		}

		public override void Update( Player player, ref int buffIndex ) {
		}
	}
}
