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
///     The claws now have different ways of messing with your deck, but can be counteracted by passive or active damage.
///     Makes Crusher's block more important.
/// </summary>
internal static class KaiserCrabPatch
{
    [HarmonyPatch]
    internal static class KaiserCrabPatch_Rocket_AfterAddedToRoom
    {
        [HarmonyPatch(typeof(Rocket), "AfterAddedToRoom")]
        internal static async void Postfix(Rocket __instance)
        {
            if (!BuffTheSpireConfig.KaiserCrabEnabled) { return; }
            if (!BuffTheSpireConfig.KaiserCrabSearingBlow) { return; }

            await PowerCmd.Apply<SearingBlowPower>(new ThrowingPlayerChoiceContext(), __instance.Creature, BuffTheSpireConfig.KaiserCrabSearingBlowQty, __instance.Creature, null);
        }
    }
    [HarmonyPatch]
    internal static class KaiserCrabPatch_Crusher_AfterAddedToRoom
    {
        [HarmonyPatch(typeof(Crusher), "AfterAddedToRoom")]
        internal static async void Postfix(Crusher __instance)
        {
            if (!BuffTheSpireConfig.KaiserCrabEnabled) { return; }
            if (!BuffTheSpireConfig.KaiserCrabSuckerPunch) { return; }

            await PowerCmd.Apply<SuckerPunchPower>(new ThrowingPlayerChoiceContext(), __instance.Creature, BuffTheSpireConfig.KaiserCrabSuckerPunchQty, __instance.Creature, null);
        }
    }
}