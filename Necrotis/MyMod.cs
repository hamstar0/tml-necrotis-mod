using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necrotis.NecrotisBehaviors;
using Necrotis.HUD;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-necrotis-mod";



		////////////////

		public static NecrotisMod Instance { get; private set; }



		////////////////

		private AnkhHUD AnkhHUD;


		////

		internal Mod SoulBarriersMod = null;



		////////////////

		public override void Load() {
			NecrotisMod.Instance = this;
			this.SoulBarriersMod = ModLoader.GetMod( "SoulBarriers" );

			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.InitializeUI();
			}

			if( NecrotisMod.Instance.SoulBarriersMod != null ) {
				NecrotisBehavior.ApplyAnimaDefenses_PBG_WeakRef();
			}
		}

		public override void Unload() {
			NecrotisMod.Instance = null;
		}


		public override void PostSetupContent() {
			this.LoadPotLuckMod();
			this.LoadCursedBramblesMod();
		}


		////////////////

		public override void ModifyLightingBrightness( ref float scale ) {
			var myplayer = Main.LocalPlayer.GetModPlayer<NecrotisPlayer>();
			
			NecrotisBehavior.UpdateViewVisibilityScale( ref scale, myplayer.NecrotisPercent );
		}
	}
}