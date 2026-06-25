using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine.UI;
using System;

namespace PRF;

[Fix]
[HarmonyPatch(typeof(MessageUI), "Awake")]
internal class ClickthroughChatbox(ConfigFile config): ConfigurableFix(config)
{
    static void Postfix(MessageUI __instance)
    {
        try
        {
            __instance.messageText.raycastTarget = false;
            __instance.killFeedText.raycastTarget = false;

            var image = __instance.messageBackground.GetComponent<Image>();

            if (image != null)
                image.raycastTarget = false;
        }
        catch (Exception ex)
        {
            PRF.Logger.LogError(ex);
        }
    }
}