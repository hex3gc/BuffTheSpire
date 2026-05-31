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
///     Quicksand encourages a tighter balance between sticking close to The Insatiable and running away. Also gives it an additional vector for scaling.
/// </summary>
internal static class TheInsatiablePatch
{
    [HarmonyPatch]
    internal static class TheInsatiablePatch_TheInsatiable_LiquifyMove
    {
        [HarmonyPatch(typeof(TheInsatiable), "LiquifyMove")]
        internal static async void Postfix(TheInsatiable __instance, IReadOnlyList<Creature> targets)
        {
            if (!BuffTheSpireConfig.TheInsatiableEnabled) { return; }
            if (!BuffTheSpireConfig.TheInsatiableQuicksand) { return; }

            foreach (Creature target in targets)
            {
                await PowerCmd.Apply<QuicksandPower>(new ThrowingPlayerChoiceContext(), target, BuffTheSpireConfig.TheInsatiableQuicksandDamage, __instance.Creature, null);
            }
        }
    }

    [HarmonyPatch]
    internal static class TheInsatiablePatch_TheInsatiable_SalivateMove
    {
        [HarmonyPatch(typeof(TheInsatiable), "SalivateMove")]
        internal static async void Postfix(TheInsatiable __instance, IReadOnlyList<Creature> targets)
        {
            if (!BuffTheSpireConfig.TheInsatiableEnabled) { return; }
            if (!BuffTheSpireConfig.TheInsatiableQuicksand) { return; }
            if (!BuffTheSpireConfig.TheInsatiableQuicksandBuff) { return; }

            foreach (Player player in __instance.CombatState.Players)
            {
                await PowerCmd.Apply<QuicksandPower>(new ThrowingPlayerChoiceContext(), player.Creature, BuffTheSpireConfig.TheInsatiableQuicksandDamage, __instance.Creature, null);
            }
        }
    }
}