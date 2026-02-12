using HarmonyLib;
using Terraria;
using Terraria.ID;

namespace Tweaks
{
    [HarmonyPatch]
	public class GuideDoll
    {
		[HarmonyPostfix]
		[HarmonyPatch(typeof(Item), "SetDefaults")]
		public static void SetDefaults(int Type, Item __instance)
		{
			if (Type == ItemID.GuideVoodooDoll)
			{
				__instance.useStyle = 4;
				__instance.consumable = true;
				__instance.useAnimation = 45;
				__instance.useTime = 45;
			}
		}
		
		[HarmonyPostfix]
		[HarmonyPatch(typeof(Player), "ItemCheck_UseBossSpawners")]
		public static void ItemCheck_UseBossSpawners(int onWhichPlayer, Item sItem, Player __instance)
		{
			if (__instance.ItemTimeIsZero && __instance.itemAnimation > 0 && sItem.type == ItemID.GuideVoodooDoll && __instance.SummonItemCheck(sItem))
			{
				NPC.SpawnWOF(__instance.position);
				//SpawnWOF may or may not succeed but doesn't return anything, check if WoF spawned to apply useTime
				if(NPC.AnyNPCs(NPCID.WallofFlesh)) 
                {
                    __instance.ApplyItemTime(sItem);
                }
			}
		}
		
		[HarmonyPostfix]
		[HarmonyPatch(typeof(Player), "SummonItemCheck")]
		public static void SummonItemCheck(Item item, ref bool __result)
		{
			if(item.type == ItemID.GuideVoodooDoll) {
				__result = !NPC.AnyNPCs(NPCID.WallofFlesh);
			}
		}
    }
}