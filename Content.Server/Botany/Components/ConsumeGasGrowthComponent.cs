using Content.Shared.Atmos;

namespace Content.Server.Botany.Components
{
    [RegisterComponent]
    public sealed partial class ConsumeGasGrowthComponent : PlantGrowthComponent
    {
        [DataField("consumeGasses")] public Dictionary<Gas, float> ConsumeGasses = new();
    }
}
