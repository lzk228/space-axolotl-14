using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Robust.Shared.Random; // RU-Localization

namespace Content.Server.Speech.EntitySystems;

public sealed class MothAccentSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!; // RU-Localization

    private static readonly Regex RegexLowerBuzz = new Regex("z{1,3}");
    private static readonly Regex RegexUpperBuzz = new Regex("Z{1,3}");

    // RU-Localization-Start
    private static readonly string[] LowerJ = ["жж", "жжж"];
    private static readonly string[] UpperJ = ["ЖЖ", "ЖЖЖ"];
    private static readonly string[] LowerZ = ["зз", "ззз"];
    private static readonly string[] UpperZ = ["ЗЗ", "ЗЗЗ"];
    // RU-Localization-End

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<MothAccentComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, MothAccentComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // buzzz
        message = RegexLowerBuzz.Replace(message, "zzz");
        // buZZZ
        message = RegexUpperBuzz.Replace(message, "ZZZ");

        // RU-Localization-Start
        // ж => жжж
        message = message.Replace("ж", _random.Pick(LowerJ));
        // Ж => ЖЖЖ
        message = message.Replace("Ж", _random.Pick(UpperJ));
        // з => ззз
        message = message.Replace("з", _random.Pick(LowerZ));
        // З => ЗЗЗ
        message = message.Replace("З", _random.Pick(UpperZ));
        // RU-Localization-End

        args.Message = message;
    }
}
