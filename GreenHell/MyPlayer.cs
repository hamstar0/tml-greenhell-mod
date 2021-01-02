using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using GreenHell.Logic;


namespace GreenHell {
	class GreenHellPlayer : ModPlayer {
		public int InfectionStage { get; internal set; } = 0;

		public bool HasVerdantBlessing { get; internal set; } = false;


		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Hurt( bool pvp, bool quiet, double damage, int hitDirection, bool crit ) {
			GreenHellPlayerLogic.ApplyInfectionIf( this, crit ? damage * 2d : damage );
		}


		////////////////

		public override void ResetEffects() {
			this.HasVerdantBlessing = false;
		}


		////////////////

		public override void PreUpdate() {
			GreenHellPlayerLogic.UpdateBrambleState( this );
			GreenHellPlayerLogic.UpdateParasiteState( this.player );
		}


		public override void UpdateLifeRegen() {
			GreenHellPlayerLogic.UpdateLifeEffectsIfParasited( this.player );
		}
	}
}
