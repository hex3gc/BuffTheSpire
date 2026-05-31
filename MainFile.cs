using BuffTheSpire.Config;
using BaseLib.Config;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace BuffTheSpire;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "BuffTheSpire";
    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        ModConfigRegistry.Register(ModId, new BuffTheSpireConfig());
        Harmony harmony = new(ModId);
        harmony.PatchAll();
    }
}
