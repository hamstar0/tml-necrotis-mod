using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;
using HUDElementsLib;
using Necrotis.Libraries.Libraries.XNA;


namespace Necrotis.HUD {
	partial class AnkhHUD : HUDElement {
		public static AnkhHUD CreateDefault() {
			var mymod = NecrotisMod.Instance;
			Texture2D bgTex = mymod.GetTexture( "HUD/AnkhBG" );
			Vector2 posOffset = AnkhHUD.GetBaseHudUncomputedPosition();
			Vector2 posPerc = new Vector2(
				posOffset.X < 0f ? 1f : 0f,
				posOffset.Y < 0f ? 1f : 0f
			);
			Vector2 dim = new Vector2( bgTex.Width, bgTex.Height );

			return new AnkhHUD( posOffset, posPerc, dim );
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

		private AnkhHUD( Vector2 posOffset, Vector2 posPerc, Vector2 dim )
					: base( "Anima Gauge", posOffset, posPerc, dim, () => true )  {
			var mymod = NecrotisMod.Instance;
			this.AnkhDripSource = mymod.GetTexture( "HUD/AnkhDripSource" );
			this.AnkhBgTex = mymod.GetTexture( "HUD/AnkhBG" );
			this.AnkhFgTex = mymod.GetTexture( "HUD/AnkhFG" );
			this.AnkhGlowTex = mymod.GetTexture( "HUD/AnkhGlow" );
			this.AnkhUnglowTex = mymod.GetTexture( "HUD/AnkhUnglow" );
			this.AnkhOhmTex = mymod.GetTexture( "HUD/AnkhOhm" );
			this.AnkhShieldTex = mymod.GetTexture( "HUD/AnkhShield" );

			XNALibraries.PremultiplyTexture( this.AnkhDripSource );
			XNALibraries.PremultiplyTexture( this.AnkhGlowTex );
			XNALibraries.PremultiplyTexture( this.AnkhUnglowTex );
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