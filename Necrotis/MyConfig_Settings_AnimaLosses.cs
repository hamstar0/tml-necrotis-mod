using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Necrotis {
	public partial class NecrotisConfig : ModConfig {
		[Range( -0.1f, 0.1f )]
		[DefaultValue( -2f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SkyAnimaPercentLossPerTick { get; set; } = -2f / (60f * 60f * 10f);  // -2 per 10m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( -2.5f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BeachAnimaPercentLossPerTick { get; set; } = -2.5f / (60f * 60f * 10f);  // -2.5f per 10m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 2f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float PlainCavesAnimaPercentLossPerTick { get; set; } = 2f / (60f * 60f * 10f);  // 2 per 10m
		
		[Range( -0.1f, 0.1f )]
		[DefaultValue( -2f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HellAnimaPercentLossPerTick { get; set; } = -2f / (60f * 60f * 10f);  // -2 per 10m

		//

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 4f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DungeonAnimaPercentLossPerTick { get; set; } = 4f / (60f * 60f * 10f);  // 4 per 10m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 2f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float CorruptionAnimaPercentLossPerTick { get; set; } = 2f / (60f * 60f * 10f);  // 2 per 10m

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
		[DefaultValue( -0.5f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float ForestAnimaPercentLossPerTick { get; set; } = -0.5f / (60f * 60f * 10f);  // -0.5 per 10m

		//

		[Range( -0.1f, 0.1f )]
		[DefaultValue( -0.5f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float TownAnimaPercentLossPerTick { get; set; } = -0.5f / (60f * 60f * 10f);  // -0.5 per 10m

		[Range( -0.1f, 0.1f )]
		[DefaultValue( 1f / (60f * 60f * 10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float NightOrEclipseAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 10f);  // 1 per 10m
	}
}
