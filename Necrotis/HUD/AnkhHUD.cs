using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;
using HUDElementsLib;


namespace Necrotis.HUD {
	partial class AnkhHUD : HUDElement {
		public static AnkhHUD CreateDefault() {
			var mymod = NecrotisMod.Instance;
			Texture2D bgTex = mymod.GetTexture( "HUD/AnkhBG" );
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
		private Texture2D AnkhShieldTex;

		private bool IsShowingDefaultHoverText = false;



		////////////////

		private AnkhHUD( Vector2 pos, Vector2 dim ) : base( "Anima Gauge", pos, dim, () => true )  {
			var mymod = NecrotisMod.Instance;
			this.AnkhDripSource = mymod.GetTexture( "HUD/AnkhDripSource" );
			this.AnkhBgTex = mymod.GetTexture( "HUD/AnkhBG" );
			this.AnkhFgTex = mymod.GetTexture( "HUD/AnkhFG" );
			this.AnkhGlowTex = mymod.GetTexture( "HUD/AnkhGlow" );
			this.AnkhUnglowTex = mymod.GetTexture( "HUD/AnkhUnglow" );
			this.AnkhOhmTex = mymod.GetTexture( "HUD/AnkhOhm" );
			this.AnkhShieldTex = mymod.GetTexture( "HUD/AnkhShield" );

			AnkhHUD.PremultiplyTexture( this.AnkhDripSource );
			AnkhHUD.PremultiplyTexture( this.AnkhGlowTex );
			AnkhHUD.PremultiplyTexture( this.AnkhUnglowTex );
		}


		////////////////

		public override (string text, int duration) GetHoverText(
					bool editMode,
					bool isCollisionToggleButton,
					bool isResetButton,
					bool isAnchorRightToggle,
					bool isAnchorBottomToggle ) {
			(string text, int duration) = base.GetHoverText(
				editMode,
				isCollisionToggleButton,
				isResetButton,
				isAnchorRightToggle,
				isAnchorBottomToggle
			);

			this.IsShowingDefaultHoverText = !string.IsNullOrEmpty( text );

			return (text, duration);
		}
	}
}