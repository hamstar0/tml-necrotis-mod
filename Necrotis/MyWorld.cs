using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using ModLibsCore.Libraries.DotNET.Extensions;
using Necrotis.Libraries.Services.FX;
using Necrotis.Items;


namespace Necrotis {
	class NecrotisWorld : ModWorld {
		private IDictionary<float, ISet<float>> LoadEctoPositions = new Dictionary<float, ISet<float>>();



		////////////////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey("dilluted_ecto_count") ) {
				return;
			}

			int count = tag.GetInt( "dilluted_ecto_count" );

			for( int i=0; i<count; i++ ) {
				this.LoadEctoPositions.Set2D(
					(float)tag.GetFloat( "dilluted_ecto_x_" + i ),
					(float)tag.GetFloat( "dilluted_ecto_y_" + i )
				);
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound();
			int ectoType = ModContent.ItemType<DillutedEctoplasmItem>();

			int j = 0;
			for( int i=0; i<Main.item.Length; i++ ) {
				Item item = Main.item[i];
				if( item?.active != true || item.type != ectoType ) {
					continue;
				}

				tag[ "dilluted_ecto_x_"+j ] = (float)item.position.X;
				tag[ "dilluted_ecto_y_"+j ] = (float)item.position.Y;
				j++;
			}

			tag[ "dilluted_ecto_count" ] = (int)j;

			return tag;
		}


		////////////////

		public override void PostUpdate() {
			if( this.LoadEctoPositions.Count == 0 ) {
				return;
			}

			Player anyPlr = Main.player.FirstOrDefault( p => p?.active == true );
			if( anyPlr == null ) {
				return;
			}

			int ectoType = ModContent.ItemType<DillutedEctoplasmItem>();

			foreach( (float wldX, ISet<float> wldYs) in this.LoadEctoPositions ) {
				foreach( float wldY in wldYs ) {
					int itemWho = Item.NewItem(
						position: new Vector2( wldX, wldY ),
						Type: ectoType
					);
					Main.item[itemWho].velocity = default;
				}
			}

			this.LoadEctoPositions.Clear();
		}


		////////////////

		public override void PostDrawTiles() {
			CustomParticle.DrawParticles( Main.spriteBatch, true );
		}
	}
}
