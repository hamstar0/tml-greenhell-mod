using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;


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


		[DefaultValue( 0.05f )]
		public float SnakeSpawnChanceFromGrassCut { get; set; } = 0.05f;
		
		[DefaultValue( 7 * 16 )]
		public int SnakeAlertRangeFar { get; set; } = 7 * 16;

		[DefaultValue( 3 * 16 )]
		public int SnakeAlertRangeNear { get; set; } = 3 * 16;


		[DefaultValue( 0.10f )]
		public float ParasiteChancePerSecond { get; set; } = 0.10f;

		[DefaultValue( 60 * 60 * 3 )]	// 3 minutes
		public int ParasiteTickDuration { get; set; } = 60 * 60 * 3;

		[DefaultValue( 0.05f )]
		public float ParasiteAfflictChancePerSecond { get; set; } = 0.05f;


		[DefaultValue( 0.10f )]
		public float InfectionChancePer10LifeLostInJungle { get; set; } = 0.10f;

		[DefaultValue( 60 * 60 * 2 )]	// 2 minutes
		public int InfectionTickDuration { get; set; } = 60 * 60 * 2;


		[DefaultValue( true )]
		public bool VerdantBlessingRecipeEnabled { get; set; } = true;

		[DefaultValue( false )]
		public bool VerdantBlessingSoldByDryad { get; set; } = false;


		[DefaultValue( true )]
		public bool AntidoteSoldByWitchDoctor { get; set; } = true;

		[DefaultValue( (int)(60f * 60f * 1.5f) )]	// 2 minutes
		public int AntidoteBuffTickDuration { get; set; } = (int)(60f * 60f * 1.5f);

		[DefaultValue( true )]
		public bool AntiveninBuffTradesForVenom { get; set; } = true;
	}
}
