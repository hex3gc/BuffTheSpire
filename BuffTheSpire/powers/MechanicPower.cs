using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Entities.Cards;
using BuffTheSpire.Config;

namespace BuffTheSpire.Powers
{
    public sealed class MechanicPower : CustomPowerModel
    {
        public override string? CustomPackedIconPath => "res://BuffTheSpire/images/powers/mechanic.png";
        public override string? CustomBigIconPath => "res://BuffTheSpire/images/powers/mechanic.png";
        public override string? CustomBigBetaIconPath => "res://BuffTheSpire/images/powers/mechanic.png";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        public override bool ShouldScaleInMultiplayer => true;
        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (side != CombatSide.Enemy)
            {
                return;
            }
            IEnumerable<Creature> enumerable = base.CombatState.Enemies.Where((Creature c) => c.Monster is LivingShield);
            foreach (Creature item in enumerable)
            {
                await CreatureCmd.Heal(item, this.Amount);
            }
        }
    }
}