using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace Necrotis.UI {
	partial class AnkhHUD {
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



		////////////////

		public AnkhHUD() {
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
	}
}