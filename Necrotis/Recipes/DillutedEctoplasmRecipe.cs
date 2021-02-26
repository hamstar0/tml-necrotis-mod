using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Necrotis.Items;


namespace Necrotis.Recipes {
	class DillutedEctoplasmItemRecipe : ModRecipe {
		public DillutedEctoplasmItemRecipe( DillutedEctoplasmItem myitem, (int type, int stack)? altItem=null )
					: base( myitem.mod ) {
			this.AddTile( TileID.Bottles );

			this.AddIngredient( ItemID.Gel, 10 );
			this.AddRecipeGroup( "Necrotis:StrangePlants", 1 );
			if( altItem.HasValue ) {
				this.AddIngredient( altItem.Value.type, altItem.Value.stack );
			} else {
				this.AddRecipeGroup( "Necrotis:Tombstone", 1 );
			}

			this.SetResult( myitem );
		}

		public override bool RecipeAvailable() {
			var config = NecrotisConfig.Instance;
			return config.Get<bool>( nameof( config.DillutedEctoplasmRecipeEnabled ) );
		}
	}
}