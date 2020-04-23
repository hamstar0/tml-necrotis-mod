using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using Necrotis.Items;


namespace Necrotis {
	class NecrotisWorld : ModWorld {
		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey("dilluted_ecto_count") ) {
				return;
			}

			int ectoType = ItemType<DillutedEctoplasmItem>();
			int count = tag.GetInt( "dilluted_ecto_count" );

			for( int i=0; i<count; i++ ) {
				var pos = new Vector2(
					tag.GetFloat( "dilluted_ecto_x_" + i ),
					tag.GetFloat( "dilluted_ecto_y_" + i )
				);

				Item.NewItem( pos, ectoType );
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound();
			int ectoType = ItemType<DillutedEctoplasmItem>();

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
	}
}
