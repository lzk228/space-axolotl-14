using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Robust.Shared.Random; // RU-Localization

namespace Content.Server.Speech.EntitySystems;

public sealed partial class LizardAccentSystem : EntitySystem
{
    private static readonly Regex RegexLowerS = new("s+");
    private static readonly Regex RegexUpperS = new("S+");
    private static readonly Regex RegexInternalX = new(@"(\w)x");
    private static readonly Regex RegexLowerEndX = new(@"\bx([\-|r|R]|\b)");
    private static readonly Regex RegexUpperEndX = new(@"\bX([\-|r|R]|\b)");

    // RU-Localization-Start
    [GeneratedRegex(@"с+", RegexOptions.IgnoreCase)]
    private static partial Regex RegexS();

    [GeneratedRegex(@"з+", RegexOptions.IgnoreCase)]
    private static partial Regex RegexZ();

    [GeneratedRegex(@"ш+", RegexOptions.IgnoreCase)]
    private static partial Regex RegexSh();

    [GeneratedRegex(@"ч+", RegexOptions.IgnoreCase)]
    private static partial Regex RegexCh();

    private static readonly (Func<Regex> regex, string[] lowerReplacements, string[] upperReplacements)[] Rules = new (Func<Regex>, string[], string[])[]
    {
        // c => ссс
        (RegexS, ["сс", "ссс"], ["СС", "ССС"]),
        // з => ссс
        (RegexZ, ["сс", "ссс"], ["СС", "ССС"]),
        // ш => шшш
        (RegexSh, ["шш", "шшш"], ["ШШ", "ШШШ"]),
        // ч => щщщ
        (RegexCh, ["щщ", "щщщ"], ["ЩЩ", "ЩЩЩ"]),
    };
    // RU-Localization-End

    [Dependency] private readonly IRobustRandom _random = default!; // RU-Localization

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<LizardAccentComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, LizardAccentComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // hissss
        message = RegexLowerS.Replace(message, "sss");
        // hiSSS
        message = RegexUpperS.Replace(message, "SSS");
        // ekssit
        message = RegexInternalX.Replace(message, "$1kss");
        // ecks
        message = RegexLowerEndX.Replace(message, "ecks$1");
        // eckS
        message = RegexUpperEndX.Replace(message, "ECKS$1");

        // RU-Localization-Start
        foreach (var (regex, lowerReplacements, upperReplacements) in Rules)
        {
            message = regex()
                .Replace(message,
                    match =>
                    {
                        var replacements = char.IsUpper(match.Value[0]) ? upperReplacements : lowerReplacements;
                        return _random.Pick(replacements);
                    });
        }
        // RU-Localization-End

        args.Message = message;
    }
}
