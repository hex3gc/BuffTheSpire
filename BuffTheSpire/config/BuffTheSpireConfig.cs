using BaseLib.Config;

namespace BuffTheSpire.Config
{
    [HoverTipsByDefault]
    internal class BuffTheSpireConfig : SimpleModConfig
    {
        // BOSSES - ACT 1
        [ConfigSection("LagavulinSection")]
            public static bool LagavulinEnabled { get; set; } = true;
            public static bool LagavulinDebuffOften { get; set; } = true;
        [ConfigSection("TheKinSection")]
            public static bool TheKinEnabled { get; set; } = true;
            public static bool TheKinBuffLeader { get; set; } = true;

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

        // BOSSES - ACT 3
        /*
        [ConfigSection("AeonglassSection")]
            public static bool AeonglassEnabled { get; set; } = true;
            [ConfigSlider(400, 800, 1, Format = "{0} HP")]
            public static int AeonglassMaxHpEasy { get; set; } = 582;
            [ConfigSlider(400, 800, 1, Format = "{0} HP")]
            public static int AeonglassMaxHpHard { get; set; } = 612;
        */

        [ConfigSection("QueenSection")]
            public static bool QueenEnabled { get; set; } = true;
            [ConfigSlider(1, 10, 1)]
            public static int QueenCardsAfflicted { get; set; } = 3;
            public static bool QueenBurnsAdd { get; set; } = true;
            [ConfigSlider(1, 3, 1)]
            public static int QueenBurnsAddCount { get; set; } = 1;
            public static bool QueenMoreBeams { get; set; } = true;

        // ELITES - ACT 3
        [ConfigSection("SoulNexusSection")]
            public static bool SoulNexusEnabled { get; set; } = true;
            [ConfigSlider(1, 10, 1)]
            public static int SoulNexusHits { get; set; } = 5;

        // ENEMIES - ACT 3
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
    }
}