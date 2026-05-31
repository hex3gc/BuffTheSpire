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

namespace BuffTheSpire.Powers
{
    public sealed class QuicksandPower : CustomPowerModel
    {
        public override string? CustomPackedIconPath => "res://BuffTheSpire/images/powers/quicksand.png";
        public override string? CustomBigIconPath => "res://BuffTheSpire/images/powers/quicksand.png";
        public override string? CustomBigBetaIconPath => "res://BuffTheSpire/images/powers/quicksand.png";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        public override async Task AfterSideTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            int sandpitStacks = 0;
            foreach (Creature creature in this.CombatState.Enemies)
            {
                if (creature.Side == CombatSide.Enemy && creature.HasPower<SandpitPower>())
                {
                    foreach (PowerModel power in creature.Powers)
                    {
                        if (power is SandpitPower && power.Target == base.Owner)
                        {
                            sandpitStacks += power.Amount;
                        }
                    }
                }
            }

            if (participants.Contains(base.Owner) && sandpitStacks > 0)
            {
                await CreatureCmd.Damage(choiceContext, base.Owner, base.Amount * sandpitStacks, ValueProp.Unpowered, base.Owner, null);
                VfxCmd.PlayOnCreatureCenter(base.Owner, "vfx/vfx_attack_blunt");
            }
        }
    }
}