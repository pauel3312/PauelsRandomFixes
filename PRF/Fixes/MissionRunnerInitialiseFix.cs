#if CLIENT
using System.Collections.Generic;
using BepInEx.Configuration;
using HarmonyLib;
using NuclearOption.SavedMission;

namespace PRF.Fixes;

[Fix]
[HarmonyPatch]
internal class MissionRunnerInitialiseFix(ConfigFile config) : ConfigurableFix(config)
{
    protected override string Description =>
        $"{base.Description}\nFixes an issue on client preventing Mission Runner from initialising if a unit is " +
        "destroyed at the wrong timing around join.";
    
    [HarmonyPatch(typeof(Mission), nameof(Mission.OnSceneLoaded))]
    [HarmonyPrefix]
    // ReSharper disable once InconsistentNaming
    private static void MissionOnSceneLoadedPrefix(Mission __instance)
    {
        var manager = NetworkSceneSingleton<MissionManager>.i;
        if (manager == null || !manager.IsClient || manager.IsServer)
            return;
        
        var savedUnits = new List<SavedUnit>();
        MissionManager.GetAllSavedUnitsNonAlloc(__instance, savedUnits, false);
        
        foreach (var saved in savedUnits)
            if (saved != null && UnitRegistry.customIDLookup.TryGetValue(saved.UniqueName, out var unit) &&
                unit == null)
                UnitRegistry.customIDLookup.Remove(saved.UniqueName);
    }
}
#endif