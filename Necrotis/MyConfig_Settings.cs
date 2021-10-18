using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Necrotis {
	public partial class NecrotisConfig : ModConfig {
		public bool DebugModeInfo { get; set; } = false;

		////

		[Range( -4096, 4096 )]
		[DefaultValue( -336 )]
		public int InitialAnkhScreenPositionX { get; set; } = -336;

		[Range( -1024, 1024 )]
		[DefaultValue( 30 )]
		public int InitialAnkhScreenPositionY { get; set; } = 30;


		////

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 6f )]
		public float DillutedEctoplasmPotDropChance { get; set; } = 1f / 6f;

		[ReloadRequired]
		public bool DillutedEctoplasmRecipeEnabled { get; set; } = false;

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 2f )]
		public float DillutedEctoplasmAnimaPercentHeal { get; set; } = 1f / 2f;

		[Range( 0, 60 * 60 )]
		[DefaultValue( 30 )]
		public int DillutedEctoplasmRespiritedDurationSeconds { get; set; } = 30;


		////

		[Range( 0f, 1f )]
		[DefaultValue( 0.75f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WitchDoctorHealPercentMax { get; set; } = 0.75f;

		[Range( 0f, 1000000f )]
		[DefaultValue( 100f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WitchDoctorHealCostPerPercent { get; set; } = 100f;


		////

		[DefaultValue( true )]
		public bool CanopicJarRecipeEnabled { get; set; } = true;


		////

		[DefaultValue( true )]
		public bool ElixirRecipeEnabled { get; set; } = true;

		[Range( 0f, 1f )]
		[DefaultValue( 0f )]
		public float ElixirAnimaDrainMultiplier { get; set; } = 0f;

		[Range( 0, 60 * 60 )]
		[DefaultValue( 60 * 3 )]
		public int ElixirDurationInSeconds { get; set; } = 60 * 3;


		////

		[Range( 0f, 1f )]
		[DefaultValue( 0.2f )]
		public float RespawnMinimumAnima { get; set; } = 0.2f;
	}
}
