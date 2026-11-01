using HarmonyLib;
using NuclearOption.Networking;
using UnityEngine;

namespace PRF.Fixes;

[Fix]
[HarmonyPatch(typeof(Player))]
internal class ServerValidateConvoySpawn
{
    [HarmonyPatch(nameof(Player.UserCode_CmdPurchaseConvoy_\u002D772415385))]
    [HarmonyPrefix]
    public static bool Prefix(Player __instance, int convoyIndex)
    {
        if (__instance.HQ == null || !__instance.HQ.faction.TryGetConvoyGroup(convoyIndex, out var convoyGroup))
            return true;
        if (__instance.HQ.convoySpawned.TryGetValue(convoyGroup, out var tsslAtLastSpawn))
        {
            var tsslFirstAllowed = convoyGroup.coolDown + tsslAtLastSpawn;
            
            if (tsslFirstAllowed <= Time.timeSinceLevelLoad) return true;

            ColorLog<Player>.InfoWarn($"{__instance} may be trying to cheat by spamming convoy purchases");
            __instance.Owner.SetError(1, NuclearOptionPlayerErrorFlags.Custom(267));
            return false;
        }
        return true;
    }
}