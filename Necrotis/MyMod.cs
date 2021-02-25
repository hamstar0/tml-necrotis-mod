using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necrotis.NecrotisBehaviors;
using Necrotis.UI;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-necrotis-mod";


		////////////////

		public static NecrotisMod Instance { get; private set; }



		////////////////

		private AnkhHUD AnkhHUD;



		////////////////

		public NecrotisMod() {
			NecrotisMod.Instance = this;
		}

		////////////////

		public override void Load() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.AnkhHUD = new AnkhHUD();
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