using HarmonyLib;
using System.IO;

namespace Tweaks
{
	
    public class Patch
    {
        public static void Execute()
        {
            var harmony = new Harmony("com.pezeee.voodoo_summon");
			
			harmony.PatchAll();
        }
    }
}