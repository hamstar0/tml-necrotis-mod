using System;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.UI.ModConfig;


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
	}
}
