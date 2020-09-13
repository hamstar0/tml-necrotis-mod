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



		////////////////

		private void InitializeUI() {
			this.AnkhGlowTex = this.GetTexture( "UI/AnkhGlow" );
			NecrotisMod.PremultiplyTexture( this.AnkhGlowTex );
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

			int topIdx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Mouse Over" ) );
			if( topIdx == -1 ) {
				return;
			}

			//

			GameInterfaceDrawMethod drawAnkh = () => {
				this.DrawAnkhLayer();
				return true;
			};

			GameInterfaceDrawMethod drawParticles = () => {
				CustomParticle.DrawParticles( Main.spriteBatch, false );
				return true;
			};

			GameInterfaceDrawMethod drawAnkhHoverTip = () => {
				this.DrawAnkhHoverTooltipLayer();
				return true;
			};

			//

			var ankhLayer = new LegacyGameInterfaceLayer( "Necrotis: Ankh Status Display", drawAnkh, InterfaceScaleType.UI );
			var particleLayer = new LegacyGameInterfaceLayer( "Necrotis: UI Particles", drawParticles, InterfaceScaleType.UI );
			var hoverLayer = new LegacyGameInterfaceLayer( "Necrotis: Ankh Hover Tooltip", drawParticles, InterfaceScaleType.UI );

			//

			//layers.RemoveAt( idx );
			layers.Insert( barsIdx + 1, particleLayer );
			layers.Insert( barsIdx + 1, ankhLayer );

			layers.Insert( topIdx + 1, hoverLayer );
		}


		////

		private void DrawAnkhLayer() {
			var config = NecrotisConfig.Instance;
			Texture2D bgTex = this.GetTexture( "UI/AnkhBG" );
			Texture2D fgTex = this.GetTexture( "UI/AnkhFG" );

			var pos = new Vector2(
				config.Get<int>( nameof(NecrotisConfig.AnkhScreenPositionX) ),
				config.Get<int>( nameof(NecrotisConfig.AnkhScreenPositionY) )
			);
			if( pos.X < 0 ) {
				pos.X = Main.screenWidth + pos.X;
			}
			if( pos.Y < 0 ) {
				pos.Y = Main.screenHeight + pos.Y;
			}

			var myplayer = Main.LocalPlayer.GetModPlayer<NecrotisPlayer>();
			int necScroll = (int)( (float)myplayer.NecrotisResistPercent * (float)fgTex.Height );
			var statSrcRect = new Rectangle(
				x: 0,
				y: fgTex.Height - necScroll,
				width: fgTex.Width,
				height: necScroll
			);

			this.DrawUIAnkhChangeFX( pos, statSrcRect, myplayer.CurrentNecrotisResistPercentChangeRate );
			this.DrawUIAnkh( bgTex, fgTex, pos, statSrcRect, myplayer.NecrotisResistPercent );
		}

		private void DrawAnkhHoverTooltipLayer() {
			var config = NecrotisConfig.Instance;
			Texture2D bgTex = this.GetTexture( "UI/AnkhBG" );

			var pos = new Vector2(
				config.Get<int>( nameof(NecrotisConfig.AnkhScreenPositionX) ),
				config.Get<int>( nameof(NecrotisConfig.AnkhScreenPositionY) )
			);

			var area = new Rectangle( (int)pos.X, (int)pos.Y, bgTex.Width, bgTex.Height );
			if( !area.Contains( Main.mouseX, Main.mouseY ) ) {
				return;
			}

			var myplayer = Main.LocalPlayer.GetModPlayer<NecrotisPlayer>();
			float necrotisResistPercent = myplayer.CurrentNecrotisResistPercentChangeRate;

			this.DrawAnkhHoverTooltip( necrotisResistPercent );
		}
	}
}