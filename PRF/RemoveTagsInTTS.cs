using System.Text.RegularExpressions;
using BepInEx.Configuration;
using HarmonyLib;
using NuclearOption.Chat;

namespace PRF;

[Fix]
[HarmonyPatch(typeof(ChatManager))]
// ReSharper disable once InconsistentNaming
internal class RemoveTagsInTTS(ConfigFile config): ConfigurableFix(config)
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(ChatManager.RunTTS))]
    // ReSharper disable once InconsistentNaming
    internal static void RunTTSPrefix(ref string playerName, ref string message)
    {
        playerName = Regex.Replace(playerName, "<.*?>", string.Empty);
        message = Regex.Replace(message, "<.*?>", string.Empty);
    }
}