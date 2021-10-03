using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;


namespace Necrotis {
	public delegate void AnimaChangeHook(
		Player player,
		float oldPercent,
		ref string context,
		ref float percentLost,
		ref bool quiet
	);




	public partial class NecrotisAPI : ILoadable {
		public static void AddAnimaChangeHook( AnimaChangeHook hook ) {
			ModContent.GetInstance<NecrotisAPI>().AnimaChangeHooks.Add( hook );
		}


		////////////////

		internal static void RunAnimaChangeHooks(
					Player player,
					float oldAnimaAmount,
					ref string context,
					ref float animaToLose,
					ref bool quiet ) {
			var api = ModContent.GetInstance<NecrotisAPI>();

			foreach( var hook in api.AnimaChangeHooks ) {
				hook.Invoke( player, oldAnimaAmount, ref context, ref animaToLose, ref quiet );
			}
		}
	}
}
