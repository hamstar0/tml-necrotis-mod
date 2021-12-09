using System;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using Necrotis.Libraries.Services.FX;
using Necrotis.HUD;
using HUDElementsLib;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		private void InitializeUI() {
			this.AnkhHUD = AnkhHUD.CreateDefault();

			HUDElementsLibAPI.AddWidget( this.AnkhHUD );
		}


		////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			//int barsIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Resource Bars" ) );
			//if( barsIdx == -1 ) {
			//	return;
			//}

			int npcChatIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: NPC / Sign Dialog" ) );
			if( npcChatIdx == -1 ) {
				return;
			}

			//int topIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Mouse Over" ) );
			//if( topIdx == -1 ) {
			//	return;
			//}

			//

			var particleLayer = new LegacyGameInterfaceLayer(
				"Necrotis: UI Particles",
				() => {
					CustomParticle.DrawParticles( Main.spriteBatch, false );
					return true;
				},
				InterfaceScaleType.UI
			);

			/*var hoverLayer = new LegacyGameInterfaceLayer(
				"Necrotis: Ankh Hover Tooltip",
				drawAnkhHoverTip,
				InterfaceScaleType.UI
			);*/

			GameInterfaceLayer npcButtonWDLayer = this.GetInterfaceLayer_NpcChatButton_WitchDoctor_HealNecrotis();

			//

			layers.Add( particleLayer );    //barsIdx + 1

			//layers.Insert( topIdx + 1, hoverLayer );

			layers.Insert( npcChatIdx + 1, npcButtonWDLayer );
		}
	}
}