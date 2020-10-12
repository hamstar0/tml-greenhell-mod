using Terraria;
using Terraria.ModLoader;
using GreenHell.Buffs;


namespace GreenHell {
	public class GreenHellMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-greenhell-mod";


		////////////////

		public static GreenHellMod Instance { get; private set; }



		////////////////

		public GreenHellMod() {
			GreenHellMod.Instance = this;
		}

		////////////////

		public override void Load() {
			if( !Main.dedServ ) {
				InfectionDeBuff.LoadTextures();
			}
		}

		////

		public override void Unload() {
			GreenHellMod.Instance = null;
		}
	}
}