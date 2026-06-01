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
///     Turret Operator and his shield are pretty easy to burst down, so giving the shield some longevity draws out the fight more
/// </summary>
internal static class LivingShieldTurretOperatorPatch
{
    [HarmonyPatch]
    internal static class LivingShieldTurretOperatorPatch_MonsterModel_AfterAddedToRoom
    {
        [HarmonyPatch(typeof(MonsterModel), "AfterAddedToRoom")]
        internal static async void Postfix(MonsterModel __instance)
        {
            if (__instance is not TurretOperator) { return; }
            if (!BuffTheSpireConfig.TurretOperatorEnabled) { return; }
            if (BuffTheSpireConfig.TurretOperatorMechanicAmount <= 0) { return; }

            await PowerCmd.Apply<MechanicPower>(new ThrowingPlayerChoiceContext(), __instance.Creature, BuffTheSpireConfig.TurretOperatorMechanicAmount, __instance.Creature, null);
        }
    }

    [HarmonyPatch]
    internal static class LivingShieldTurretOperatorPatch_TurretOperator_ReloadMove
    {
        [HarmonyPatch(typeof(TurretOperator), "ReloadMove")]
        internal static async void Postfix(TurretOperator __instance)
        {
            if (!BuffTheSpireConfig.TurretOperatorEnabled) { return; }
            if (BuffTheSpireConfig.TurretOperatorAdditionalStrengthGain <= 0) { return; }

            await PowerCmd.Apply<StrengthPower>(new ThrowingPlayerChoiceContext(), __instance.Creature, BuffTheSpireConfig.TurretOperatorAdditionalStrengthGain, __instance.Creature, null);
        }
    }
}