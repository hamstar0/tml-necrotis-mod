using System;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Buffs;


namespace Necrotis {
	partial class NecrotisPlayer : ModPlayer {
		public float NecrotisResistPercent { get; internal set; } = 0f;


		////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey( "necrotis_resist_percent" ) ) {
				return;
			}

			this.NecrotisResistPercent = tag.GetFloat( "necrotis_resist_percent" );
		}

		public override TagCompound Save() {
			return new TagCompound {
				{ "necrotis_resist_percent", this.NecrotisResistPercent },
			};
		}


		////////////////

		public override void PreUpdate() {
			this.UpdateNecrotisForCurrentContext();

			if( this.NecrotisResistPercent < 0f ) {
				this.player.AddBuff( BuffType<NecrotisDeBuff>(), 2 );
			}
//DebugHelpers.Print( "necrotis", "necrotis%: "+this.NecrotisResistPercent.ToString("N2") );
		}
	}
}
