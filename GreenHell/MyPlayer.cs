using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.Debug;
using GreenHell.Logic;
using GreenHell.Items;


namespace GreenHell {
	class GreenHellPlayer : ModPlayer {
		public static void GiveMessageAboutJungle_WeakRef() {
			string id = "GreenHell_Overview";

			Messages.MessagesAPI.AddMessage(
				title: "Beware the Jungle!",
				description: "Think you know what's in store? Think again! Jungles are now cesspools of nasty"
					+" biological hazards, full of parasites, diseases, and lurking venomous things. Even the"
					+" very waters aren't safe to traverse, seeing as how they carry a risk of blood sucking"
					+" parasites."
					+"\n \n"
					+"The big killer now, though, is infection. Every wound you receive has a chance of"
					+" becoming infected, and can worsen with subsequent injuries. Oh, and you may also want to"
					+" now avoid any brambles you come across, too. Be sure not to disturb the bushes and grass"
					+" too much, also."
					+"\n \n"
					+"Don't like the sound of this? Be sure to consult your resident nature specialist"
					+" townsfolks for solutions. Just don't think they'll come cheap!",
				modOfOrigin: GreenHellMod.Instance,
				alertPlayer: Messages.MessagesAPI.IsUnread(id),
				isImportant: false,
				parentMessage: Messages.MessagesAPI.GameInfoCategoryMsg,
				id: id
			);
		}



		////////////////

		public int InfectionStage { get; internal set; } = 0;

		////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Initialize() {
			this.InfectionStage = 0;
		}

		public override void Load( TagCompound tag ) {
			this.InfectionStage = 0;

			if( tag.ContainsKey("infection_stage") ) {
				this.InfectionStage = tag.GetInt( "infection_stage" );
			}
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "infection_stage", this.InfectionStage }
			};
		}


		////////////////

		public override void Hurt( bool pvp, bool quiet, double damage, int hitDirection, bool crit ) {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				GreenHellPlayerLogic.ApplyInfectionIf(
					myplayer: this,
					damage: damage,//crit ? damage * 2d : damage,
					syncIfServer: Main.netMode == NetmodeID.Server
				);
			}
		}


		////////////////
		
		public override void PreUpdate() {
			GreenHellPlayerLogic.UpdateInfectionStateIf( this );
			GreenHellPlayerLogic.UpdateBrambleStateIf( this );
			GreenHellPlayerLogic.UpdateParasiteStateIf( this.player );

			if( this.player.whoAmI == Main.myPlayer ) {
				if( this.player.ZoneJungle ) {
					if( ModLoader.GetMod("Messages") != null ) {
						GreenHellPlayer.GiveMessageAboutJungle_WeakRef();
					}
				}
			}
		}


		public override void UpdateLifeRegen() {
			GreenHellPlayerLogic.UpdateLifeEffectsIfInfection( this.player );
			GreenHellPlayerLogic.UpdateLifeEffectsIfParasites( this.player );
		}


		////////////////

		public bool HasVerdantBlessing() {
			int verdantBlessingItemType = ModContent.ItemType<VerdantBlessingItem>();
			int maxAcc = 8 + player.extraAccessorySlots;

			for( int i=3; i<maxAcc; i++ ) {
				Item acc = this.player.armor[i];
				if( acc?.active == true && acc.type == verdantBlessingItemType ) {
					return true;
				}
			}
			return false;
		}
	}
}
