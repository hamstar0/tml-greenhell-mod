using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace GreenHell.NPCs {
	internal partial class JungleSnakeNPC : ModNPC {
		private void RunSnakeAI( Player target ) {
			float distSqr = (target.Center - npc.Center).LengthSquared();

			if( npc.velocity.Y == 0 ) {
				this.RunGroundedSnakeAI( target, distSqr );
			}

			if( this.npc.getRect().Intersects(target.getRect()) ) {
				if( this.npc.velocity.X > 0 ) {
					this.npc.velocity.X *= 0.9f;
				}
			}
		}


		private void RunGroundedSnakeAI( Player target, float distSqr ) {
			var config = GreenHellConfig.Instance;
			int farAlertRange = config.Get<int>( nameof( config.SnakeAlertRangeFar ) );
			int nearAlertRange = config.Get<int>( nameof( config.SnakeAlertRangeNear ) );

			if( distSqr < (farAlertRange * farAlertRange) ) {
				// Hiss
				if( this.AlertedElapsed == 0 ) {
					Main.PlaySound( SoundID.Item13, this.npc.Center );
				}

				this.AlertedElapsed += 2;

				// Halt travel
				this.npc.velocity.X *= 0.5f;
				this.npc.ai[0] = 0f;
				this.npc.ai[1] = 0f;
				this.npc.ai[2] = 0f;
				this.npc.ai[3] = 0f;

				// Too close?
				if( distSqr < (nearAlertRange * nearAlertRange) ) {
					this.AlertedElapsed += 4;
				}

				// Strike!
				if( this.AlertedElapsed > 480 ) {  // 4 * 60 * 2
					this.AlertedElapsed = 0;
					this.Pounce( target );
				}
			} else if( this.AlertedElapsed > 0 ) {	// Chill out
				this.AlertedElapsed--;
			}
		}


		////////////////

		public bool Pounce( Player target ) {
			var myplayer = target.GetModPlayer<GreenHellPlayer>();
			if( myplayer.HasVerdantBlessing ) {
				return false;
			}

			var pounceDir = target.Center - this.npc.Center;
			pounceDir.Normalize();
			pounceDir *= 7f;
			pounceDir.Y -= 1f;

			this.npc.velocity += pounceDir;

			return true;
		}
	}
}
