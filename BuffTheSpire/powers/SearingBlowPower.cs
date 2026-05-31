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
    public sealed class SearingBlowPower : CustomPowerModel
    {
        private class Data
        {
            public int counter = BuffTheSpireConfig.KaiserCrabSearingBlowQty;
        }
        public override string? CustomPackedIconPath => "res://BuffTheSpire/images/powers/searingBlow.png";
        public override string? CustomBigIconPath => "res://BuffTheSpire/images/powers/searingBlow.png";
        public override string? CustomBigBetaIconPath => "res://BuffTheSpire/images/powers/searingBlow.png";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        public override int DisplayAmount => GetInternalData<Data>().counter;
        protected override IEnumerable<DynamicVar> CanonicalVars => new List<DynamicVar>() { new DynamicVar("counter", BuffTheSpireConfig.KaiserCrabSearingBlowQty) };
        protected override object InitInternalData()
        {
            return new Data();
        }

        public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            Data data = GetInternalData<Data>();

            // Reduce counter on being hit
            if (target.HasPower<SearingBlowPower>() && result.UnblockedDamage > 0)
            {
                if (data.counter > 0) 
                {
                    data.counter--;
                    InvokeDisplayAmountChanged();
                    Flash();
                }
            }

            // Transform cards into burns on hit
            if 
            (   
                dealer != null 
                && target.IsPlayer
                && dealer.HasPower<SearingBlowPower>() 
                && dealer == this.Owner
                && result.UnblockedDamage > 0
                && data.counter > 0
            )
            {
                for (int i = 0; i < data.counter; i++)
                {
                    if (i < target.Player.PlayerCombatState.DrawPile.Cards.Count)
                    {
                        await CardCmd.TransformTo<Burn>(target.Player.PlayerCombatState.DrawPile.Cards[i]);
                    }
                }
                Flash();
            }
        }

        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (side == CombatSide.Enemy)
            {
                Data data = GetInternalData<Data>();
                data.counter = BuffTheSpireConfig.KaiserCrabSearingBlowQty;
                InvokeDisplayAmountChanged();
            }
        }
    }
}