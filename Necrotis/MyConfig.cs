using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using static Terraria.ModLoader.ModContent;
using HamstarHelpers.Classes.UI.ModConfig;
using HamstarHelpers.Services.Configs;


namespace Necrotis {
	class MyFloatInputElement : FloatInputElement { }




	public class NullablePercent {
		[Range( 0f, 1f )]
		public float Percent;
	}




	public partial class NecrotisConfig : StackableModConfig {
		public static NecrotisConfig Instance => ModConfigStack.GetMergedConfigs<NecrotisConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[Range(0f, 1f)]
		[DefaultValue( 1f / 60f )]
		public float ResistPercentDropPerTick { get; set; } = 1f / 60f;


		[Range( 60, 60 * 60 * 60 )]
		[DefaultValue( 60 * 60 * 2 )]
		public int NecrotisMaxAfflictionTickDuration { get; set; } = 60 * 60 * 2;


		[Range( 0f, 1f )]
		[DefaultValue( 0.35f )]
		public float DebuffPercentUntilLowestMovement { get; set; } = 0.35f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.35f )]
		public float LowestPercentOfMovementProducedByDebuff { get; set; } = 0.35f;


		[DefaultValue( true )]
		public bool DebuffReducesRunWalkSpeed { get; set; } = true;

		[DefaultValue( false )]
		public bool DebuffReducesJumpHeight { get; set; } = false;


		public NullablePercent DebuffPercentBeforeBleeding { get; set; } = new NullablePercent {
			Percent = 0.99f
		};

		public NullablePercent DebuffPercentBeforePoisoned { get; set; } = null;

		public NullablePercent DebuffPercentBeforeCursedInferno { get; set; } = null;


		////

		[Range( -360f, 360f )]
		[DefaultValue( -0.05f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float UnderworldAfflicationIncreasePerTick { get; set; } = -0.05f / 60f;

		[Range( -360f, 360f )]
		[DefaultValue( 0.60f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DungeonAfflicationIncreasePerTick { get; set; } = 0.60f / 60f;

		[Range( -360f, 360f )]
		[DefaultValue( 0.20f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float CorruptionAfflicationIncreasePerTick { get; set; } = 0.20f / 60f;

		[Range( -360f, 360f )]
		[DefaultValue( 0.01f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HallowAfflicationIncreasePerTick { get; set; } = 0.01f / 60f;

		[Range( -360f, 360f )]
		[DefaultValue( 0.05f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DesertAfflicationIncreasePerTick { get; set; } = 0.05f / 60f;

		[Range( -360f, 360f )]
		[DefaultValue( 0.05f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SnowAfflicationIncreasePerTick { get; set; } = 0.05f / 60f;

		[Range( -360f, 360f )]
		[DefaultValue( 0.025f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float JungleAfflicationIncreasePerTick { get; set; } = 0.025f / 60f;

		[Range( -360f, 360f )]
		[DefaultValue( -0.01f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BeachAfflicationIncreasePerTick { get; set; } = -0.01f / 60f;

		[Range( -360f, 360f )]
		[DefaultValue( -0.01f / 60f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SkyAfflicationIncreasePerTick { get; set; } = -0.01f / 60f;


		////

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 4f )]
		public float DillutedEctoplasmPotDropChance { get; set; } = 1f / 4f;

		[ReloadRequired]
		public bool DillutedEctoplasmRecipeEnabled { get; set; } = false;

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 3f )]
		public float DillutedEctoplasmFortifyPercent { get; set; } = 1f / 3f;
	}
}
