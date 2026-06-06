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
using MegaCrit.Sts2.Core.Runs;
using BuffTheSpire.Powers;
using Godot;

namespace BuffTheSpire.Patches;

/// <summary>
///     Mecha Knight now rewards card spamming and less passive play styles (e.g Souls, shivs)
///     Should introduce some additional challenge in the same act as Aeonglass
/// </summary>
internal static class MechaKnightPatch
{
    // Add power
    [HarmonyPatch]
    internal static class MechaKnightPatch_MechaKnight_AfterAddedToRoom
    {
        [HarmonyPatch(typeof(MechaKnight), "AfterAddedToRoom")]
        internal static async void Postfix(MechaKnight __instance)
        {
            if (!BuffTheSpireConfig.MechaKnightEnabled) { return; }
            if (!BuffTheSpireConfig.MechaKnightOffKilter) { return; }

            await PowerCmd.Apply<OffKilterPower>(new ThrowingPlayerChoiceContext(), __instance.Creature, 1, __instance.Creature, null);
        }
    }

    // Strength buff
    [HarmonyPatch]
    internal static class MechaKnightPatch_MechaKnight_WindupMove
    {
        [HarmonyPatch(typeof(MechaKnight), "WindupMove")]
        internal static async void Postfix(MechaKnight __instance, IReadOnlyList<Creature> targets)
        {
            if (!BuffTheSpireConfig.MechaKnightEnabled) { return; }

            if (BuffTheSpireConfig.MechaknightStrengthBuff > 0)
            {
                await PowerCmd.Apply<StrengthPower>(new ThrowingPlayerChoiceContext(), __instance.Creature, BuffTheSpireConfig.MechaknightStrengthBuff, __instance.Creature, null);
            }
        }
    }

    // Damage numbers
    [HarmonyPatch]
    internal static class MechaKnightPatch_MechaKnight_ChargeDamage
    {
        [HarmonyPatch(typeof(MechaKnight), "ChargeDamage", MethodType.Getter)]
        internal static bool Prefix(MechaKnight __instance, ref int __result)
        {
            if (!BuffTheSpireConfig.MechaKnightEnabled) { return true; }

            __result = AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, BuffTheSpireConfig.MechaknightChargeDamageHard, BuffTheSpireConfig.MechaknightChargeDamageEasy);
            return false;
        }
    }
    [HarmonyPatch]
    internal static class MechaKnightPatch_MechaKnight_HeavyCleaveDamage
    {
        [HarmonyPatch(typeof(MechaKnight), "HeavyCleaveDamage", MethodType.Getter)]
        internal static bool Prefix(MechaKnight __instance, ref int __result)
        {
            if (!BuffTheSpireConfig.MechaKnightEnabled) { return true; }

            __result = AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, BuffTheSpireConfig.MechaknightCleaveDamageHard, BuffTheSpireConfig.MechaknightCleaveDamageEasy);
            return false;
        }
    }
}