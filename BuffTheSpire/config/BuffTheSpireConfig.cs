using BaseLib.Config;

namespace BuffTheSpire.Config
{
    [HoverTipsByDefault]
    internal class BuffTheSpireConfig : SimpleModConfig
    {
        // ELITES - ACT 1
        [ConfigSection("ByrdonisSection")]
            public static bool ByrdonisEnabled { get; set; } = true;
            [ConfigSlider(60, 120, 1, Format = "{0} HP")]
            public static int ByrdonisMinMaxHpEasy { get; set; } = 91;
            [ConfigSlider(60, 120, 1, Format = "{0} HP")]
            public static int ByrdonisMaxMaxHpEasy { get; set; } = 94;
            [ConfigSlider(60, 120, 1, Format = "{0} HP")]
            public static int ByrdonisMinMaxHpHard { get; set; } = 99;
            [ConfigSlider(60, 120, 1, Format = "{0} HP")]
            public static int ByrdonisMaxMaxHpHard { get; set; } = 99;

        // BOSSES - ACT 1
        [ConfigSection("LagavulinSection")]
            public static bool LagavulinEnabled { get; set; } = true;
            public static bool LagavulinDebuffOften { get; set; } = true;
        [ConfigSection("TheKinSection")]
            public static bool TheKinEnabled { get; set; } = true;
            public static bool TheKinBuffLeader { get; set; } = true;

        // ELITES - ACT 2
        [ConfigSection("EntomancerSection")]
            public static bool EntomancerEnabled { get; set; } = true;
            [ConfigSlider(100, 200, 1, Format = "{0} HP")]
            public static int EntomancerHpEasy { get; set; } = 160;
            [ConfigSlider(100, 200, 1, Format = "{0} HP")]
            public static int EntomancerHpHard { get; set; } = 175;

        // BOSSES - ACT 2
        [ConfigSection("KaiserCrabSection")]
            public static bool KaiserCrabEnabled { get; set; } = true;
            public static bool KaiserCrabSearingBlow { get; set; } = true;
            [ConfigSlider(1, 10, 1)]
            public static int KaiserCrabSearingBlowQty { get; set; } = 2;
            public static bool KaiserCrabSuckerPunch { get; set; } = true;
            [ConfigSlider(1, 10, 1)]
            public static int KaiserCrabSuckerPunchQty { get; set; } = 3;

        [ConfigSection("TheInsatiableSection")]
            public static bool TheInsatiableEnabled { get; set; } = true;
            public static bool TheInsatiableQuicksand { get; set; } = true;
            public static bool TheInsatiableQuicksandBuff { get; set; } = true;
            [ConfigSlider(1, 10, 1)]
            public static int TheInsatiableQuicksandDamage { get; set; } = 2;
            [ConfigSlider(1, 20, 1, Format = "{0}x2")]
            public static int TheInsatiableThrashDamageEasy { get; set; } = 7;
            [ConfigSlider(1, 20, 1, Format = "{0}x2")]
            public static int TheInsatiableThrashDamageHard { get; set; } = 8;
            [ConfigSlider(1, 20, 1)]
            public static int TheInsatiableBiteDamageEasy { get; set; } = 25;
            [ConfigSlider(1, 20, 1)]
            public static int TheInsatiableBiteDamageHard { get; set; } = 28;

        // ENEMIES - ACT 3
        [ConfigSection("AxebotSection")]
            public static bool AxebotEnabled { get; set; } = true;
            [ConfigSlider(1, 12, 1)]
            public static int AxebotAdaptiveVigorGain { get; set; } = 1;
            [ConfigSlider(1, 12, 1)]
            public static int AxebotAdaptiveBlockGain { get; set; } = 2;

        [ConfigSection("SlimedBerserkerSection")]
            public static bool SlimedBerserkerEnabled { get; set; } = true;
            [ConfigSlider(0, 10, 1)]
            public static int SlimedBerserkerDiscards { get; set; } = 5;
            [ConfigSlider(0, 10, 1)]
            public static int SlimedBerserkerDraws { get; set; } = 5;

        [ConfigSection("ScrollSection")]
            public static bool ScrollEnabled { get; set; } = true;
            [ConfigSlider(1, 10, 1, Format = "{0} Max HP")]
            public static int ScrollMaxHpReduction { get; set; } = 5;
            [ConfigSlider(20, 80, 1, Format = "{0} HP")]
            public static int ScrollMinMaxHpEasy { get; set; } = 38;
            [ConfigSlider(20, 80, 1, Format = "{0} HP")]
            public static int ScrollMaxMaxHpEasy { get; set; } = 44;
            [ConfigSlider(20, 80, 1, Format = "{0} HP")]
            public static int ScrollMinMaxHpHard { get; set; } = 40;
            [ConfigSlider(20, 80, 1, Format = "{0} HP")]
            public static int ScrollMaxMaxHpHard { get; set; } = 46;

        [ConfigSection("GlobeheadSection")]
            public static bool GlobeheadEnabled { get; set; } = true;
            [ConfigSlider(1, 24, 1)]
            public static int GlobeheadPowerDamage { get; set; } = 12;

        [ConfigSection("LostForgottenSection")]
            public static bool LostForgottenEnabled { get; set; } = true;

        [ConfigSection("TurretOperatorSection")]
            public static bool TurretOperatorEnabled { get; set; } = true;
            [ConfigSlider(0, 40, 1)]
            public static int TurretOperatorMechanicAmount { get; set; } = 20;
            [ConfigSlider(0, 8, 1)]
            public static int TurretOperatorAdditionalStrengthGain { get; set; } = 1;

        // ELITES - ACT 3
        [ConfigSection("KnightTrioSection")]
            public static bool KnightTrioEnabled { get; set; } = true;
            public static bool KnightTrioGuard { get; set; } = true;

        [ConfigSection("SoulNexusSection")]
            public static bool SoulNexusEnabled { get; set; } = true;
            [ConfigSlider(1, 10, 1)]
            public static int SoulNexusHits { get; set; } = 5;

        [ConfigSection("MechaKnightSection")]
            public static bool MechaKnightEnabled { get; set; } = true;
            [ConfigSlider(0, 30, 1)]
            public static bool MechaKnightOffKilter { get; set; } = true;
            [ConfigSlider(0, 30, 1)]
            public static int MechaknightStrengthBuff { get; set; } = 5;
            [ConfigSlider(0, 120, 1)]
            public static int MechaknightChargeDamageEasy { get; set; } = 25;
            [ConfigSlider(0, 120, 1)]
            public static int MechaknightChargeDamageHard { get; set; } = 35;
            [ConfigSlider(0, 120, 1)]
            public static int MechaknightCleaveDamageEasy { get; set; } = 50;
            [ConfigSlider(0, 120, 1)]
            public static int MechaknightCleaveDamageHard { get; set; } = 60;

        // BOSSES - ACT 3
        [ConfigSection("QueenSection")]
            public static bool QueenEnabled { get; set; } = true;
            [ConfigSlider(1, 10, 1)]
            public static int QueenCardsAfflicted { get; set; } = 3;
            public static bool QueenBurnsAdd { get; set; } = true;
            [ConfigSlider(1, 3, 1)]
            public static int QueenBurnsAddCount { get; set; } = 1;
            public static bool QueenMoreBeams { get; set; } = true;
    }
}