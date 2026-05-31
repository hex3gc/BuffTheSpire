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
///     Lagavulin debuffing more often puts it more in line with STS1's Lagavulin move-wise
/// </summary>
internal static class LagavulinPatch
{
    [HarmonyPatch]
    internal static class LagavulinPatch_LagavulinMatriarch_GenerateMoveStateMachine
    {
        [HarmonyPatch(typeof(LagavulinMatriarch), "GenerateMoveStateMachine")]
        internal static bool Prefix(LagavulinMatriarch __instance, ref MonsterMoveStateMachine __result)
        {
            if (!BuffTheSpireConfig.LagavulinEnabled) { return true; }
            if (!BuffTheSpireConfig.LagavulinDebuffOften) { return true; }

            List<MonsterState> list = new List<MonsterState>();
            MoveState moveState = new MoveState("SLEEP_MOVE", __instance.SleepMove, new SleepIntent());
            MoveState moveState2 = new MoveState("SLASH_MOVE", __instance.SlashMove, new SingleAttackIntent(__instance.SlashDamage));
            MoveState moveState3 = new MoveState("SLASH2_MOVE", __instance.Slash2Move, new SingleAttackIntent(__instance.Slash2Damage), new DefendIntent());
            MoveState moveState4 = new MoveState("DISEMBOWEL_MOVE", __instance.DisembowelMove, new MultiAttackIntent(__instance.DisembowelDamage, __instance.DisembowelRepeat));
            MoveState moveState5 = new MoveState("SOUL_SIPHON_MOVE", __instance.SoulSiphonMove, new DebuffIntent(), new BuffIntent());
            ConditionalBranchState conditionalBranchState = (ConditionalBranchState)(moveState.FollowUpState = new ConditionalBranchState("SLEEP_BRANCH"));
            moveState2.FollowUpState = moveState4;
            moveState4.FollowUpState = moveState5;
            moveState5.FollowUpState = moveState2;
            conditionalBranchState.AddState(moveState, () => __instance.Creature.HasPower<AsleepPower>());
            conditionalBranchState.AddState(moveState2, () => !__instance.Creature.HasPower<AsleepPower>());
            list.Add(conditionalBranchState);
            list.Add(moveState);
            list.Add(moveState2);
            list.Add(moveState3);
            list.Add(moveState5);
            list.Add(moveState4);
            __result = new MonsterMoveStateMachine(list, moveState);
            return false;
        }
    }
}