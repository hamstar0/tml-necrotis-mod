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

		public NullablePercent() { }
		public NullablePercent( float perc ) {
			this.Percent = perc;
		}
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

		public NullablePercent DebuffPercentUntilLowestMovement { get; set; } = new NullablePercent( 0.01f );

		public NullablePercent LowestPercentOfMovementProducedByDebuff { get; set; } = new NullablePercent( 0.75f );

		public NullablePercent DebuffPercentUntilLowestJumping { get; set; } = new NullablePercent( 0.01f );

		public NullablePercent LowestPercentOfJumpingProducedByDebuff { get; set; } = new NullablePercent( 0.75f );


		[DefaultValue( true )]
		public bool DebuffReducesRunWalkSpeed { get; set; } = true;

		[DefaultValue( false )]
		public bool DebuffReducesJumpHeight { get; set; } = false;

		[DefaultValue( true )]
		public bool DebuffReducesMaxLifeWhenActive { get; set; } = true;


		public NullablePercent DebuffPercentBeforeBleeding { get; set; } = null;	// new NullablePercent { Percent = 0.99f };

		public NullablePercent DebuffPercentBeforePoisoned { get; set; } = null;

		public NullablePercent DebuffPercentBeforeCursedInferno { get; set; } = null;


		////

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 6f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float UndergroundAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 6f);	// 6/m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * -4f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HellAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * -4f);	// -4/m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 2f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DungeonAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 2f); // 2/m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 4f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float CorruptionAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 4f);  // 4/m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 6f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float HallowAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 6f);	// 6/m

		[Range( -1f, 1f )]
		[DefaultValue( 0f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float DesertAnimaPercentLossPerTick { get; set; } = 0f;	// 0/m

		[Range( -1f, 1f )]
		[DefaultValue( 0f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SnowAnimaPercentLossPerTick { get; set; } = 0f;	// 0/m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 8f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float JungleAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 8f);	// 8/m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * -10f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float BeachAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * -10f);	// -10/m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * -4f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float SkyAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * -4f);	// -4/m

		[Range( -1f, 1f )]
		[DefaultValue( 1f / (60f * 60f * 8f) )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float NightOrEclipseAnimaPercentLossPerTick { get; set; } = 1f / (60f * 60f * 8f);	// 8/m


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

		[Range( -1f, 1f )]
		[DefaultValue( 0.8f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float EnlivenedAnimaPercentMinimum { get; set; } = 0.9f;

		[Range( 0f, 10f )]
		[DefaultValue( 1.2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float EnlivenedMovementPercent { get; set; } = 1.2f;


		[Range( 0f, 1f )]
		[DefaultValue( 0.75f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WitchDoctorHealPercentMax { get; set; } = 0.75f;

		[Range( 0f, 1000000f )]
		[DefaultValue( 100f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float WitchDoctorHealCostPerPercent { get; set; } = 100f;
	}
}
