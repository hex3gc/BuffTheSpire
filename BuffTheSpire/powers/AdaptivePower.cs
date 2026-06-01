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
    public sealed class AdaptivePower : CustomPowerModel
    {
        public override string? CustomPackedIconPath => "res://BuffTheSpire/images/powers/adaptive.png";
        public override string? CustomBigIconPath => "res://BuffTheSpire/images/powers/adaptive.png";
        public override string? CustomBigBetaIconPath => "res://BuffTheSpire/images/powers/adaptive.png";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        public int StockAmount = 0;
        public override async Task BeforeCardPlayed(CardPlay cardPlay)
        {
            if (base.Owner.Monster is Axebot)
            {
                Axebot ownerBot = base.Owner.Monster as Axebot;
                StockAmount = this.Amount;
            }
        }
        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Card.Type == CardType.Attack && base.Owner.Monster is Axebot)
            {
                Axebot ownerBot = base.Owner.Monster as Axebot;
                
                if (StockAmount == this.Amount)
                {
                    await PowerCmd.Apply<VigorPower>(choiceContext, base.Owner, BuffTheSpireConfig.AxebotAdaptiveVigorGain * this.Amount, base.Owner, null);
                    await CreatureCmd.GainBlock(base.Owner, BuffTheSpireConfig.AxebotAdaptiveBlockGain * this.Amount, ValueProp.Move, null);
                }
                StockAmount = 0;
            }
        }
    }
}