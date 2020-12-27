using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Necrotis {
	public partial class NecrotisConfig : ModConfig {
		public bool DebugModeInfo { get; set; } = false;

		////

		[Range( -4096, 4096 )]
		[DefaultValue( -336 )]
		public int AnkhScreenPositionX { get; set; } = -336;

		[Range( -1024, 1024 )]
		[DefaultValue( 30 )]
		public int AnkhScreenPositionY { get; set; } = 30;

		[Range( -4096, 4096 )]
		[DefaultValue( -448 )]
		public int AnkhInvScreenPositionX { get; set; } = -488;

		[Range( -1024, 1024 )]
		[DefaultValue( 30 )]
		public int AnkhInvScreenPositionY { get; set; } = 30;


		////

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 5f )]
		public float DillutedEctoplasmPotDropChance { get; set; } = 1f / 5f;

		[ReloadRequired]
		public bool DillutedEctoplasmRecipeEnabled { get; set; } = false;

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 3f )]
		public float DillutedEctoplasmAnimaPercentHeal { get; set; } = 1f / 3f;


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
	}
}
