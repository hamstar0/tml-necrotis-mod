using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Necrotis {
	public partial class NecrotisConfig : ModConfig {
		[Range( -0.1f, 0.1f )]
		[DefaultValue( -4f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SkyAnimaPercentLossPerTick { get; set; } = -4f / (60f * 60f * 10f);  // -4 per 10m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( -5f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BeachAnimaPercentLossPerTick { get; set; } = -5f / (60f * 60f * 10f);  // -5 per 10m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 4f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PlainCavesAnimaPercentLossPerTick { get; set; } = 4f / (60f * 60f * 10f);  // 4 per 10m
		
		[Range( -0.1f, 0.1f )]
		[DefaultValue( -4f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HellAnimaPercentLossPerTick { get; set; } = -4f / (60f * 60f * 10f);  // -4 per 10m

		//

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DungeonAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 10f);  // 1 per 10m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 4f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float CorruptionAnimaPercentLossPerTick { get; set; } = 4f / (60f * 60f * 10f);  // 4 per 10m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( -1f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HallowAnimaPercentLossPerTick { get; set; } = -1f / (60f * 60f * 10f);  // -1 per 10m

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
		[DefaultValue( -1f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float ForestAnimaPercentLossPerTick { get; set; } = -1f / (60f * 60f * 10f);  // -1 per 10m

		//

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float TownAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 10f);  // 1 per 10m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 6f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float NightOrEclipseAnimaPercentLossPerTick { get; set; } = 6f / (60f * 60f * 10f);  // 6 per 10m
	}
}
