using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace GreenHell.NPCs {
	internal partial class JungleSnakeNPC : ModNPC {
		private int AlertedElapsed = 0;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Jungle Snake" );
			Main.npcFrameCount[ this.npc.type ] = 4;
			//Main.npcCatchable[ this.npc.type ] = true;
		}

		public override void SetDefaults() {
			this.npc.width = 20;
			this.npc.height = 14;
			this.npc.damage = 1;
			this.npc.defense = 0;
			this.npc.lifeMax = 15;
			this.npc.HitSound = SoundID.NPCHit1;
			this.npc.DeathSound = SoundID.NPCDeath1;
			this.npc.npcSlots = 0.5f;
			this.npc.aiStyle = 66;
			//this.aiType = NPCID.TruffleWorm;
			this.animationType = NPCID.TruffleWorm;
		}


		////////////////

		/*public override void HitEffect( int hitDirection, double damage ) {
			if( this.npc.life > 0 ) {
				return;
			}

			for( int i = 0; i < 6; i++ ) {
				int dust = Dust.NewDust( this.npc.position, this.npc.width, this.npc.height, 200, 2 * hitDirection, -2f );
				if( Main.rand.NextBool(2) ) {
					Main.dust[dust].noGravity = true;
					Main.dust[dust].scale = 1.2f * this.npc.scale;
				} else {
					Main.dust[dust].scale = 0.7f * this.npc.scale;
				}
			}

			Gore.NewGore( this.npc.position, this.npc.velocity, this.mod.GetGoreSlot("Gores/LavaSnailHead"), this.npc.scale );
			Gore.NewGore( this.npc.position, this.npc.velocity, this.mod.GetGoreSlot("Gores/LavaSnailShell"), this.npc.scale );
		}*/


		////////////////

		public override void OnHitPlayer( Player target, int damage, bool crit ) {
			target.AddBuff( BuffID.Venom, 60 * 5 );
		}


		////////////////

		public override bool PreAI() {
			Player target = Main.player[this.npc.target];
			if( !target.active ) {
				return base.PreAI();
			}

			this.RunSnakeAI( target );

			return base.PreAI();
		}
	}
}
