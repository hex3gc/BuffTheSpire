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
///     A rare anti-attack enemy to give Axebot a real niche
///     Also encourages passive damage sources or big attacks
/// </summary>
internal static class AxebotPatch
{
    [HarmonyPatch]
    internal static class AxebotPatch_Axebot_AfterAddedToRoom
    {
        [HarmonyPatch(typeof(Axebot), "AfterAddedToRoom")]
        internal static async void Postfix(Axebot __instance)
        {
            if (!BuffTheSpireConfig.AxebotEnabled) { return; }

            await PowerCmd.Apply<AdaptivePower>(new ThrowingPlayerChoiceContext(), __instance.Creature, 3 - __instance.StockAmount, __instance.Creature, null);
        }
    }
}