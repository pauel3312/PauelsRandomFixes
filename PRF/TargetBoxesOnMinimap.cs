using BepInEx.Configuration;
using HarmonyLib;

namespace PRF;

[Fix]
[HarmonyPatch(typeof(TargetMarker))]
internal class TargetBoxesOnMinimap(ConfigFile config): ConfigurableFix(config)
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(TargetMarker.Show))]
    internal static void ShowPostfix(ref TargetMarker __instance, bool value)
    {
        __instance.markerImg.enabled = true;
    }
}