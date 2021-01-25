using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Necrotis {
	public partial class NecrotisConfig : ModConfig {
		public NullablePercent DebuffPercentUntilLowestMovement { get; set; } = new NullablePercent( 0.01f );

		public NullablePercent LowestPercentOfMovementProducedByDebuff { get; set; } = new NullablePercent( 0.75f );

		public NullablePercent DebuffPercentUntilLowestJumping { get; set; } = new NullablePercent( 0.01f );

		public NullablePercent LowestPercentOfJumpingProducedByDebuff { get; set; } = new NullablePercent( 0.75f );

		[Range( 0f, 1f )]
		[DefaultValue( 0.65f )]
		public float LowestPercentViewVisibilityFromDebuff { get; set; } = 0.65f;


		//

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
		[DefaultValue( 0.8f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float EnlivenedAnimaPercentMinimum { get; set; } = 0.9f;

		[Range( 0f, 10f )]
		[DefaultValue( 1.2f )]
		[CustomModConfigItem( typeof( MyFloatInputElement ) )]
		public float EnlivenedMovementPercent { get; set; } = 1.2f;
	}
}
