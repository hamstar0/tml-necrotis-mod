using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using Necrotis.Libraries.Services.FX;


namespace Necrotis {
	public partial class NecrotisMod : Mod {
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


		private void DrawUIAnkhChangeFX( Vector2 pos, Rectangle innerSrcRect, float necrotisResistPercentChangeRate ) {
			if( necrotisResistPercentChangeRate < 0f ) {
//DebugHelpers.Print( "drain", "drain:" + (-necrotisResistPercentChangeRate * 1024f) );
				if( Main.rand.NextFloat() < (-necrotisResistPercentChangeRate * 1024f) ) {
					int duration = Main.rand.Next( 15, 60 );
					var newPos = pos + new Vector2(
						(float)innerSrcRect.Width * Main.rand.NextFloat(),
						innerSrcRect.Y
					);

					CustomParticle.Create( false, newPos, duration, Color.Gold, 2f, 1f, true );
				}
			} else if( necrotisResistPercentChangeRate > 0f ) {
				Texture2D glowTex = this.AnkhGlowTex;
				float brite = Math.Min( (necrotisResistPercentChangeRate * 2048f), 1f );
//DebugHelpers.Print( "brite", "brite:" + brite );
				
				Main.spriteBatch.Draw(
					texture: glowTex,
					position: pos + new Vector2(-5f, -5f),
					sourceRectangle: null,
					color: Color.White * brite
				);
			}
		}


		private void DrawAnkhHoverTooltip( float necrotisResistPercent ) {
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
}