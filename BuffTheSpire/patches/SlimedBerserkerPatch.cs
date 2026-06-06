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
///     It can be too easy to set up your entire deck before you encounter the berserker's slimes
///     Adding some to the draw pile gives you an immediate problem to deal with
/// </summary>
internal static class SlimedBerserkerPatch
{
    public static async Task VomitIchorMoveReplacement(this SlimedBerserker __instance, IReadOnlyList<Creature> targets)
    {
        SfxCmd.Play(__instance.SlimeSfx);
        await CreatureCmd.TriggerAnim(__instance.Creature, "Vomit", 0.7f);

        if (BuffTheSpireConfig.SlimedBerserkerDiscards > 0)
        {
            await CardPileCmd.AddToCombatAndPreview<Slimed>(targets, PileType.Discard, BuffTheSpireConfig.SlimedBerserkerDiscards, null, CardPilePosition.Random);
        }
        if (BuffTheSpireConfig.SlimedBerserkerDraws > 0)
        {
            await CardPileCmd.AddToCombatAndPreview<Slimed>(targets, PileType.Draw, BuffTheSpireConfig.SlimedBerserkerDraws, null, CardPilePosition.Random);
        }
    }

    [HarmonyPatch]
    internal static class SlimedBerserkerPatch_SlimedBerserker_GenerateMoveStateMachine
    {
        [HarmonyPatch(typeof(SlimedBerserker), "GenerateMoveStateMachine")]
        internal static bool Prefix(SlimedBerserker __instance, ref MonsterMoveStateMachine __result)
        {
            if (!BuffTheSpireConfig.SlimedBerserkerEnabled) { return true; }

            List<MonsterState> list = new List<MonsterState>();
            MoveState moveState = new MoveState("VOMIT_ICHOR_MOVE", __instance.VomitIchorMoveReplacement, new StatusIntent(10));
            MoveState moveState2 = new MoveState("LEECHING_HUG_MOVE", __instance.LeechingHugMove, new DebuffIntent(), new BuffIntent());
            MoveState moveState3 = new MoveState("SMOTHER_MOVE", __instance.SmotherMove, new SingleAttackIntent(__instance.SmotherDamage));
            MoveState moveState4 = (MoveState)(moveState.FollowUpState = new MoveState("FURIOUS_PUMMELING_MOVE", __instance.FuriousPummelingMove, new MultiAttackIntent(__instance.PummelingDamage, 4)));
            moveState4.FollowUpState = moveState2;
            moveState2.FollowUpState = moveState3;
            moveState3.FollowUpState = moveState;
            list.Add(moveState);
            list.Add(moveState3);
            list.Add(moveState2);
            list.Add(moveState4);
            __result = new MonsterMoveStateMachine(list, moveState);
            return false;
        }
    }
}