using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
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

			int idx = layers.FindIndex( layer => layer.Name.Equals( "Vanilla: Resource Bars" ) );
			if( idx == -1 ) {
				return;
			}

			GameInterfaceDrawMethod drawAnkh = () => {
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

				this.DrawUIAnkhChangeFX( pos, statSrcRect, myplayer.CurrentNecrotisResistPercentChangeRate );
				this.DrawUIAnkh( bgTex, fgTex, pos, statSrcRect, myplayer.NecrotisResistPercent );

				return true;
			};

			GameInterfaceDrawMethod drawParticles = () => {
				CustomParticle.DrawParticles( Main.spriteBatch, false );
				return true;
			};

			var ankhLayer = new LegacyGameInterfaceLayer( "Necrotis: Status Display", drawAnkh, InterfaceScaleType.UI );
			var particleLayer = new LegacyGameInterfaceLayer( "Necrotis: UI Particles", drawParticles, InterfaceScaleType.UI );

			//layers.RemoveAt( idx );
			layers.Insert( idx + 1, particleLayer );
			layers.Insert( idx + 1, ankhLayer );
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

		private void DrawUIAnkhChangeFX( Vector2 pos, Rectangle innerSrcRect, float necrotisResistPercentChangeRate ) {
			if( necrotisResistPercentChangeRate < 0f ) {
//DebugHelpers.Print( "drain", "drain:" + (-necrotisResistPercentChangeRate * 1024f) );
				if( Main.rand.NextFloat() < (-necrotisResistPercentChangeRate * 1024f) ) {
					int duration = Main.rand.Next( 15, 60 );
					var newPos = pos + new Vector2(
						(float)innerSrcRect.Width * Main.rand.NextFloat(),
						innerSrcRect.Y
					);

					CustomParticle.Create( false, newPos, duration, Color.Gold, 1f, 1f, true );
				}
			} else if( necrotisResistPercentChangeRate > 0f ) {
				Texture2D glowTex = this.AnkhGlowTex;
				float brite = Math.Min( (necrotisResistPercentChangeRate * 2048f), 1f );
//DebugHelpers.Print( "brite", "brite:" + brite );
				
				Main.spriteBatch.Draw(
					texture: glowTex,
					position: pos + new Vector2(-8f, -8f),
					sourceRectangle: null,
					color: Color.White * brite
				);
			}
		}
	}
}