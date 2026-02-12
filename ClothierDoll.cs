using HarmonyLib;
using Terraria;
using Terraria.ID;
using Terraria.Audio;

namespace Tweaks
{
    [HarmonyPatch]
	public class ClothierVoodooDoll
    {
		[HarmonyPostfix]
		[HarmonyPatch(typeof(Item), "SetDefaults")]
		public static void SetDefaults(int Type, Item __instance)
		{
			if (Type == ItemID.ClothierVoodooDoll)
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
			if (__instance.ItemTimeIsZero && __instance.itemAnimation > 0 && sItem.type == ItemID.ClothierVoodooDoll && __instance.SummonItemCheck(sItem))
			{
				if (!Main.IsItDay())
				{
					__instance.ApplyItemTime(sItem);
					SoundEngine.PlaySound(15, (int)__instance.position.X, (int)__instance.position.Y, 0, 1f, 0f);
					if (Main.netMode != 1)
					{
						NPC.SpawnOnPlayer(onWhichPlayer, NPCID.SkeletronHead, 0f, 0f, 0f, 0f);
					}
					else
					{
						NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, __instance.whoAmI, NPCID.SkeletronHead, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}
		
		[HarmonyPostfix]
		[HarmonyPatch(typeof(Player), "SummonItemCheck")]
		public static void SummonItemCheck(Item item, ref bool __result)
		{
			if(item.type == ItemID.ClothierVoodooDoll) {
				__result = !NPC.AnyNPCs(NPCID.SkeletronHead);
			}
		}
    }
}