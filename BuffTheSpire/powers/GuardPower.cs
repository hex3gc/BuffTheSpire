using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace BuffTheSpire.Powers
{
    public sealed class GuardPower : CustomPowerModel
    {
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.None;
        public override string? CustomPackedIconPath => "res://BuffTheSpire/images/powers/guard.png";
        public override string? CustomBigIconPath => "res://BuffTheSpire/images/powers/guard.png";
        public override string? CustomBigBetaIconPath => "res://BuffTheSpire/images/powers/guard.png";

        public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            if (target != null && target != base.Owner && target.Side == base.Owner.Side)
            {
                return 0.5m;
            }

            return 1m;
        }
    }
}
