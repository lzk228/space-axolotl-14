using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Robust.Shared.Random; // RU-Localization

namespace Content.Server.Speech.EntitySystems;

public sealed partial class FrontalLispSystem : EntitySystem
{
    // @formatter:off
    private static readonly Regex RegexUpperTh = new(@"[T]+[Ss]+|[S]+[Cc]+(?=[IiEeYy]+)|[C]+(?=[IiEeYy]+)|[P][Ss]+|([S]+[Tt]+|[T]+)(?=[Ii]+[Oo]+[Uu]*[Nn]*)|[C]+[Hh]+(?=[Ii]*[Ee]*)|[Z]+|[S]+|[X]+(?=[Ee]+)");
    private static readonly Regex RegexLowerTh = new(@"[t]+[s]+|[s]+[c]+(?=[iey]+)|[c]+(?=[iey]+)|[p][s]+|([s]+[t]+|[t]+)(?=[i]+[o]+[u]*[n]*)|[c]+[h]+(?=[i]*[e]*)|[z]+|[s]+|[x]+(?=[e]+)");
    private static readonly Regex RegexUpperEcks = new(@"[E]+[Xx]+[Cc]*|[X]+");
    private static readonly Regex RegexLowerEcks = new(@"[e]+[x]+[c]*|[x]+");
    // @formatter:on

    // RU-Localization Start
    [GeneratedRegex(@"с", RegexOptions.IgnoreCase)]
    private static partial Regex RegexS();

    [GeneratedRegex(@"ч", RegexOptions.IgnoreCase)]
    private static partial Regex RegexCh();

    [GeneratedRegex(@"ц", RegexOptions.IgnoreCase)]
    private static partial Regex RegexTs();

    [GeneratedRegex(@"\B[т](?![АЕЁИОУЫЭЮЯаеёиоуыэюя])", RegexOptions.IgnoreCase)]
    private static partial Regex RegexNonVocalT();

    [GeneratedRegex(@"з", RegexOptions.IgnoreCase)]
    private static partial Regex RegexZ();

    private static readonly (Func<Regex> regex, string lowerReplacement, string upperReplacement)[] Rules = new (Func<Regex>, string, string)[]
    {
        // с - ш
        (RegexS, "ш", "Ш"),
        // ч - ш
        (RegexCh, "ш", "Ш"),
        // ц - ч
        (RegexTs, "ч", "Ч"),
        // т - ч
        (RegexNonVocalT, "ч", "Ч"),
        // з - ж
        (RegexZ, "ж", "Ж"),
    };
    // RU-Localization End

    [Dependency] private readonly IRobustRandom _random = default!; // RU-Localization

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<FrontalLispComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, FrontalLispComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // handles ts, sc(i|e|y), c(i|e|y), ps, st(io(u|n)), ch(i|e), z, s
        message = RegexUpperTh.Replace(message, "TH");
        message = RegexLowerTh.Replace(message, "th");
        // handles ex(c), x
        message = RegexUpperEcks.Replace(message, "EKTH");
        message = RegexLowerEcks.Replace(message, "ekth");

        // RU-Localization Start
        foreach (var (regex, lowerReplacement, upperReplacement) in Rules)
        {
            message = regex()
                .Replace(message,
                    match =>
                    {
                // Checking whether a letter is capitalised
                var replacement = char.IsUpper(match.Value[0]) ? upperReplacement : lowerReplacement;
                return _random.Prob(0.90f) ? replacement : match.Value;
            });
        }
        // RU-Localization End

        args.Message = message;
    }
}
