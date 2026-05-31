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
using BuffTheSpire.Config;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace BuffTheSpire.Powers
{
    public sealed class SuckerPunchPower : CustomPowerModel
    {
        private class Data
        {
            public int counter = BuffTheSpireConfig.KaiserCrabSuckerPunchQty;
        }
        public override string? CustomPackedIconPath => "res://BuffTheSpire/images/powers/suckerPunch.png";
        public override string? CustomBigIconPath => "res://BuffTheSpire/images/powers/suckerPunch.png";
        public override string? CustomBigBetaIconPath => "res://BuffTheSpire/images/powers/suckerPunch.png";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        public override int DisplayAmount => GetInternalData<Data>().counter;
        protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar>() { new DynamicVar("counter", BuffTheSpireConfig.KaiserCrabSuckerPunchQty) };
        protected override object InitInternalData()
        {
            return new Data();
        }

        public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            Data data = GetInternalData<Data>();

            // Reduce counter on being hit
            if (target.HasPower<SuckerPunchPower>() && result.UnblockedDamage > 0)
            {
                if (data.counter > 0)
                {
                    data.counter--;
                    InvokeDisplayAmountChanged();
                    Flash();
                }
            }

            // Add dazed on hit
            if
            (
                dealer != null
                && target.IsPlayer
                && dealer.HasPower<SuckerPunchPower>()
                && dealer == this.Owner
                && result.UnblockedDamage > 0
                && data.counter > 0
            )
            {
                await CardPileCmd.AddToCombatAndPreview<Dazed>(target, PileType.Draw, data.counter, null);
                Flash();
            }
        }

        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (side == CombatSide.Enemy)
            {
                Data data = GetInternalData<Data>();
                data.counter = BuffTheSpireConfig.KaiserCrabSuckerPunchQty;
                InvokeDisplayAmountChanged();
            }
        }
    }
}