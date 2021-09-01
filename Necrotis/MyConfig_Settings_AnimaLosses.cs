using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Necrotis {
	public partial class NecrotisConfig : ModConfig {
		public static float Convert10MinToTick( float val ) {
			return val / (60f * 60f * 10f);
		}



		////////////////

		[Range( -100f, 100f )]
		[DefaultValue( -1f )]   // was 2f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SkyAnimaPercentLossPer10Min { get; set; } = -1f;

		[Range( -100f, 100f )]
		[DefaultValue( -1.25f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BeachAnimaPercentLossPer10Min { get; set; } = -1.25f;

		[Range( -100f, 100f )]
		[DefaultValue( 0.75f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PlainCavesAnimaPercentLossPer10Min { get; set; } = 0.75f;

		[Range( -100f, 100f )]
		[DefaultValue( -3f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HellAnimaPercentLossPer10Min { get; set; } = -3f;

		//

		[Range( -100f, 100f )]
		[DefaultValue( 2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DungeonAnimaPercentLossPer10Min { get; set; } = 2f;

		[Range( -100f, 100f )]
		[DefaultValue( 1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float CorruptionAnimaPercentLossPer10Min { get; set; } = 1f;

		[Range( -100f, 100f )]
		[DefaultValue( -1f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HallowAnimaPercentLossPer10Min { get; set; } = -1f;

		[Range( -100f, 100f )]
		[DefaultValue( -0.25f )]	// was 0.5f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DesertAnimaPercentLossPer10Min { get; set; } = -0.25f;

		[Range( -100f, 100f )]
		[DefaultValue( -0.25f )]    // was 0.5f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SnowAnimaPercentLossPer10Min { get; set; } = -0.25f;

		[Range( -100f, 100f )]
		[DefaultValue( -0.25f )]    // was 0.5f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float JungleAnimaPercentLossPer10Min { get; set; } = -0.25f;

		[Range( -100f, 100f )]
		[DefaultValue( -0.25f )]    // was 0.5f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float ForestAnimaPercentLossPer10Min { get; set; } = -0.25f;

		//

		[Range( -100f, 100f )]
		[DefaultValue( -0.25f )]    // was 0.5f
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float TownAnimaPercentLossPer10Min { get; set; } = -0.25f;

		[Range( -100f, 100f )]
		[DefaultValue( 0.5f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float NightOrEclipseAnimaPercentLossPer10Min { get; set; } = 0.5f;
	}
}
