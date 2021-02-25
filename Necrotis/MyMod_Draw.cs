using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Libraries.Services.FX;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		public override void UpdateUI( GameTime gameTime ) {
			this.AnkhHUD.Update();
		}


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			int barsIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Resource Bars" ) );
			if( barsIdx == -1 ) {
				return;
			}

			int npcChatIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: NPC / Sign Dialog" ) );
			if( npcChatIdx == -1 ) {
				return;
			}

			int topIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Mouse Over" ) );
			if( topIdx == -1 ) {
				return;
			}

			//

			var ankhLayer = new LegacyGameInterfaceLayer(
				"Necrotis: Ankh Status Display",
				() => {
					this.AnkhHUD.Draw( Main.spriteBatch );
					return true;
				},
				InterfaceScaleType.UI
			);
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

			//layers.RemoveAt( idx );
			layers.Insert( barsIdx + 1, particleLayer );
			layers.Insert( barsIdx + 1, ankhLayer );

			//layers.Insert( topIdx + 1, hoverLayer );

			layers.Insert( npcChatIdx + 1, npcButtonWDLayer );

			//

			int cursorIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Cursor" ) );
			if( cursorIdx == -1 ) { return; }

			if( this.AnkhHUD.ConsumesCursor() ) {
				layers.RemoveAt( cursorIdx );
			}
		}
	}
}