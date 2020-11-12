using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Libraries.Services.FX;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		// Credit: @Oli
		public static void PremultiplyTexture( Texture2D texture ) {
			Color[] buffer = new Color[texture.Width * texture.Height];
			texture.GetData( buffer );

			for( int i = 0; i < buffer.Length; i++ ) {
				buffer[i] = Color.FromNonPremultiplied( buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A );
			}

			texture.SetData( buffer );
		}



		////////////////

		private Texture2D AnkhGlowTex;
		private Texture2D AnkhUnglowTex;



		////////////////

		private void InitializeUI() {
			this.AnkhGlowTex = this.GetTexture( "UI/AnkhGlow" );
			this.AnkhUnglowTex = this.GetTexture( "UI/AnkhUnglow" );
			NecrotisMod.PremultiplyTexture( this.AnkhGlowTex );
			NecrotisMod.PremultiplyTexture( this.AnkhUnglowTex );
		}


		////////////////

		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			if( Main.playerInventory ) {
				return;
			}

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

			bool DrawAnkh() {
				var myplayer = Main.LocalPlayer.GetModPlayer<NecrotisPlayer>();

				this.DrawHUDAnkh( myplayer.AnimaPercent, myplayer.CurrentAnimaPercentChangeRate );
				return true;
			};

			bool drawParticles() {
				CustomParticle.DrawParticles( Main.spriteBatch, false );
				return true;
			};

			/*bool drawAnkhHoverTip() {
				this.DrawAnkhHoverTooltipLayer();
				return true;
			};*/

			//

			var ankhLayer = new LegacyGameInterfaceLayer(
				"Necrotis: Ankh Status Display",
				DrawAnkh,
				InterfaceScaleType.UI
			);
			var particleLayer = new LegacyGameInterfaceLayer(
				"Necrotis: UI Particles",
				drawParticles,
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
		}
	}
}