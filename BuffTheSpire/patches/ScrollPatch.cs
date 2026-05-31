using System.Collections.ObjectModel;
using System.Reflection.Emit;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.GameInfo.Objects;
using BuffTheSpire.Config;

namespace Act3Buff.Patches;

/// <summary>
///     Scrolls Of Biting can now seriously drain max HP, which is important as the game has so many sources of it
/// </summary>
internal static class ScrollPatch
{
    // Max HP changes
    [HarmonyPatch]
    internal static class ScrollPatch_ScrollOfBiting_MinInitialHp
    {
        [HarmonyPatch(typeof(ScrollOfBiting), "MinInitialHp", MethodType.Getter)]
        internal static bool Prefix(ScrollOfBiting __instance, ref int __result)
        {
            if (!BuffTheSpireConfig.ScrollEnabled) { return true; }

            __result = AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, (int)BuffTheSpireConfig.ScrollMinMaxHpHard, (int)BuffTheSpireConfig.ScrollMinMaxHpEasy);
            return false;
        }
    }
    [HarmonyPatch]
    internal static class ScrollPatch_ScrollOfBiting_MaxInitialHp
    {
        [HarmonyPatch(typeof(ScrollOfBiting), "MaxInitialHp", MethodType.Getter)]
        internal static bool Prefix(ScrollOfBiting __instance, ref int __result)
        {
            if (!BuffTheSpireConfig.ScrollEnabled) { return true; }

            __result = AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, (int)BuffTheSpireConfig.ScrollMaxMaxHpHard, (int)BuffTheSpireConfig.ScrollMaxMaxHpEasy);
            return false;
        }
    }

    // Paper cuts power increase
    [HarmonyPatch]
    internal static class ScrollPatch_ScrollOfBiting_AfterAddedToRoom
    {
        [HarmonyPatch(typeof(ScrollOfBiting), "AfterAddedToRoom")]
        internal static bool Prefix(ScrollOfBiting __instance, ref Task __result)
        {
            if (!BuffTheSpireConfig.ScrollEnabled) { return true; }

            __result = Task.CompletedTask;
            return false;
        }
        [HarmonyPatch(typeof(ScrollOfBiting), "AfterAddedToRoom")]
        internal static async void Postfix(ScrollOfBiting __instance)
        {
            if (!BuffTheSpireConfig.ScrollEnabled) { return; }

            await PowerCmd.Apply<PaperCutsPower>(new ThrowingPlayerChoiceContext(), __instance.Creature, (int)BuffTheSpireConfig.ScrollMaxHpReduction, __instance.Creature, null);
        }
    }
}