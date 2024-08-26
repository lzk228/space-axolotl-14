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

        public void AffectGrowth(int amount, PlantHolderComponent? component = null)
        {
            if (component == null || component.Seed == null)
                return;

            if (amount > 0)
            {
                if (component.Age < component.Seed.Maturation)
                    component.Age += amount;
                else if (!component.Harvest && component.Seed.Yield <= 0f)
                    component.LastProduce -= amount;
            }
            else
            {
                if (component.Age < component.Seed.Maturation)
                    component.SkipAging++;
                else if (!component.Harvest && component.Seed.Yield <= 0f)
                    component.LastProduce += amount;
            }
        }
    }

    [ByRefEvent]
    public readonly record struct GrowEvent(PlantGrowthComponent component, PlantHolderComponent holder);
}
