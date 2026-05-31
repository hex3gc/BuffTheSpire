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
///     Being able to buff their leader's strength makes The Kin far more threatening
///     It becomes necessary to burst down at least one Kin Follower by turn 3
/// </summary>
internal static class TheKinPatch
{
    [HarmonyPatch]
    internal static class TheKinPatch_KinFollower_PowerDanceMove
    {
        [HarmonyPatch(typeof(KinFollower), "PowerDanceMove")]
        internal static async void Postfix(KinFollower __instance, IReadOnlyList<Creature> targets)
        {
            if (!BuffTheSpireConfig.TheKinEnabled) { return; }
            if (!BuffTheSpireConfig.TheKinBuffLeader) { return; }

            foreach (Creature enemy in __instance.CombatState.Enemies)
            {
                if (enemy.Monster is KinPriest)
                {
                    await PowerCmd.Apply<StrengthPower>(new ThrowingPlayerChoiceContext(), enemy, __instance.DanceStrength, __instance.Creature, null);
                }
            }
        }
    }

    [HarmonyPatch]
    internal static class TheKinPatch_KinFollower_GenerateMoveStateMachine
    {
        [HarmonyPatch(typeof(KinFollower), "GenerateMoveStateMachine")]
        internal static bool Prefix(KinFollower __instance, ref MonsterMoveStateMachine __result)
        {
            if (!BuffTheSpireConfig.TheKinEnabled) { return true; }
            if (!BuffTheSpireConfig.TheKinBuffLeader) { return true; }

            List<MonsterState> list = new List<MonsterState>();
            MoveState moveState = new MoveState("QUICK_SLASH_MOVE", __instance.QuickSlashMove, new SingleAttackIntent(__instance.QuickSlashDamage));
            MoveState moveState2 = new MoveState("BOOMERANG_MOVE", __instance.BoomerangMove, new MultiAttackIntent(__instance.BoomerangDamage, 2));
            MoveState moveState2b = new MoveState("QUICK_SLASH_MOVE_B", __instance.QuickSlashMove, new SingleAttackIntent(__instance.QuickSlashDamage));
            MoveState moveState3 = new MoveState("POWER_DANCE_MOVE", __instance.PowerDanceMove, new BuffIntent());
            moveState.FollowUpState = moveState2;
            moveState2.FollowUpState = moveState2b;
            moveState2b.FollowUpState = moveState3;
            moveState3.FollowUpState = moveState;
            list.Add(moveState);
            list.Add(moveState2);
            list.Add(moveState2b);
            list.Add(moveState3);
            MoveState initialState = (__instance.StartsWithDance ? moveState3 : moveState);
            __result = new MonsterMoveStateMachine(list, initialState);
            return false;
        }
    }
}