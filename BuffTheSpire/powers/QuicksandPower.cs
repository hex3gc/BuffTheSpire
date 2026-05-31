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
    public sealed class QuicksandPower : CustomPowerModel
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
    }
}