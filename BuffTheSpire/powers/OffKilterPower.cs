using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.ValueProps;

namespace BuffTheSpire.Powers
{
    public sealed class OffKilterPower : CustomPowerModel
    {
        public override string? CustomPackedIconPath => "res://BuffTheSpire/images/powers/offKilter.png";
        public override string? CustomBigIconPath => "res://BuffTheSpire/images/powers/offKilter.png";
        public override string? CustomBigBetaIconPath => "res://BuffTheSpire/images/powers/offKilter.png";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.None;
        protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[] { HoverTipFactory.FromPower<WobblePower>() };
        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<WobblePower>(new ThrowingPlayerChoiceContext(), this.Owner, 1, this.Owner, null);
        }
    }

}