using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreenHell.NetProtocols;


namespace GreenHell.Buffs {
	partial class InfectionDeBuff : ModBuff {
		public const int Stages = 4;
		
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
