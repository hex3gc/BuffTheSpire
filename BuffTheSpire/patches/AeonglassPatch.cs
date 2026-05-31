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

namespace BuffTheSpire.Patches;

internal static class AeonglassPatch
{
    /*
    // HP change
    [HarmonyPatch]
    internal static class AeonglassPatch_Aeonglass_MinInitialHp
    {
        [HarmonyPatch(typeof(Aeonglass), "MinInitialHp", MethodType.Getter)]
        internal static bool Prefix(Aeonglass __instance, ref int __result)
        {
            if (!BuffTheSpireConfig.AeonglassEnabled) { return true; }

            __result = AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, (int)BuffTheSpireConfig.AeonglassMaxHpHard, (int)BuffTheSpireConfig.AeonglassMaxHpEasy);
            return false;
        }
    }
    */
}