﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Utilities;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Helpers.TModLoader;


namespace Necrotis.Libraries.Services.FX {
	public class CustomParticle : ILoadable {
		private static IList<CustomParticle> Particles;



		////////////////

		public static void Create(
					bool isInWorld,
					Vector2 pos,
					int tickDuration,
					Color color,
					float scale,
					float sprayAmt,
					bool hasGravity ) {
			var particle = new CustomParticle( isInWorld, pos, tickDuration, color, scale, sprayAmt, hasGravity );
			CustomParticle.Particles.Add( particle );
		}

		internal static void UpdateParticles() {
			int len = CustomParticle.Particles.Count;
			for( int i=0; i<len; i++ ) {
				CustomParticle particle = CustomParticle.Particles[i];

				if( !particle.Update() ) {
					CustomParticle.Particles.RemoveAt( i-- );
					len--;
				}
			}
		}

		internal static void DrawParticles( SpriteBatch sb ) {
			foreach( CustomParticle particle in CustomParticle.Particles ) {
				particle.Draw( sb );
			}
		}



		////////////////

		public int TickDuration { get; }
		private int TicksElapsed = 0;

		public bool IsInWorld { get; }
		public Vector2 Position { get; private set; }
		public Color Color { get; private set; }
		public float Scale { get; }
		public float SprayAmt { get; }
		public bool HasGravity { get; }

		private Vector2 Velocity = Vector2.Zero;

		private float CurrRot = 0f;
		private float RotVelocity = 0f;



		////////////////

		private CustomParticle(
					bool isInWorld,
					Vector2 pos,
					int tickDuration,
					Color color,
					float scale,
					float sprayAmt,
					bool hasGravity ) {
			this.IsInWorld = isInWorld;
			this.Position = pos;
			this.TickDuration = tickDuration;
			this.Color = color;
			this.Scale = scale;
			this.SprayAmt = sprayAmt;
			this.HasGravity = hasGravity;

			UnifiedRandom rand = TmlHelpers.SafelyGetRand();

			this.Velocity = new Vector2(
				rand.NextFloat() * this.SprayAmt,
				rand.NextFloat() * this.SprayAmt
			);
			this.RotVelocity = rand.NextFloat() - 0.5f;
		}



		////////////////

		void ILoadable.OnModsLoad() {
			CustomParticle.Particles = new List<CustomParticle>();
		}

		void ILoadable.OnModsUnload() {
			CustomParticle.Particles = null;
		}

		void ILoadable.OnPostModsLoad() { }


		////////////////

		public bool Update() {
			this.TicksElapsed++;
			if( (this.TickDuration - this.TicksElapsed) <= 0 ) {
				return false;
			}

			this.Position += this.Velocity;

			if( this.HasGravity ) {
				this.Velocity.Y += 0.01f;
			}

			UnifiedRandom rand = TmlHelpers.SafelyGetRand();

			this.CurrRot = (this.CurrRot + this.RotVelocity) % (MathHelper.Pi * 2f);

			float percentDone = (float)this.TicksElapsed / (float)this.TickDuration;
			float colorScale = 1f - (percentDone * percentDone * percentDone);

			this.Color *= colorScale;

			return true;
		}


		////////////////

		public void Draw( SpriteBatch sb ) {
			sb.Draw(
				texture: Main.magicPixel,
				position: this.Position,
				sourceRectangle: null,
				color: this.Color,
				rotation: this.CurrRot,
				origin: new Vector2( Main.magicPixel.Width/2, Main.magicPixel.Height/2 ),
				scale: this.Scale,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);
		}
	}
}