using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace Necrotis.UI {
	partial class AnkhHUD {
		public static Vector2 GetHUDBasePosition() {
			var config = NecrotisConfig.Instance;
			Vector2 pos;

			if( Main.playerInventory ) {
				pos = new Vector2(
					config.Get<int>( nameof(config.AnkhInvScreenPositionX) ),
					config.Get<int>( nameof(config.AnkhInvScreenPositionY) )
				);
			} else {
				pos = new Vector2(
					config.Get<int>( nameof(config.AnkhScreenPositionX) ),
					config.Get<int>( nameof(config.AnkhScreenPositionY) )
				);
			}

			if( pos.X < 0 ) {
				pos.X = Main.screenWidth + pos.X;
			}
			if( pos.Y < 0 ) {
				pos.Y = Main.screenHeight + pos.Y;
			}

			return pos;
		}

		public static Vector2 GetHUDPosition() {
			var myplayer = Main.LocalPlayer.GetModPlayer<NecrotisPlayer>();
			return AnkhHUD.GetHUDPosition() + myplayer.AnkhHUDDisplayOffset;
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

		private bool IsHovering = false;



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


		////////////////

		public void Update() {
			if( Main.playerInventory ) {
				this.RunHUDEditor( out bool isHovering );

				Main.LocalPlayer.mouseInterface = Main.LocalPlayer.mouseInterface || isHovering;
				this.IsHovering = isHovering;
			} else {
				this.IsHovering = false;
			}
		}


		////////////////

		public bool ConsumesCursor() {
			return this.IsHovering;
		}
	}
}