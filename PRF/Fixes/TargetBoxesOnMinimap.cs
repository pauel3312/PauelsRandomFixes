using BepInEx.Configuration;
using HarmonyLib;

namespace PRF.Fixes;

[Fix]
[HarmonyPatch(typeof(TargetMarker))]
internal class TargetBoxesOnMinimap(ConfigFile config) : ConfigurableFix(config)
{
    protected override string Description =>
        $"{base.Description}\nFixes showing target boxes on selected units on the minimap.";
    
    [HarmonyPatch(nameof(TargetMarker.Show))]
    [HarmonyPostfix]
    internal static void ShowPostfix(ref TargetMarker __instance, bool value)
    {
        __instance.markerImg.enabled = MapOptions.i.showTargetInfo;
    }
}