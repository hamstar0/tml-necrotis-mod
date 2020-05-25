using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			if( Main.playerInventory ) {
				return;
			}

			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Resource Bars" ) );
			if( idx == -1 ) {
				return;
			}

			GameInterfaceDrawMethod draw = () => {
				Texture2D bgTex = this.GetTexture( "UI/AnkhBG" );
				Texture2D fgTex = this.GetTexture( "UI/AnkhFG" );
				var pos = new Vector2(
					NecrotisConfig.Instance.AnkhScreenPositionX,
					NecrotisConfig.Instance.AnkhScreenPositionY
				);
				if( pos.X < 0 ) {
					pos.X = Main.screenWidth + pos.X;
				}
				if( pos.Y < 0 ) {
					pos.Y = Main.screenHeight + pos.Y;
				}

				var myplayer = Main.LocalPlayer.GetModPlayer<NecrotisPlayer>();
				int necScroll = (int)((float)myplayer.NecrotisResistPercent * (float)fgTex.Height);
				var statSrcRect = new Rectangle(
					x: 0,
					y: fgTex.Height - necScroll,
					width: fgTex.Width,
					height: necScroll
				);

				this.DrawUIAnkh( bgTex, fgTex, pos, statSrcRect, myplayer.NecrotisResistPercent );
				if( myplayer.CurrentNecrotisAfflictPercentRate != 0 ) {
					this.DrawUIAnkhChangeFX( pos, bgTex.Bounds, statSrcRect );
				}

				return true;
			};
			var interfaceLayer = new LegacyGameInterfaceLayer( "Necrotis: Status Display", draw, InterfaceScaleType.UI );

			//layers.RemoveAt( idx );
			layers.Insert( idx, interfaceLayer );
		}


		////

		private void DrawUIAnkh(
					Texture2D bgTex,
					Texture2D fgTex,
					Vector2 pos,
					Rectangle srcRect,
					float necrotisResistPercent ) {
			Main.spriteBatch.Draw(
				texture: bgTex,
				position: pos,
				sourceRectangle: null,
				color: Color.White
			);

			if( srcRect.Height > 0 ) {
				Main.spriteBatch.Draw(
					texture: fgTex,
					position: pos + new Vector2( 0, srcRect.Y ),
					sourceRectangle: srcRect,
					color: Color.White
				);
			}

			var area = new Rectangle( (int)pos.X, (int)pos.Y, bgTex.Width, bgTex.Height );
			if( area.Contains( Main.mouseX, Main.mouseY ) ) {
				float percent = necrotisResistPercent * 100f;
				if( percent < 0f ) { percent = 0f; }

				Main.spriteBatch.DrawString(
					spriteFont: Main.fontMouseText,
					text: percent.ToString( "N0" ) + "% Necrotis Resist",
					position: Main.MouseScreen + new Vector2( 0f, 24f ),
					color: necrotisResistPercent > 0f
						? Color.White
						: Color.Red
				);
			}
		}

		private void DrawUIAnkhChangeFX( Vector2 pos, Rectangle fullSrcRect, Rectangle innerSrcRect ) {
		}
	}
}