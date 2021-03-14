using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HUDElementsLib;


namespace Necrotis.HUD {
	partial class AnkhHUD : HUDElement {
		public static AnkhHUD CreateDefault() {
			var mymod = NecrotisMod.Instance;
			Texture2D bgTex = mymod.GetTexture( "UI/AnkhBG" );
			Vector2 pos = AnkhHUD.GetBaseHudUncomputedPosition();
			Vector2 dim = new Vector2( bgTex.Width, bgTex.Height );

			return new AnkhHUD( pos, dim );
		}


		////////////////

		public static Vector2 GetBaseHudUncomputedPosition() {
			var config = NecrotisConfig.Instance;
			Vector2 pos = new Vector2(
				config.Get<int>( nameof(config.InitialAnkhScreenPositionX) ),
				config.Get<int>( nameof(config.InitialAnkhScreenPositionY) )
			);

			return pos;
		}


		////////////////
		
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

		private Texture2D AnkhDripSource;
		private Texture2D AnkhBgTex;
		private Texture2D AnkhFgTex;
		private Texture2D AnkhGlowTex;
		private Texture2D AnkhUnglowTex;
		private Texture2D AnkhOhmTex;

		private bool IsShowingDefaultHoverText = false;



		////////////////

		private AnkhHUD( Vector2 pos, Vector2 dim ) : base( "Anima Gauge", pos, dim )  {
			var mymod = NecrotisMod.Instance;
			this.AnkhDripSource = mymod.GetTexture( "UI/AnkhDripSource" );
			this.AnkhBgTex = mymod.GetTexture( "UI/AnkhBG" );
			this.AnkhFgTex = mymod.GetTexture( "UI/AnkhFG" );
			this.AnkhGlowTex = mymod.GetTexture( "UI/AnkhGlow" );
			this.AnkhUnglowTex = mymod.GetTexture( "UI/AnkhUnglow" );
			this.AnkhOhmTex = mymod.GetTexture( "UI/AnkhOhm" );

			AnkhHUD.PremultiplyTexture( this.AnkhDripSource );
			AnkhHUD.PremultiplyTexture( this.AnkhGlowTex );
			AnkhHUD.PremultiplyTexture( this.AnkhUnglowTex );
		}


		////////////////

		public override string GetHoverText( bool isCollisionToggleButton, bool isAnchorRightToggle, bool isAnchorBottomToggle ) {
			string text = base.GetHoverText( isCollisionToggleButton, isAnchorRightToggle, isAnchorBottomToggle );

			this.IsShowingDefaultHoverText = !string.IsNullOrEmpty( text );

			return text;
		}
	}
}