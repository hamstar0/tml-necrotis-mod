using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Necrotis {
	public partial class NecrotisConfig : ModConfig {
		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / ( 60f * 60f * -4f ) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SkyAnimaPercentLossPerTick { get; set; } = 1f / ( 60f * 60f * -4f ); // -4/m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / ( 60f * 60f * -10f ) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BeachAnimaPercentLossPerTick { get; set; } = 1f / ( 60f * 60f * -10f );  // -10/m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / (60f * 60f * 4f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PlainCavesAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 4f);  // 4/m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / (60f * 60f * -4f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HellAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * -4f);    // -4/m

		//

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DungeonAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 10f); // 10/m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / (60f * 60f * 4f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float CorruptionAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 4f);  // 4/m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / (60f * 60f * -1f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HallowAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 6f);   // -1/m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 0f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DesertAnimaPercentLossPerTick { get; set; } = 0f;  // 0/m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 0f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SnowAnimaPercentLossPerTick { get; set; } = 0f;    // 0/m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 0f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float JungleAnimaPercentLossPerTick { get; set; } = 0f;  // 0/m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / (60f * 60f * -1) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float ForestAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * -1f);  // -1/m

		//

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / ( 60f * 60f * -1 ) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float TownAnimaPercentLossPerTick { get; set; } = 1f / ( 60f * 60f * -1f );  // -1/m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / (60f * 60f * 6f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float NightOrEclipseAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 6f);	// 6/m
	}
}
