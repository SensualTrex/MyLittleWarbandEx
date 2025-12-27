using System;
using HarmonyLib;
using TaleWorlds.Core;

namespace MyLittleWarbandEx.Patches
{
    // Define the class containing the original property
    [HarmonyPatch(typeof(BasicCharacterObject))]
    // Target the 'get' method of the property named "GetComp" (example)
    [HarmonyPatch("IsRanged", MethodType.Getter)]
    internal class IsRanged
    {
        // Use a Prefix to run before the original getter
        [HarmonyPrefix]
        public static bool Prefix(BasicCharacterObject __instance, ref bool __result) // Use __result to control the return value
        {
            __result = (__instance.DefaultFormationClass.IsRanged());
            // Return false to skip the original getter method entirely
            return __result;
        }
    }
}