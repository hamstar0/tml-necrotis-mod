using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.UI.ModConfig;


namespace Necrotis {
	class MyFloatInputElement : FloatInputElement { }




	public class NullablePercent {
		[Range( 0f, 1f )]
		public float Percent;
	}




	public partial class NecrotisConfig : ModConfig {
		public static NecrotisConfig Instance => ModContent.GetInstance<NecrotisConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		public bool DebugModeInfo { get; set; } = false;

		////

		[Range( -4096, 4096 )]
		[DefaultValue( -336 )]
		public int AnkhScreenPositionX { get; set; } = -336;

		[Range( -1024, 1024 )]
		[DefaultValue( 30 )]
		public int AnkhScreenPositionY { get; set; } = 30;


		////

		[Range( 0f, 1f )]
		[DefaultValue( 0.01f )]
		public float DebuffPercentUntilLowestMovement { get; set; } = 0.01f;

		[Range( 0f, 1f )]
		[DefaultValue( 0.75f )]
		public float LowestPercentOfMovementProducedByDebuff { get; set; } = 0.75f;


		[DefaultValue( true )]
		public bool DebuffReducesRunWalkSpeed { get; set; } = true;

		[DefaultValue( false )]
		public bool DebuffReducesJumpHeight { get; set; } = false;


		public NullablePercent DebuffPercentBeforeBleeding { get; set; } = null;	// new NullablePercent { Percent = 0.99f };

		public NullablePercent DebuffPercentBeforePoisoned { get; set; } = null;

		public NullablePercent DebuffPercentBeforeCursedInferno { get; set; } = null;


		////

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 3f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float UndergroundAfflicationIncreasePerTick { get; set; } = 1f / (60f * 60f * 3f);	// 3m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * -2f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HellAfflicationIncreasePerTick { get; set; } = 1f / (60f * 60f * -2f);	// -2m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 1f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DungeonAfflicationIncreasePerTick { get; set; } = 1f / (60f * 60f * 1f); // 1m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 2f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float CorruptionAfflicationIncreasePerTick { get; set; } = 1f / (60f * 60f * 2f);  // 2m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 4f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HallowAfflicationIncreasePerTick { get; set; } = 1f / (60f * 60f * 4f);	// 4m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 8f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DesertAfflicationIncreasePerTick { get; set; } = 1f / (60f * 60f * 8f);	// 8m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 3f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SnowAfflicationIncreasePerTick { get; set; } = 1f / (60f * 60f * 3f);	// 3m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 4f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float JungleAfflicationIncreasePerTick { get; set; } = 1f / (60f * 60f * 4f);	// 4m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * -5f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BeachAfflicationIncreasePerTick { get; set; } = 1f / (60f * 60f * -5f);	// -5m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * -2f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SkyAfflicationIncreasePerTick { get; set; } = 1f / (60f * 60f * -2f);	// -2m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 4f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float NightOrEclipseAfflicationIncreasePerTick { get; set; } = 1f / (60f * 60f * 4f);	// 4m


		////

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 3f )]
		public float DillutedEctoplasmPotDropChance { get; set; } = 1f / 3f;

		[ReloadRequired]
		public bool DillutedEctoplasmRecipeEnabled { get; set; } = false;

		[Range( 0f, 1f )]
		[DefaultValue( 1f / 3f )]
		public float DillutedEctoplasmFortifyPercent { get; set; } = 1f / 3f;
	}
}
