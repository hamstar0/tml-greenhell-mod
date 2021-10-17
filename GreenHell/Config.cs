using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.UI.ModConfig;


namespace GreenHell {
	class MyFloatInputElement : FloatInputElement { }




	[Label( "Config" )]
	public partial class GreenHellConfig : ModConfig {
		public static GreenHellConfig Instance => ModContent.GetInstance<GreenHellConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[DefaultValue( 60 * 7 )]
		public int EmbrambledDuration { get; set; } = 60 * 7;


		[DefaultValue( 0.01f )]
		public float SnakeSpawnChanceFromGrassCut { get; set; } = 0.01f;
		
		[DefaultValue( 7 * 16 )]
		public int SnakeAlertRangeFar { get; set; } = 7 * 16;

		[DefaultValue( 3 * 16 )]
		public int SnakeAlertRangeNear { get; set; } = 3 * 16;


		[DefaultValue( 0.10f )]
		public float ParasiteChancePerSecond { get; set; } = 0.10f;

		[Range( 0, 60 * 60 * 60 )]
		[DefaultValue( 60 * 60 * 3 )]	// 3 minutes
		public int ParasiteTickDuration { get; set; } = 60 * 60 * 3;

		[DefaultValue( 0.05f )]
		public float ParasiteAfflictChancePerSecond { get; set; } = 0.05f;


		[DefaultValue( 0.2f )]
		public float InfectionChancePer10LifeLostInJungle { get; set; } = 0.2f;

		[Range( 0, 60 * 60 * 60 )]
		[DefaultValue( (int)(60f * 60f * 1.5f) )]	// 1 minutes (was 2)
		public int InfectionTickDuration { get; set; } = (int)(60f * 60f * 1.5f);

		[Range( 0f, 100f )]
		[DefaultValue( 2.5f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float InfectionDamagePerVelocityScale { get; set; } = 2.5f;


		[DefaultValue( true )]
		public bool VerdantBlessingRecipeEnabled { get; set; } = true;

		[DefaultValue( false )]
		public bool VerdantBlessingSoldByDryad { get; set; } = false;

		[DefaultValue( true )]
		public bool VerdantBlessingByWitchDoctor { get; set; } = true;


		[DefaultValue( true )]
		public bool PanaceaSoldByWitchDoctor { get; set; } = true;

		[DefaultValue( true )]
		public bool PanaceaSoldByDryad { get; set; } = true;

		[Range( 0, 60 * 60 * 60 )]
		[DefaultValue( (int)(60f * 60f * 1.5f) )]	// 2 minutes
		public int PanaceaBuffTickDuration { get; set; } = (int)(60f * 60f * 1.5f);

		[DefaultValue( true )]
		public bool AntiveninBuffTradesForVenom { get; set; } = true;
	}
}
