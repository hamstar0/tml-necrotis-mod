using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Necrotis {
	public partial class NecrotisConfig : ModConfig {
		public static float Convert10MinToTick( float val ) {
			return val / (60f * 60f * 10f);
		}



		////////////////

		[Range( -10f, 10f )]
		[DefaultValue( -1f )]   // was 2f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SkyAnimaPercentLossPer10Min { get; set; } = -1f;

		[Range( -10f, 10f )]
		[DefaultValue( -1.25f )]    // was 1.2.5f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BeachAnimaPercentLossPer10Min { get; set; } = -1.25f;

		[Range( -10f, 10f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PlainCavesAnimaPercentLossPer10Min { get; set; } = 1f;

		[Range( -10f, 10f )]
		[DefaultValue( -3f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HellAnimaPercentLossPer10Min { get; set; } = -3f;

		//

		[Range( -10f, 10f )]
		[DefaultValue( 2.5f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DungeonAnimaPercentLossPer10Min { get; set; } = 2.5f;

		[Range( -10f, 10f )]
		[DefaultValue( 1.25f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float CorruptionAnimaPercentLossPer10Min { get; set; } = 1.25f;

		[Range( -10f, 10f )]
		[DefaultValue( -1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HallowAnimaPercentLossPer10Min { get; set; } = -1f;

		[Range( -10f, 10f )]
		[DefaultValue( -0.25f )]	// was 0.5f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DesertAnimaPercentLossPer10Min { get; set; } = -0.25f;

		[Range( -10f, 10f )]
		[DefaultValue( -0.25f )]    // was 0.5f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SnowAnimaPercentLossPer10Min { get; set; } = -0.25f;

		[Range( -10f, 10f )]
		[DefaultValue( -0.25f )]    // was 0.5f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float JungleAnimaPercentLossPer10Min { get; set; } = -0.25f;

		[Range( -10f, 10f )]
		[DefaultValue( -0.25f )]    // was 0.5f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float ForestAnimaPercentLossPer10Min { get; set; } = -0.25f;

		//

		[Range( -10f, 10f )]
		[DefaultValue( -0.25f )]    // was 0.5f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float TownAnimaPercentLossPerTick { get; set; } = -0.25f;

		[Range( -10f, 10f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float NightOrEclipseAnimaPercentLossPerTick { get; set; } = 1f;
	}
}
