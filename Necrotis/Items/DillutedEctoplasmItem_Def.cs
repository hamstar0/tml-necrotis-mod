using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Necrotis.Recipes;


namespace Necrotis.Items {
	public partial class DillutedEctoplasmItem : ModItem {
		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Dilluted Ectoplasm" );
			this.Tooltip.SetDefault( "Psychomagnotheric secretions from spiritual entities. Dilluted." );

			int mytype = ItemType<DillutedEctoplasmItem>();
			ItemID.Sets.ItemNoGravity[ mytype ] = true;
		}

		public override void SetDefaults() {
			this.item.width = 16;
			this.item.height = 16;
			this.item.value = 0;
			this.item.rare = ItemRarityID.Green;
		}


		////////////////

		 private double Animation = 0f;

		public override bool PreDrawInWorld( SpriteBatch sb, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI ) {
			float amp = (float)Math.Sin( this.Animation );

			scale = 0.8f + ( 0.3f * amp );

			this.Animation += 1f / 60f;
			this.Animation %= Math.PI;

			Lighting.AddLight( this.item.Center, 0.5f*amp, 0.5f*amp, 0.325f*amp );

			return base.PreDrawInWorld( sb, lightColor, alphaColor, ref rotation, ref scale, whoAmI );
		}


		////////////////

		public override void AddRecipes() {
			var config = NecrotisConfig.Instance;
			if( !config.Get<bool>( nameof(config.DillutedEctoplasmRecipeEnabled) ) ) {
				return;
			}

			var recipe1 = new DillutedEctoplasmItemRecipe( this );
			recipe1.AddRecipe();
			
			var recipe2 = new DillutedEctoplasmItemRecipe( this, (ItemID.Bone, 1) );
			recipe2.AddRecipe();
		}
	}
}