using BepInEx.Configuration;
using HarmonyLib;

namespace PRF;

[Fix]
[HarmonyPatch]
internal class BrakeAsAxis: ConfigurableFix
{
    public BrakeAsAxis(ConfigFile config) : base(config)
    {
        _useBrakesNegativeRegion = config.Bind("BrakeAsAxis", "UseBrakesNegativeRegion", false, "Use negative region of input for the brake axis");
    }

    private static ConfigEntry<bool> _useBrakesNegativeRegion = null!;
    
    [HarmonyPatch(typeof(PilotPlayerState), nameof(PilotPlayerState.PlayerControls))]
    [HarmonyPostfix]
    public static void PlayerControlsPostfix(PilotPlayerState __instance)
    {
        if (_useBrakesNegativeRegion.Value)
        {
            __instance.controlInputs.brake = (__instance.player.GetAxisRaw("Brake") + 1) / 2f;
        }
        else
        {
            __instance.controlInputs.brake = __instance.player.GetAxisRaw("Brake");
        }
    }
}