using Content.Server.Botany.Components;

namespace Content.Server.Botany.Systems
{
    public abstract class PlantGrowthSystem : EntitySystem
    {
        public override void Initialize()
        {
            base.Initialize();
        }

        //TODO: it is possible that I need a generic call here for OnGrow(), and it implements some setup that
        //pass that along to the right system by the type of the component?
    }

    [ByRefEvent]
    public readonly record struct GrowEvent(PlantGrowthComponent component, PlantHolderComponent holder);
}
